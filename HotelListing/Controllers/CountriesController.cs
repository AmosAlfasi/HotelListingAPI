﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Data;
using HotelListing.Core.Models.Country;
using AutoMapper;
using HotelListing.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.Core.Exceptions;
using HotelListing.Core.Models;
using Microsoft.AspNetCore.OData.Query;
using HotelListing.Core.Contracts;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CountriesController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository,ILogger<CountriesController> logger)
        {

            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
            _logger = logger;
        }

        // GET: api/Countries
        [HttpGet("GetAll")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            //Select * from Countries
            var countries = await _countriesRepository.GetAllAsync();

            var response = _mapper.Map<List<GetCountryDto>>(countries);

            return Ok(response);
        }
        // GET: api/Countries/?StartIndex=0&PageSize=25&PageNumber=1
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            //Select * from Countries
            var pagedCountriesResult = await _countriesRepository.GetallAsync<GetCountryDto>(queryParameters);       
            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            //var response = _mapper.Map<CountryDto>(country);

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            //_context.Entry(country).State = EntityState.Modified;

            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                throw new NotFoundException(nameof(PutCountry), id);
            }


            _mapper.Map(updateCountryDto, country);

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
        {
            //var countryOld = new Country
            //{
            //    Name = createCountry.Name,
            //    ShortName = createCountry.ShortName
            //};

            var country = _mapper.Map<Country>(createCountry);

            //_context.Countries.Add(country);
            //await _countriesRepository.UpdateAsync();
            await _countriesRepository.AddAsync(country);


            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                throw new NotFoundException(nameof(DeleteCountry), id);
            }

            //_context.Countries.Remove(country);
            //await _context.SaveChangesAsync();
            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
