using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Vidly.Models;

namespace Vidly.ViewModels
{
  public class NewMovieViewModel
  {
    public IEnumerable<Genre> Genres { get; set; }
   
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Release Date")]
    public DateTime? ReleaseDate { get; set; }

    [Required]
    [Display(Name = "Date Added")]
    public DateTime? DateAdded { get; set; }

    [Required]
    [Range(0,20)]
    [Display(Name = "Number in Stock")]
    public byte? NumberInStock { get; set; }

    [Required]
    [Display(Name = "Genre")]
    public int? GenreId { get; set; }

    public NewMovieViewModel() 
    { 
      Id = 0;
    }
    public NewMovieViewModel(Movie movie)
    {
      Id = movie.Id;
      Name = movie.Name;
      DateAdded = movie.DateAdded;
      ReleaseDate = movie.ReleaseDate;
      NumberInStock = movie.NumberInStock;
      GenreId = movie.GenreId;
    }

  }
}