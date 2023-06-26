using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//Mapper.CreateMap<Customer, CustomerDto>()
			//	.ForMember(c => c.Id, opt => opt.Ignore()).ReverseMap();
			Mapper.CreateMap<Customer, CustomerDto>().ReverseMap();
			Mapper.CreateMap<Movie, MovieDto>().ReverseMap();
			//Mapper.CreateMap<Movie, MovieDto>()
			//	.ForMember(m => m.Id, opt => opt.Ignore()).ReverseMap();
		}
	}
}