using AutoMapper;
using InsightAirport.Dtos;
using InsightAirport.Models;
using InsightAirport.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightAirport.Controllers
{
    /// <summary>
    /// API controller for managing pilot-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PilotController : ControllerBase
    {
        private readonly IPilotService _pilotService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the PilotController class.
        /// </summary>
        /// <param name="pilotService">The service responsible for handling pilot-related operations.</param>
        public PilotController(IPilotService pilotService, IMapper mapper)
        {
            _pilotService = pilotService ?? throw new ArgumentNullException(nameof(pilotService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a list of pilots.
        /// </summary>
        /// <returns>An ActionResult containing a list of PilotDto.</returns>
        [HttpGet]
        public async Task<ActionResult<List<PilotDto>>> GetAllPilots()
        {
            List<PilotDto> pilots = _mapper.Map<List<PilotDto>>(await _pilotService.GetAllPilots());
            return Ok(pilots);
        }

        /// <summary>
        /// Gets a pilot by their ID.
        /// </summary>
        /// <param name="id">The ID of the pilot to retrieve.</param>
        /// <returns>An ActionResult containing the PilotDto.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PilotDto>> GetPilotById(int id)
        {
            PilotDto? pilot = _mapper.Map<PilotDto>(await _pilotService.GetPilotById(id));
            return Ok(pilot);
        }

        /// <summary>
        /// Adds a new pilot using the provided request body.
        /// </summary>
        /// <param name="request">The request body containing details of the new pilot.</param>
        /// <returns>An ActionResult containing the added PilotDto.</returns>
        [HttpPost()]
        public async Task<ActionResult<PilotDto>> Add([FromBody] PilotModel request)
        {
            if (request == null)
                return BadRequest();

            PilotDto newPilot = _mapper.Map<PilotDto>(await _pilotService.Add(request));
            return Ok(newPilot);
        }

        /// <summary>
        /// Removes a pilot from an airplane by the specified pilot ID.
        /// </summary>
        /// <param name="id">The ID of the pilot to be removed from the airplane.</param>
        /// <returns>Returns the updated details of the affected airplane in the form of a PilotDto.</returns>
        [HttpPatch("{id}/remove-airplane")]
        public async Task<ActionResult<PilotDto>> RemovePilotFromAirplane(int id)
        {
            PilotDto updatedAirplane = _mapper.Map<PilotDto>(await _pilotService.RemovePilotFromAirplane(id));

            return Ok(updatedAirplane);
        }

        /// <summary>
        /// Adds an airplane to a pilot using the specified pilot ID and airplane ID.
        /// </summary>
        /// <param name="pilotId">The ID of the pilot to whom the airplane is to be added.</param>
        /// <param name="airplaneId">The ID of the airplane to be added to the pilot.</param>
        /// <returns>Returns the updated details of the affected pilot in the form of a PilotDto.</returns>
        [HttpPatch("{pilotId}/add-airplane/{airplaneId}")]
        public async Task<ActionResult<PilotDto>> AddAirplaneToPilot(int pilotId, int airplaneId)
        {
            PilotDto updatedAirplane = _mapper.Map<PilotDto>(await _pilotService.AddAirplaneToPilot(pilotId, airplaneId));

            return Ok(updatedAirplane);
        }

        /// <summary>
        /// Deletes a pilot with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the pilot to be deleted.</param>
        /// <returns>Returns a boolean indicating the success of the deletion operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            bool valid = await _pilotService.Delete(id);

            return Ok(valid);
        }
    }
}
