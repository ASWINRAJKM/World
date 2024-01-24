using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorldAPI.DTO.States;
using WorldAPI.Models;
using WorldAPI.Repository.IRepository;

namespace WorldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAllAsync()
        {
            var states = await _statesRepository.GetAll();

            var stateDto = _mapper.Map<List<StatesDto>>(states);

            if (states == null)
            {
                return NoContent();
            }
            return Ok(stateDto);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {
            var states = await _statesRepository.GetById(id);

            var stateDto = _mapper.Map<StatesDto>(states);

            if (states == null)
            {
                return NoContent();
            }
            return Ok(stateDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto statesDto)
        {
            var result = _statesRepository.IsStateExists(statesDto.Name);
            if (result)
            {
                return Conflict("States already exists in Database");
            }

            var states = _mapper.Map<States>(statesDto);

            await _statesRepository.Create(states);
            return CreatedAtAction("GetById", new { id = states.Id }, states);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UpdateStatesDto>> Update(int id, [FromBody] UpdateStatesDto statesDto)
        {
            if (statesDto == null || id != statesDto.Id)
            {
                return BadRequest();
            }

            var states = _mapper.Map<States>(statesDto);

            await _statesRepository.Update(states);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var states = await _statesRepository.GetById(id);
            if (states == null)
            {
                return NotFound();
            }
            await _statesRepository.Delete(states);
            return NoContent();
        }
    }
}
