using InternalBookingApp.Data;
using InternalBookingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternalBookingApp.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _context;

        public ResourceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<Resource> GetResourceByIdAsync(int id)
        {
            return await _context.Resources.FindAsync(id) ?? throw new Exception("Resource not found");
        }

        public async Task CreateResourceAsync(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResourceAsync(Resource resource)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResourceAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null) throw new Exception("Resource not found");
            if (await _context.Bookings.AnyAsync(b => b.ResourceId == id))
                throw new Exception("Cannot delete resource with active bookings");
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }
}