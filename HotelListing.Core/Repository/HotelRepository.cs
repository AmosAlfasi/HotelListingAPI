using AutoMapper;
using HotelListing.Core.Contracts;
using HotelListing.Data;

namespace HotelListing.Core.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
   
        public HotelRepository(HotelListingDbContext context ,IMapper mapper) : base(context ,mapper)
        {

        }
    }
}
