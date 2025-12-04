using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventOrganiserBorromeoBellen.Data;
using EventOrganizer.Models;

namespace EventOrganiserBorromeoBellen.Controllers
{
    public class EventDetailsController : Controller
    {
        private readonly EventOrganiserBorromeoBellenContext _context;

        public EventDetailsController(EventOrganiserBorromeoBellenContext context)
        {
            _context = context;
        }

        // GET: EventDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventDetails.ToListAsync());
        }

        // GET: EventDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventDetails
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        // GET: EventDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,Description,EventDate,EventTime,Location,Capacity")] EventDetails eventDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventDetails);
        }

        // GET: EventDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventDetails.FindAsync(id);
            if (eventDetails == null)
            {
                return NotFound();
            }
            return View(eventDetails);
        }

        // POST: EventDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,Description,EventDate,EventTime,Location,Capacity")] EventDetails eventDetails)
        {
            if (id != eventDetails.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventDetailsExists(eventDetails.EventId))
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
            return View(eventDetails);
        }

        // GET: EventDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.EventDetails
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        // POST: EventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventDetails = await _context.EventDetails.FindAsync(id);
            if (eventDetails != null)
            {
                _context.EventDetails.Remove(eventDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventDetailsExists(int id)
        {
            return _context.EventDetails.Any(e => e.EventId == id);
        }
    }
}
