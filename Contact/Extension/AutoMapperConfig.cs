using AutoMapper;
using Data.Entities;
using Models;

namespace Contact.Extension
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<AddUserDTO, User>().ReverseMap();
			CreateMap<User, UserToReturnDTO>()
				.ReverseMap();
			CreateMap<LoginDTOs, User>().ReverseMap();
		}
	}
}
