using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rgr.Data;

namespace Rgr.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestController : Controller
{
    private readonly DataContext _ctx;

    public RequestController(DataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpGet("/client/{id}")]
    public async Task<ActionResult<List<JobRequest>>> Get(string id)
    {
        var client = await _ctx.Clients.Include(i => i.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        if (client == null)
            return BadRequest("Client not found");
        return Ok(client.ClientsRequests);
    }

    [HttpPost("/client/{id}")]
    public async Task<ActionResult<List<JobRequest>>> AddClient(string id, CreateJobRequestDto jobRequest)
    {
        var dbClient = await _ctx.Clients.Include(i => i.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        if (dbClient == null)
            return BadRequest("Client not found");

        dbClient.ClientsRequests.Add(new JobRequest()
        {
            Id = Guid.NewGuid().ToString(),
            PositionName = jobRequest.PositionName,
            Salary = jobRequest.Salary
        });
        await _ctx.SaveChangesAsync();

        var result = await _ctx.Clients.Include(c => c.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        return Ok(result?.ClientsRequests);
    }

    [HttpPut]
    public async Task<ActionResult<List<Client>>> UpdateClient(Client request)
    {
        var dbClient = await _ctx.Clients.FindAsync(request.Id);
        if (dbClient == null)
            return BadRequest("Client not found");

        dbClient.Name = request.Name;
        dbClient.MiddleName = request.MiddleName;
        dbClient.Surname = request.Surname;
        dbClient.Address = request.Address;
        dbClient.Gender = request.Gender;
        dbClient.ReceiptNumber = request.ReceiptNumber;
        dbClient.RegistryNumber = request.RegistryNumber;

        await _ctx.SaveChangesAsync();

        return Ok(await _ctx.Clients.ToListAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Client>>> Delete(int id)
    {
        var dbClient = await _ctx.Clients.FindAsync(id);
        if (dbClient == null)
            return BadRequest("Client no found");

        _ctx.Clients.Remove(dbClient);
        await _ctx.SaveChangesAsync();

        return Ok(await _ctx.Clients.ToArrayAsync());
    }
}