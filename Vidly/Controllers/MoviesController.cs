using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            return View(movie);

            //Types of action views:
            //return Content("hello world");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page =1, sortBy = "name}); //get calls to the new part of arg 
        }
        
        //attribute route example:
        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        // GET: Movies/Edit
        public ActionResult Edit(int id)
        {
            return Content("id=" + id);  
        }

        //GET: Movies/Index or Movies (b/c its index type method)
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            //perimitive type like int need ? for nullable calls
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy{1}", pageIndex, sortBy));
        }
    }
}