using InternalBookingApp.Models;
using InternalBookingApp.Services;
using InternalBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternalBookingApp.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IBookingService _bookingService;

        public ResourcesController(IResourceService resourceService, IBookingService bookingService)
        {
            _resourceService = resourceService;
            _bookingService = bookingService;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var resources = await _resourceService.GetAllResourcesAsync();
            return View(resources);
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var resource = await _resourceService.GetResourceByIdAsync(id);
                var viewModel = new ResourceDetailsViewModel
                {
                    Id = resource.Id,
                    Name = resource.Name,
                    Description = resource.Description,
                    Location = resource.Location,
                    Capacity = resource.Capacity,
                    IsAvailable = resource.IsAvailable,
                    Bookings = await _bookingService.GetBookingsByResourceIdAsync(id)
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resource resource)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _resourceService.CreateResourceAsync(resource);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(resource);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var resource = await _resourceService.GetResourceByIdAsync(id);
                return View(resource);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Resource resource)
        {
            if (id != resource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _resourceService.UpdateResourceAsync(resource);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(resource);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resource = await _resourceService.GetResourceByIdAsync(id);
                return View(resource);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _resourceService.DeleteResourceAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}