using AutoMapper;
using Entity.DTOs.Users;
using Entity.DTOs;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AutoMapper.Users
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<AppUser, UserDto>().ReverseMap();
			CreateMap<AppUser, UserAddDto>().ReverseMap();
			CreateMap<AppUser, UserUpdateDto>().ReverseMap();
			CreateMap<AppUser, UserProfileDto>().ReverseMap();

		}
	}
}
