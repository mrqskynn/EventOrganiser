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
    public class ParticipantsController : Controller
    {
        private readonly EventOrganiserBorromeoBellenContext _context;

        public ParticipantsController(EventOrganiserBorromeoBellenContext context)
        {
            _context = context;
        }

        // GET: Participants
        public async Task<IActionResult> Index()
        {
            var eventOrganiserBorromeoBellenContext = _context.Participant.Include(p => p.Event);
            return View(await eventOrganiserBorromeoBellenContext.ToListAsync());
        }

        // GET: Participants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participant
                .Include(p => p.Event)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        // GET: Participants/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.EventDetails, "EventId", "EventName");
            return View();
        }

        // POST: Participants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Participant participant)
        {
            // Ensure dropdown is available if we need to re-render the view
            ViewData["EventId"] = new SelectList(_context.EventDetails, "EventId", "EventName", participant?.EventId);

            // Basic server-side fixes / normalization
            if (participant != null)
            {
                // Trim strings to avoid validation failures for whitespace
                participant.FullName = participant.FullName?.Trim();
                participant.Email = participant.Email?.Trim();
                participant.ContactNumber = participant.ContactNumber?.Trim();

                // If JoinedDate wasn't provided or model binder failed, set it to today
                // Note: clear ModelState and revalidate after normalization to handle parse errors
                if (participant.JoinedDate == default)
                {
                    participant.JoinedDate = DateTime.UtcNow.Date;
                }
                else
                {
                    participant.JoinedDate = participant.JoinedDate.Date;
                }

                // Clear previous modelstate (parsing errors) and revalidate the normalized model
                ModelState.Clear();
                TryValidateModel(participant);
            }

            if (!ModelState.IsValid)
            {
                // Build a concise list of validation errors for debugging
                var errors = ModelState.Where(kvp => kvp.Value.Errors.Count > 0)
                    .Select(kvp => new { Field = kvp.Key, Errors = kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray() })
                    .ToList();

                // Pass errors to the view in case the developer needs details (do not expose in production)
                ViewData["ValidationErrors"] = errors;

                return View(participant);
            }

            try
            {
                _context.Add(participant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Capture the exception and show an error on the form so the user knows what happened
                ModelState.AddModelError(string.Empty, "An error occurred while saving the participant: " + ex.Message);
                return View(participant);
            }
        }

        // GET: Participants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participant.FindAsync(id);
            if (participant == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.EventDetails, "EventId", "EventName", participant.EventId);
            return View(participant);
        }

        // POST: Participants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticipantId,FullName,Email,ContactNumber,EventId,JoinedDate")] Participant participant)
        {
            if (id != participant.ParticipantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipantExists(participant.ParticipantId))
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
            ViewData["EventId"] = new SelectList(_context.EventDetails, "EventId", "EventName", participant.EventId);
            return View(participant);
        }

        // GET: Participants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participant = await _context.Participant
                .Include(p => p.Event)
                .FirstOrDefaultAsync(m => m.ParticipantId == id);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participant = await _context.Participant.FindAsync(id);
            if (participant != null)
            {
                _context.Participant.Remove(participant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipantExists(int id)
        {
            return _context.Participant.Any(e => e.ParticipantId == id);
        }
    }
}
