using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rgr.Data;

namespace Rgr.Controllers;

[ApiController]
[Route("employers")]
public class EmployersController : Controller
{
    private readonly DataContext _ctx;

    public EmployersController(DataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpGet]
    public async Task<ActionResult<List<Employer>>> Get()
    {
        return Ok(await _ctx.Employers.Include(e => e.EmployerOffers).ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employer>> Get(string id)
    {
        var employer = await _ctx.Employers.Include(i => i.EmployerOffers).FirstOrDefaultAsync(i => i.Id == id);
        if (employer == null)
            return BadRequest("Employer not found");
        return Ok(employer);
    }

    [HttpPost]
    public async Task<ActionResult<Employer>> AddEmployer(ModifyEmployerDto employer)
    {
        var newEmployer = new Employer()
        {
            Id = Guid.NewGuid().ToString(),
            Name = employer.Name,
            OwnerShipType = employer.OwnerShipType,
            Address = employer.Address,
            Phone = employer.Phone,
            RegistryNumber = employer.RegistryNumber,
        };
        _ctx.Employers.Add(newEmployer);
        await _ctx.SaveChangesAsync();

        return Ok(newEmployer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Employer>> UpdateClient(string id, ModifyEmployerDto request)
    {
        var dbEmployer = await _ctx.Employers.FindAsync(id);
        if (dbEmployer == null)
            return BadRequest("Client not found");

        dbEmployer.Name = request.Name;
        dbEmployer.OwnerShipType = request.OwnerShipType;
        dbEmployer.Address = request.Address;
        dbEmployer.Phone = request.Phone;
        dbEmployer.RegistryNumber = request.RegistryNumber;

        await _ctx.SaveChangesAsync();

        return Ok(dbEmployer);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Employer>> Delete(string id)
    {
        var dbEmployer = await _ctx.Employers.Include(e => e.EmployerOffers).FirstOrDefaultAsync(i => i.Id == id);
        if (dbEmployer == null)
            return BadRequest("Client no found");

        
        foreach (var offer in dbEmployer.EmployerOffers)
        {
            _ctx.Offers.Remove(offer);
        }
        
        _ctx.Employers.Remove(dbEmployer);
        await _ctx.SaveChangesAsync();

        return Ok();
    }
}