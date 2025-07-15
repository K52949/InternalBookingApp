using InternalBookingApp.Data;
using InternalBookingApp.Models;
using InternalBookingApp.Services;
using InternalBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InternalBookingApp.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IResourceService _resourceService;
        private readonly ApplicationDbContext _context;

        public BookingsController(IBookingService bookingService, IResourceService resourceService, ApplicationDbContext context)
        {
            _bookingService = bookingService;
            _resourceService = resourceService;
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(bookings);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new BookingViewModel
            {
                ResourceList = new SelectList(await _resourceService.GetAllResourcesAsync(), "Id", "Name")
            };
            return View(viewModel);
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var booking = new Booking
                    {
                        ResourceId = viewModel.ResourceId,
                        StartTime = viewModel.StartTime,
                        EndTime = viewModel.EndTime,
                        BookedBy = viewModel.BookedBy,
                        Purpose = viewModel.Purpose
                    };
                    await _bookingService.CreateBookingAsync(booking);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            viewModel.ResourceList = new SelectList(await _resourceService.GetAllResourcesAsync(), "Id", "Name", viewModel.ResourceId);
            return View(viewModel);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null) return NotFound();
                var viewModel = new BookingViewModel
                {
                    Id = booking.Id,
                    ResourceId = booking.ResourceId,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    BookedBy = booking.BookedBy,
                    Purpose = booking.Purpose,
                    ResourceList = new SelectList(await _resourceService.GetAllResourcesAsync(), "Id", "Name", booking.ResourceId)
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var booking = await _context.Bookings.FindAsync(id);
                    if (booking == null) return NotFound();

                    booking.ResourceId = viewModel.ResourceId;
                    booking.StartTime = viewModel.StartTime;
                    booking.EndTime = viewModel.EndTime;
                    booking.BookedBy = viewModel.BookedBy;
                    booking.Purpose = viewModel.Purpose;

                    await _bookingService.UpdateBookingAsync(booking); // Use new UpdateBookingAsync method
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            viewModel.ResourceList = new SelectList(await _resourceService.GetAllResourcesAsync(), "Id", "Name", viewModel.ResourceId);
            return View(viewModel);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var booking = await _context.Bookings.Include(b => b.Resource).FirstOrDefaultAsync(b => b.Id == id);
                if (booking == null) return NotFound();
                return View(booking);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking != null)
                {
                    _context.Bookings.Remove(booking);
                    await _context.SaveChangesAsync();
                }
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