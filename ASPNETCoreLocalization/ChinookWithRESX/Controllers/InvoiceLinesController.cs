using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChinookWithRESX.Models.Chinook;

namespace ChinookWithRESX.Controllers
{
    public class InvoiceLinesController : Controller
    {
        private readonly ChinookContext _context;

        public InvoiceLinesController(ChinookContext context)
        {
            _context = context;
        }

        // GET: InvoiceLines
        public async Task<IActionResult> Index()
        {
            var chinookContext = _context.InvoiceLine.Include(i => i.Invoice).Include(i => i.Track);
            return View(await chinookContext.ToListAsync());
        }

        // GET: InvoiceLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLine
                .Include(i => i.Invoice)
                .Include(i => i.Track)
                .FirstOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // GET: InvoiceLines/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId");
            ViewData["TrackId"] = new SelectList(_context.Track, "TrackId", "Name");
            return View();
        }

        // POST: InvoiceLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceLineId,InvoiceId,TrackId,UnitPrice,Quantity")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId", invoiceLine.InvoiceId);
            ViewData["TrackId"] = new SelectList(_context.Track, "TrackId", "Name", invoiceLine.TrackId);
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLine.FindAsync(id);
            if (invoiceLine == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId", invoiceLine.InvoiceId);
            ViewData["TrackId"] = new SelectList(_context.Track, "TrackId", "Name", invoiceLine.TrackId);
            return View(invoiceLine);
        }

        // POST: InvoiceLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceLineId,InvoiceId,TrackId,UnitPrice,Quantity")] InvoiceLine invoiceLine)
        {
            if (id != invoiceLine.InvoiceLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceLineExists(invoiceLine.InvoiceLineId))
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
            ViewData["InvoiceId"] = new SelectList(_context.Invoice, "InvoiceId", "InvoiceId", invoiceLine.InvoiceId);
            ViewData["TrackId"] = new SelectList(_context.Track, "TrackId", "Name", invoiceLine.TrackId);
            return View(invoiceLine);
        }

        // GET: InvoiceLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceLine = await _context.InvoiceLine
                .Include(i => i.Invoice)
                .Include(i => i.Track)
                .FirstOrDefaultAsync(m => m.InvoiceLineId == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            return View(invoiceLine);
        }

        // POST: InvoiceLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceLine = await _context.InvoiceLine.FindAsync(id);
            _context.InvoiceLine.Remove(invoiceLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceLineExists(int id)
        {
            return _context.InvoiceLine.Any(e => e.InvoiceLineId == id);
        }
    }
}
