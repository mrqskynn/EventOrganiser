using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Models; // model types
using EventOrganiserBorromeoBellen.Data; // correct context namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventOrganiserBorromeoBellen.Controllers
{
    public class HomeController : Controller
    {
        private readonly EventOrganiserBorromeoBellenContext _context;

        public HomeController(EventOrganiserBorromeoBellenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.EventDetails
                                       .OrderBy(e => e.EventDate)
                                       .ToListAsync();

            return View(events);
        }
    }
}
