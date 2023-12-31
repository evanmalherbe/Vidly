﻿using System;
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

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult New(int? id)
		{
			if (id == null)
			{
				var genres = _context.Genres.ToList();
				var viewModel = new NewMovieViewModel
				{
					Genres = genres
				};

				return View(viewModel);
			}
			else
			{
				var movieToUpdate = _context.Movies.SingleOrDefault(m => m.Id == id);
				var genres = _context.Genres.ToList();
				var viewModel = new NewMovieViewModel(movieToUpdate)
				{
					Genres = genres
				};

				return View(viewModel);
			}
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new NewMovieViewModel(movie)
				{
					Genres = _context.Genres
				};

				return View("new", viewModel);
			}

			if (movie.Id == 0)
			{
				movie.DateAdded = DateTime.Now;
				_context.Movies.Add(movie);
			}
			else
			{
				var oldMovie = _context.Movies.Single(c => c.Id == movie.Id);

				oldMovie.Name = movie.Name;
				oldMovie.ReleaseDate = movie.ReleaseDate;
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
			Movie movie = new Movie() { Name = "Shrek!" };
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
		}

		[Authorize(Roles = RoleName.CanManageMovies)]
		public ActionResult Edit(int id)
		{
			var movieToUpdate = _context.Movies.SingleOrDefault(m => m.Id == id);
			if (movieToUpdate == null)
			{
				return RedirectToAction("Index", "Movies");
			}

			var viewModel = new NewMovieViewModel(movieToUpdate)
			{
				Genres = _context.Genres.ToList(),
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

			if (User.IsInRole(RoleName.CanManageMovies))
			{
					return View("Index", viewModel);
			}
		
			return View("ReadOnlyList", viewModel);
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