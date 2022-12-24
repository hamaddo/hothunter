﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rgr.Data;

namespace Rgr.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController: Controller
{
    private readonly ClientDataContext _ctx;

    public ClientsController(ClientDataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpGet]
    public async Task<ActionResult<List<Client>>> Get()
    {
        return Ok(await _ctx.Clients.Include(c=> c.ClientsRequests).ToListAsync());
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<List<Client>>> Get(string id)
    {
        var client = await _ctx.Clients.Include(i => i.ClientsRequests).FirstOrDefaultAsync(i => i.Id == id);
        if (client == null)
            return BadRequest("Client not found");
        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<List<Client>>> AddClient(CreateClientDto client)
    {
        var newClient = new Client()
        {
            Id = Guid.NewGuid().ToString(),
            MiddleName = client.MiddleName,
            Name = client.Name,
            Surname = client.Surname,
            Address = client.Address,
            Gender = client.Gender,
            Phone = client.Phone,
            RegistryNumber = client.RegistryNumber,
            ReceiptNumber = client.ReceiptNumber,
        };
        _ctx.Clients.Add(newClient);
        await _ctx.SaveChangesAsync();

        return Ok(await _ctx.Clients.ToArrayAsync());
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

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<List<Client>>> Delete(string id)
    {
        var dbClient = await _ctx.Clients.FindAsync(id);
        if (dbClient == null)
            return BadRequest("Client no found");

        _ctx.Clients.Remove(dbClient);
        await _ctx.SaveChangesAsync();

        return Ok(await _ctx.Clients.ToArrayAsync());
    }
}