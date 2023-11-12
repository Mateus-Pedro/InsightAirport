using AutoMapper;
using InsightAirport.Dtos;
using InsightAirport.Models;
using InsightAirport.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightAirport.Controllers
{
    /// <summary>
    /// API controller for managing airplane-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AirplaneController : ControllerBase
    {
        private readonly IAirplaneService _airplaneService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the AirplaneController class.
        /// </summary>
        /// <param name="AirplaneService">The service responsible for handling airplane-related operations.</param>
        public AirplaneController(IAirplaneService AirplaneService, IMapper mapper)
        {
            _airplaneService = AirplaneService ?? throw new ArgumentNullException(nameof(AirplaneService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        /// <summary>
        /// Gets a list of airplanes scheduled for the current day.
        /// </summary>
        /// <returns>A list of AirplaneDto.</returns>
        [HttpGet("today")]
        public async Task<ActionResult<List<AirplaneDto>>> GetAllTodayAirplanes()
        {
            List<AirplaneDto> airplaneDtos = _mapper.Map<List<AirplaneDto>>(await _airplaneService.GetAllTodayAirplanes());
            return Ok(airplaneDtos);
        }


        /// <summary>
        /// Add a new airplane using the provided request body.
        /// </summary>
        /// <param name="request">The request body containing details of the new airplane.</param>
        /// <returns>An ActionResult containing the added AirplaneDto.</returns>
        [HttpPost()]
        public async Task<ActionResult<AirplaneDto>> Add([FromBody] AirplaneModel request)
        {
            if (request == null)
                return BadRequest();

            AirplaneDto newAirplane = _mapper.Map<AirplaneDto>(await _airplaneService.Add(request));
            return Ok(newAirplane);
        }

        /// <summary>
        /// Requests takeoff for the airplane with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the airplane requesting takeoff.</param>
        /// <returns>An ActionResult containing the AirplaneDto after takeoff.</returns>
        [HttpPost("takeoff/{id}")]
        public async Task<ActionResult<AirplaneDto>> RequestTakeoff(int id)
        {
            AirplaneDto airplane = _mapper.Map<AirplaneDto>(await _airplaneService.RequestTakeoff(id));
            return Ok(airplane);
        }

        /// <summary>
        /// Requests landing for the airplane with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the airplane requesting landing.</param>
        /// <returns>An ActionResult containing the AirplaneDto after landing.</returns>
        [HttpPost("landing/{id}")]
        public async Task<ActionResult<AirplaneDto>> RequestLanding(int id)
        {
            AirplaneDto airplane = _mapper.Map<AirplaneDto>(await _airplaneService.RequestLanding(id));
            return Ok(airplane);
        }

    }
}
