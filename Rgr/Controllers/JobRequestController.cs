﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rgr.Data;

namespace Rgr.Controllers;

[ApiController]
[Route("job-request")]
public class JobRequestController : Controller
{
    private readonly DataContext _ctx;

    public JobRequestController(DataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpGet("client/{id}")]
    public async Task<ActionResult<List<JobRequest>>> Get(string id)
    {
        var client = await _ctx.Clients.Include(i => i.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        if (client == null)
            return BadRequest("Client not found");
        return Ok(client.ClientsRequests);
    }

    [HttpPost("client/{id}")]
    public async Task<ActionResult<List<JobRequest>>> AddClient(string id, ModifyJobRequestDto jobRequest)
    {
        var dbClient = await _ctx.Clients.Include(i => i.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        if (dbClient == null)
            return BadRequest("Client not found");

        var newJobRequest = new JobRequest()
        {
            Id = Guid.NewGuid().ToString(),
            PositionName = jobRequest.PositionName,
            Salary = jobRequest.Salary
        };

        dbClient.ClientsRequests.Add(newJobRequest);
        await _ctx.SaveChangesAsync();

        var result = await _ctx.Clients.Include(c => c.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        return Ok(newJobRequest);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<JobRequest>> UpdateJobRequest(string id, JobRequest request)
    {
        var dbJobRequest = await _ctx.JobRequests.FindAsync(request.Id);
        if (dbJobRequest == null)
            return BadRequest("Job request not found");

        dbJobRequest.PositionName = request.PositionName;
        dbJobRequest.Salary = request.Salary;

        await _ctx.SaveChangesAsync();

        return Ok(dbJobRequest);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> Delete(string id)
    {
        var dbClient = await _ctx.JobRequests.FindAsync(id);
        if (dbClient == null)
            return BadRequest("Client no found");

        _ctx.JobRequests.Remove(dbClient);
        await _ctx.SaveChangesAsync();

        return Ok();
    }
}