﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }

		[Required]
		[Display(Name = "Date Added")]
		public DateTime DateAdded { get; set; }

		[Required]
		[Range(0, 20)]
		[Display(Name = "Number in Stock")]
		public byte NumberInStock { get; set; }

		[Required]
		[Display(Name = "Number Available")]
		public int NumberAvailable { get; set; }

		[Required]
		[Display(Name = "Genre")]
		public int GenreId { get; set; }

		public Genre Genre { get; set; }
	}
}