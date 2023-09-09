using HotelListing.Core.Models.Country;
using HotelListing.Data;

namespace HotelListing.Core.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<CountryDto> GetDetails(int id );

    }
}
