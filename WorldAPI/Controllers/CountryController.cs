using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.Data;
using WorldAPI.DTO.Country;
using WorldAPI.Models;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository,IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllAsync()
        {
            var countries = await _countryRepository.GetAll();

            var countriesDto = _mapper.Map<List<CountryDto>>(countries);

            if (countries == null)
            {
                return NoContent();
            }
            return Ok(countriesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var country = await _countryRepository.GetById(id);

            var countryDto = _mapper.Map<CountryDto>(country);

            if (country == null)
            {
                return NoContent();
            }
            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody]CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsCountryExists(countryDto.Name);
            if (result) 
            {
                return Conflict("Country already exists in Database");
            }

            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new {id=country.Id},country);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UpdateCountryDto>> Update(int id,[FromBody]UpdateCountryDto countryDto)
        {
            if (countryDto == null || id != countryDto.Id)
            {
                return BadRequest();
            }

            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var country = await _countryRepository.GetById(id);
            if (country == null)
            {
                return NotFound();
            }
          await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
