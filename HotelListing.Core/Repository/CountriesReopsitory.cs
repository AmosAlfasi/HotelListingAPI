using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotelListing.Core.Contracts;
using HotelListing.Core.Exceptions;
using HotelListing.Core.Models.Country;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Core.Repository
{
    public class CountriesReopsitory : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesReopsitory(HotelListingDbContext context , IMapper mapper) : base(context ,mapper)
        {
            this._context = context;
            _mapper = mapper;
        }

        public async Task<CountryDto> GetDetails(int id)
        {
            var country = await _context.Countries.Include(q => q.Hotels)
                .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if(country is null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }
            return country;
        }
    }
}
