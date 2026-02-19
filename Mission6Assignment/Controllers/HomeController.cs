using Microsoft.AspNetCore.Mvc;
using Mission06_Fawson.Models;
using Mission6Assignment.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Mission6Assignment.Controllers
{
    public class HomeController : Controller
    {
        // This is our private variable that holds the database context. 
        // It's how the controller talks to the SQLite database.
        private MovieContext _context;

        // Constructor: ASP.NET Core uses "Dependency Injection" here. 
        // When the app runs, it automatically builds the MovieContext and hands it to this controller via the 'temp' variable.
        public HomeController(MovieContext temp)
        {
            _context = temp;
        }

        // --- BASIC VIEWS ---

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        // --- CREATE (ADD RECORD) ---

        // [HttpGet] means this method only runs when a user clicks a link or types the URL to visit the page.
        // It is responsible for SHOWING the blank form.
        [HttpGet]
        public IActionResult EnterMovie()
        {
            // ViewBag is a dynamic object that lets us pass data from the Controller to the View without using the strict @model.
            // We use it here to pass the list of categories so the dropdown menu can populate.
            ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();

            // We pass a new, blank Movie object to the view to avoid null reference errors on the form fields.
            return View(new Movie());
        }

        // [HttpPost] means this method runs when the user clicks the "Submit" button on the form.
        // It receives the data the user typed in, packaged neatly into the 'response' Movie object (Model Binding).
        [HttpPost]
        public IActionResult EnterMovie(Movie response)
        {
            // ModelState.IsValid checks if all the [Required] and [Range] rules we put in Movie.cs were followed.
            if (ModelState.IsValid)
            {
                _context.Movies.Add(response); // Queues up the insert command
                _context.SaveChanges(); // Actually executes the SQL to save to the database

                return View("Confirmation", response);
            }
            else
            {
                // If the user forgot a required field, the form is invalid.
                // We must re-fetch the categories because the page has to reload to show them the red error messages.
                ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
                return View(response);
            }
        }

        // --- READ (VIEW RECORDS) ---

        [HttpGet]
        public IActionResult MovieList()
        {
            // We use LINQ here to query the database.
            // The .Include() acts as a SQL JOIN, fetching the matching Category record for each Movie so we can display the text name instead of just the ID number.
            var movies = _context.Movies
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);
        }

        // --- UPDATE (EDIT RECORD) ---

        // The URL for this will look like /Home/Edit/5 (where 5 is the id).
        // MVC automatically grabs that '5' from the URL and passes it into this method.
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // .Single() looks through the Movies table and finds the one record where the IDs match.
            var recordToEdit = _context.Movies.Single(x => x.MovieId == id);

            // We still need to pass the categories to the view so the dropdown populates!
            ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();

            // Pass the specific movie data to the view so the form fields are pre-filled.
            return View(recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie updatedInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(updatedInfo); // Tells EF Core that this record has been modified
                _context.SaveChanges(); // Commits the changes to SQLite

                // Instead of showing a confirmation page, we redirect them back to the full list to see their changes.
                return RedirectToAction("MovieList");
            }
            else
            {
                // If there are validation errors, reload the dropdown and stay on the page to show errors.
                ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
                return View(updatedInfo);
            }
        }

        // --- DELETE (REMOVE RECORD) ---

        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Find the specific record the user clicked 'Delete' on
            var recordToDelete = _context.Movies.Single(x => x.MovieId == id);

            // Pass it to the view so we can ask "Are you sure you want to delete [Title]?"
            return View(recordToDelete);
        }

        // We use 'application' here as the variable name, but it represents the Movie object being submitted from the hidden field in the Delete form.
        [HttpPost]
        public IActionResult Delete(Movie application)
        {
            _context.Movies.Remove(application); // Queues up the DELETE SQL command
            _context.SaveChanges(); // Executes the deletion

            return RedirectToAction("MovieList"); // Send them back to the list
        }
    }
}