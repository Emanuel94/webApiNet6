using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;

namespace Asp.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Owner
            CreateMap<Owner, OwnerDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<OwnerForCreationDto, Owner>();
            CreateMap<OwnerForUpdate, Owner>();
            //User
            CreateMap<User, UserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserUpdateDto, User>();

        }
    }
}
