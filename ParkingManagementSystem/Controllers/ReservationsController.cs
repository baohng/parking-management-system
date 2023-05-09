using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Data;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.ParkingSpace).Include(r => r.User);
            var parkingSpace = _context.ParkingSpaces;

            var viewModel = new ReservationParkingspaceViewModel()
            {
                Reservations = await applicationDbContext.ToListAsync(),
                ParkingSpaces = await parkingSpace.ToListAsync()
            };
            return View(viewModel);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.ParkingSpace)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create(int parkingSpaceId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                // User is not authenticated
                return RedirectToAction("Login", "Account");
            }

            ViewData["ParkingSpaceId"] = parkingSpaceId;
            //ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id");
            ViewData["UserId"] = user.Id;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ParkingSpaceId,ReservationStartTime,ReservationEndTime")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    // Set the user ID for the reservation
                    reservation.UserId = user.Id;

                    // Get the parking space associated with the reservation
                    var parkingSpace = await _context.ParkingSpaces.FindAsync(reservation.ParkingSpaceId);

                    if (parkingSpace != null)
                    {
                        // Update the availability status of the parking space
                        parkingSpace.AvailabilityStatus = "reserved";
                        _context.Update(parkingSpace);
                    }

                    // Add the reservation to the database and save changes
                    _context.Add(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // User is not authenticated
                    return RedirectToAction("Login", "Account");
                }
            }
            //ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id", reservation.ParkingSpaceId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id", reservation.ParkingSpaceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ParkingSpaceId,ReservationStartTime,ReservationEndTime")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id", reservation.ParkingSpaceId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.ParkingSpace)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return (_context.Reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
