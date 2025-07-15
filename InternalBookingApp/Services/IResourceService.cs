using InternalBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternalBookingApp.Services
{
    public interface IResourceService
    {
        Task<List<Resource>> GetAllResourcesAsync();
        Task<Resource> GetResourceByIdAsync(int id);
        Task CreateResourceAsync(Resource resource);
        Task UpdateResourceAsync(Resource resource);
        Task DeleteResourceAsync(int id);
    }
}