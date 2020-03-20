using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            //defensive coding to project.  but is poluting the visibility of code
            //can use optimistic case, and suppress unnecessary edge case handling, but will be vulnerable to hackers
            //if (newRental.MovieIds.Count == 0)
            //    return BadRequest("No movie ids have ben given.");

            //var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
            //if (customer == null)
            //    return BadRequest("CustomerId is not valid.");

            //var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            //if (movies.Count != newRental.MovieIds.Count)
            //    return BadRequest("One or more movieIds are invalid");
            //foreach(var movie in movies)
            //{
            //    if (movie.NumberAvailable == 0)
            //        return BadRequest("Movie is not available");
            //    movie.NumberAvailable--;
            //    var rental = new Rental
            //    {
            //        Customer = customer,
            //        Movie = movie,
            //        DateRented = DateTime.Now
            //    };
            //    _context.Rentals.Add(rental);
            //}
            //_context.SaveChanges();
            //return Ok();



            //Optimistic coding from source:
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        } 
    }
}
