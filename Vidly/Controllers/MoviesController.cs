using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
      private ApplicationDbContext _context;

      public MoviesController()
      {
        _context = new ApplicationDbContext();
      }

      protected override void Dispose(bool disposing)
      {
        _context.Dispose();
      }

      public ActionResult New(int? id)
      {
        if (id == null)
        {
          var genres = _context.Genres.ToList();
          var viewModel = new NewMovieViewModel
          {
            Movie = new Movie(),
            Genres = genres
          };

          return View(viewModel); 
        }
        else
        {
          var movieToUpdate = _context.Movies.SingleOrDefault(m => m.Id == id);
          var genres = _context.Genres.ToList();
          var viewModel = new NewMovieViewModel
          {
            Genres = genres,
            Movie = movieToUpdate
          };

          return View(viewModel); 
        }
      }

      [HttpPost]
      public ActionResult Save(Movie movie)
      {
        if (movie.Id == 0)
          _context.Movies.Add(movie);
        else
        {
          var oldMovie = _context.Movies.Single(c => c.Id == movie.Id);

          oldMovie.Name = movie.Name;
          oldMovie.DateAdded = movie.DateAdded;
          oldMovie.NumberInStock = movie.NumberInStock;
          oldMovie.GenreId = movie.GenreId;
        }
      
        _context.SaveChanges();

        return RedirectToAction("Index", "Movies");
      }
        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movie = new Movie() {Name = "Shrek!" };
            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;

            var customers = new List<Customer>
            {
              new Customer {Name = "Customer 1"},
              new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
              Movie = movie,
              Customers = customers
            };

            return View(viewModel);
            //return Content("Hello world!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
        }

        public ActionResult Edit(int id)
        {
          var movieToUpdate = _context.Movies.SingleOrDefault(m => m.Id == id);
          if (movieToUpdate == null)
          {
            return RedirectToAction("Index", "Movies");
          }

          var genres = _context.Genres.ToList();

          var viewModel = new NewMovieViewModel
          {
            Genres = genres,
            Movie = movieToUpdate
          };

          return View(viewModel);
        }

        public ActionResult Index()
        {
          var movies = _context.Movies.Include(g => g.Genre).ToList();

          var viewModel = new MovieViewModel
          {
            Movie = new Movie(),
            Movies = movies
          };

          return View(viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {

          return Content(year + "/" + month);
        }

        // GET: /movies/details/1
        public ActionResult Details(int id)
        {
          if (id == 0)
          {
             return HttpNotFound();
          }

          Movie movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

          if (movie == null)
          {
            return HttpNotFound();
          }

          return View(movie);
        }
    }
}