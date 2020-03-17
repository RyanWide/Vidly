using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie{Id = 1, Name = "Shrek" },
                new Movie{Id = 2, Name = "Wall-e" },
            };
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer> { 
                new Customer{Name = "Customer 1"}, 
                new Customer{Name = "Customer 2"}, 
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers,
            };

            return View(viewModel);

            //2ways to call views:
            //return View(movie); //1 where view has same name as action (in this case Random)

            //ViewData["Movie"] = movie; //2 but need to specify the call in view file. but very messy
            //ViewBag.Movie = movie;
            //return View();

            //Types of action views:
            //return Content("hello world");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page =1, sortBy = "name}); //get calls to the new part of arg 
        }

        
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