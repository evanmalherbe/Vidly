using AutoMapper;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.UI;
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
		public IHttpActionResult CreateNewRental(NewRentalDto newRentalDto)
		{
			var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);
		
			var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList();

			foreach (var movie in movies)
			{
				if (movie.NumberAvailable == 0)
					return BadRequest("Movie is not available");

				movie.NumberAvailable--;
				
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
