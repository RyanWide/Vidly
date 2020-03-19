using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
            //var movies = _context.Movies.Include(m => m.Genre).ToList();
            //return View(movies);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        //// GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };
        //    var customers = new List<Customer> { 
        //        new Customer{Name = "Customer 1"}, 
        //        new Customer{Name = "Customer 2"}, 
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers,
        //    };

        //    return View(viewModel);

        //    //2ways to call views:
        //    //return View(movie); //1 where view has same name as action (in this case Random)

        //    //ViewData["Movie"] = movie; //2 but need to specify the call in view file. but very messy
        //    //ViewBag.Movie = movie;
        //    //return View();

        //    //Types of action views:
        //    //return Content("hello world");
        //    //return HttpNotFound();
        //    //return new EmptyResult();
        //    //return RedirectToAction("Index", "Home", new {page =1, sortBy = "name}); //get calls to the new part of arg 
        //}

        
        ////attribute route example:
        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

        //// GET: Movies/Edit
        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);  
        //}

        ////GET: Movies/Index or Movies (b/c its index type method)
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    //perimitive type like int need ? for nullable calls
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy{1}", pageIndex, sortBy));
        //}
    }
}