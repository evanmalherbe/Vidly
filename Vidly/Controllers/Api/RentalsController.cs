using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers.Api
{
	public class RentalsController : ApiController
	{
		private ApplicationDbContext _context;

		public RentalsController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpPost]
		public IHttpActionResult New(NewRentalDto newRentalDto)
		{
			var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);
			if (customer == null)
			{
				return BadRequest("No such customer exists.");
			}

			var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id));

			foreach (var movie in movies)
			{
				var rental = new Rental
				{
					DateRented = DateTime.Now,
					Customer = customer,
					Movie = movie
				};

				_context.Rentals.Add(rental);
			}
			_context.SaveChanges();

			return Ok();
		}

	}
}
