using AirCheap.Core.Models;
using AirCheap.DAL.Entities;
using AirCheap.Server.DataTransferObjects;
using AutoMapper;

namespace AirCheap.Server;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAuthenticationDto, UserAuthenticate>();
        CreateMap<UserInsertDto, UserInsert>();
        CreateMap<UserUpdateDto, UserUpdate>();
        CreateMap<UserUpdateUsernameDto, UserUpdateUsername>();
        CreateMap<UserUpdatePasswordDto, UserUpdatePassword>();
        CreateMap<FlightGetDto, FlightsGet>();

        CreateMap<UserInsert, UserEntity>();

        CreateMap<UserEntity, UserDetails>();
    }
}
