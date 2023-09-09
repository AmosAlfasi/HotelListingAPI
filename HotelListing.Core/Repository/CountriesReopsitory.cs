using AutoMapper;
using HotelListing.Core.Contracts;
using HotelListing.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Core.Repository
{
    public class CountriesReopsitory : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;
     

        public CountriesReopsitory(HotelListingDbContext context , IMapper mapper) : base(context ,mapper)
        {
            this._context = context;
         
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries.Include(q => q.Hotels)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
