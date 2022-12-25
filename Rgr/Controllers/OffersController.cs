using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rgr.Data;

namespace Rgr.Controllers;

[ApiController]
[Route("offers")]
public class OffersController : Controller
{
    private readonly DataContext _ctx;

    public OffersController(DataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpGet("employer/{id}")]
    public async Task<ActionResult<List<Offer>>> Get(string id)
    {
        var employer = await _ctx.Employers.Include(i => i.EmployerOffers).FirstOrDefaultAsync(i => i.Id == id);
        if (employer == null)
            return BadRequest("Employer not found");
        return Ok(employer.EmployerOffers);
    }

    [HttpPost("employer/{id}")]
    public async Task<ActionResult<List<Offer>>> AddClient(string id, ModifyOfferDto offerDto)
    {
        var dbEmployer = await _ctx.Employers.Include(i => i.EmployerOffers).FirstOrDefaultAsync(i => i.Id == id);
        if (dbEmployer == null)
            return BadRequest("Employer not found");

        var newJobRequest = new Offer()
        {
            Id = Guid.NewGuid().ToString(),
            PositionName = offerDto.PositionName,
            Gender = offerDto.Gender,
            Salary = offerDto.Salary
        };

        dbEmployer.EmployerOffers.Add(newJobRequest);
        await _ctx.SaveChangesAsync();

        return Ok(newJobRequest);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Offer>> UpdateOffer(string id, ModifyOfferDto modifyOfferDto)
    {
        var dbOffer = await _ctx.Offers.FindAsync(id);
        if (dbOffer == null)
            return BadRequest("Offer not found");

        dbOffer.PositionName = modifyOfferDto.PositionName;
        dbOffer.Salary = modifyOfferDto.Salary;
        dbOffer.Gender = modifyOfferDto.Gender;

        await _ctx.SaveChangesAsync();

        return Ok(dbOffer);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Offer>> Delete(string id)
    {
        var dbOffer = await _ctx.Offers.FindAsync(id);
        if (dbOffer == null)
            return BadRequest("Job request not found");

        _ctx.Offers.Remove(dbOffer);
        await _ctx.SaveChangesAsync();

        return Ok();
    }
}