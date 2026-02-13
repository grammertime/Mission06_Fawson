using Microsoft.AspNetCore.Mvc;
using Mission06_Fawson.Models;
using Mission6Assignment.Models;
using System.Diagnostics;

namespace Mission6Assignment.Controllers
{
    public class HomeController : Controller
    {

        private MovieContext _context;

        public HomeController(MovieContext temp) // Constructor
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EnterMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response); // Add record to the database
                _context.SaveChanges(); // Commit changes

                return View("Confirmation", response); // Show a confirmation view
            }
            else // If data is invalid
            {
                return View(response); // Show the form again with error messages
            }
        }
    }
}
