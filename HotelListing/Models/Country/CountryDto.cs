﻿using HotelListing.Models.Hotels;

namespace HotelListing.Models.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }
        public List<HotelDto> Hotels { get; set; }
    }

}


