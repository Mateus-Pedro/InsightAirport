using AutoMapper;
using InsightAirport.Dtos;
using InsightAirport.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightAirport.Controllers
{
    /// <summary>
    /// API controller for managing communication log-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicationLogController : ControllerBase
    {
        private readonly ICommunicationLogService _communicationLogService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the CommunicationLogController class.
        /// </summary>
        /// <param name="communicationLogService">The service responsible for handling communication log-related operations.</param>
        public CommunicationLogController(ICommunicationLogService communicationLogService, IMapper mapper)
        {
            _communicationLogService = communicationLogService ?? throw new ArgumentNullException(nameof(communicationLogService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets a list of communication logs.
        /// </summary>
        /// <returns>An ActionResult containing a list of CommunicationLogeDto.</returns>
        [HttpGet]
        public async Task<ActionResult<List<CommunicationLogDto>>> GetAllCommunicationLogs()
        {
            List<CommunicationLogDto> CommunicationLogs = _mapper.Map<List<CommunicationLogDto>>(await _communicationLogService.GetAllCommunicationLogs());
            return Ok(CommunicationLogs);
        }
    }
}
