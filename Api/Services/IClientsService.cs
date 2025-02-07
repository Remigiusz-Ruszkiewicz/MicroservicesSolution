using Api.Helpers;
using Api.Models;

namespace Api.Services;

public interface IClientsService
{
    public Task<Client> AddClient(Client client);

    public Task<Enums.OperationResult> EditClient(Client client);

    public Task<Enums.OperationResult> DeleteClient(int id);
    
    public Task<ICollection<Client>> GetAllClients();
    
    public Task<Client> GetClientById(int id);
}