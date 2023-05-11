using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Data;
using ParkingManagementSystem.Models;

namespace ParkingManagementSystem.Controllers
{
    public class ParkingSpacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpacesController(ApplicationDbContext context)
        {
            _context = context;
        }
        db dbop = new db();
        public IActionResult Report()
        {
            var document = new Document
            {
                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
            };
            var pdfpage = document.Pages.Add();
            Table table = new Table
            {
                ColumnWidths = "25% 25% 25% 25%",
                DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                Border = new BorderInfo(BorderSide.All, Color.Black),
                DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black),
            };
            TextStamp textStamp = new TextStamp("Report Chỗ đậu xe");
            textStamp.TopMargin = 20;
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Top;


            // Add header on all pages
            foreach (Page page in document.Pages)
            {
                page.AddStamp(textStamp);
            }
            DataTable dt = dbop.GetParkingSpace();
            table.ImportDataTable(dt, true, 0, 0);
            document.Pages[1].Paragraphs.Add(table);
            using (var streamout = new MemoryStream())
            {
                document.Save(streamout);
                return new FileContentResult(streamout.ToArray(), "application/pdf")
                {
                    FileDownloadName = "ParkingSpace.pdf"
                };
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.ParkingSpaces
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Location!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        // GET: ParkingSpaces
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ParkingSpaces.Include(p => p.ParkingLot);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ParkingSpaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkingSpaces == null)
            {
                return NotFound();
            }

            var parkingSpace = await _context.ParkingSpaces
                .Include(p => p.ParkingLot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Create
        public IActionResult Create()
        {
            ViewData["ParkingLotId"] = new SelectList(_context.ParkingLots, "Id", "Id");
            return View();
        }

        // POST: ParkingSpaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Location,AvailabilityStatus,ParkingLotId")] ParkingSpace parkingSpace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingSpace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParkingLotId"] = new SelectList(_context.ParkingLots, "Id", "Id", parkingSpace.ParkingLotId);
            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkingSpaces == null)
            {
                return NotFound();
            }

            var parkingSpace = await _context.ParkingSpaces.FindAsync(id);
            if (parkingSpace == null)
            {
                return NotFound();
            }
            ViewData["ParkingLotId"] = new SelectList(_context.ParkingLots, "Id", "Id", parkingSpace.ParkingLotId);
            return View(parkingSpace);
        }

        // POST: ParkingSpaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Location,AvailabilityStatus,ParkingLotId")] ParkingSpace parkingSpace)
        {
            if (id != parkingSpace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpaceExists(parkingSpace.Id))
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
            ViewData["ParkingLotId"] = new SelectList(_context.ParkingLots, "Id", "Id", parkingSpace.ParkingLotId);
            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkingSpaces == null)
            {
                return NotFound();
            }

            var parkingSpace = await _context.ParkingSpaces
                .Include(p => p.ParkingLot)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            return View(parkingSpace);
        }

        // POST: ParkingSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkingSpaces == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ParkingSpaces'  is null.");
            }
            var parkingSpace = await _context.ParkingSpaces.FindAsync(id);
            if (parkingSpace != null)
            {
                _context.ParkingSpaces.Remove(parkingSpace);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingSpaceExists(int id)
        {
          return (_context.ParkingSpaces?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
