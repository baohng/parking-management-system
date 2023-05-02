using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Data;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Controllers
{
    public class ParkingLotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingLotsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.ParkingLots
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Name!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }
        // GET: ParkingLots
        public async Task<IActionResult> Index()
        {
              return _context.ParkingLots != null ? 
                          View(await _context.ParkingLots.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ParkingLots'  is null.");
        }

        // GET: ParkingLots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkingLots == null)
            {
                return NotFound();
            }

            var parkingLot = await _context.ParkingLots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingLot == null)
            {
                return NotFound();
            }

            return View(parkingLot);
        }

        // GET: ParkingLots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkingLots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,VehicleType,ParkingLotPrice")] ParkingLot parkingLot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingLot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingLot);
        }

        // GET: ParkingLots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkingLots == null)
            {
                return NotFound();
            }

            var parkingLot = await _context.ParkingLots.FindAsync(id);
            if (parkingLot == null)
            {
                return NotFound();
            }
            return View(parkingLot);
        }

        // POST: ParkingLots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,VehicleType,ParkingLotPrice")] ParkingLot parkingLot)
        {
            if (id != parkingLot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingLot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingLotExists(parkingLot.Id))
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
            return View(parkingLot);
        }

        // GET: ParkingLots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkingLots == null)
            {
                return NotFound();
            }

            var parkingLot = await _context.ParkingLots
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingLot == null)
            {
                return NotFound();
            }

            return View(parkingLot);
        }

        // POST: ParkingLots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkingLots == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ParkingLots'  is null.");
            }
            var parkingLot = await _context.ParkingLots.FindAsync(id);
            if (parkingLot != null)
            {
                _context.ParkingLots.Remove(parkingLot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingLotExists(int id)
        {
          return (_context.ParkingLots?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
