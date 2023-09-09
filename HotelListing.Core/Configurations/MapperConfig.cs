using AutoMapper;
using HotelListing.Data;
using HotelListing.Core.Models.Country;
using HotelListing.Core.Models.Hotels;
using HotelListing.Core.Models.Users;
using System.Diagnostics.Metrics;

namespace HotelListing.Core.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country,GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();


            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();

            CreateMap<ApiUser, ApiUserDto>().ReverseMap();
        }


    }
}
