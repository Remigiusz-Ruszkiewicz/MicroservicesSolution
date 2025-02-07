using Api.Data;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ClientsService : IClientsService
{
    private readonly AppDbContext _dbcontext;

    public ClientsService(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Client> AddClient(Client client)
    {
        await _dbcontext.Clients.AddAsync(client);
        await _dbcontext.SaveChangesAsync();
        return client;
    }

    public async Task<Enums.OperationResult> EditClient(Client client)
    {
        var existingClient = await _dbcontext.Clients.FindAsync(client.Id);
        if (existingClient == null)
        {
            return Enums.OperationResult.Error;  // Client not found
        }

        existingClient.Name = client.Name;
        existingClient.Surname = client.Surname;
        existingClient.Cars = client.Cars;

        _dbcontext.Clients.Update(existingClient);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok; // Update success
    }

    public async Task<Enums.OperationResult> DeleteClient(int id)
    {
        var client = await _dbcontext.Clients.FindAsync(id);
        if (client == null)
        {
            return Enums.OperationResult.Error;  // Client not found
        }

        _dbcontext.Clients.Remove(client);
        await _dbcontext.SaveChangesAsync();

        return Enums.OperationResult.Ok;
    }

    public async Task<ICollection<Client>> GetAllClients()
    {
        return await _dbcontext.Clients.ToListAsync();
    }

    public async Task<Client> GetClientById(int id)
    {
        return await _dbcontext.Clients.FindAsync(id);
    }
}