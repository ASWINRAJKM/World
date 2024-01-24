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
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository,IMapper mapper, ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllAsync()
        {
            var countries = await _countryRepository.GetAll();

            if (countries == null)
            {
                return NoContent();
            }
            var countriesDto = _mapper.Map<List<CountryDto>>(countries);
            return Ok(countriesDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            var country = await _countryRepository.Get(id);

            if (country == null)
            {
                _logger.LogError($"Error while trying to get id {id}");
                return NoContent();
            }

            var countryDto = _mapper.Map<CountryDto>(country);
            return Ok(countryDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<CreateCountryDto>> Create([FromBody]CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsRecordExists(x=>x.Name==countryDto.Name);
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

            var country = await _countryRepository.Get(id);
            if (country == null)
            {
                return NotFound();
            }
          await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
