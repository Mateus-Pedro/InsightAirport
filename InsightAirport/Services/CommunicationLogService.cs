using InsightAirport.Models;
using InsightAirport.Repositories.Interfaces;
using InsightAirport.Services.Interfaces;

namespace InsightAirport.Services
{
    /// <summary>
    /// Service class for communication log-related operations.
    /// </summary>
    public class CommunicationLogService : ICommunicationLogService
    {
        private readonly ICommunicationLogRepository _communicationLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationLogService"/> class.
        /// </summary>
        /// <param name="communicationLogRepository">The communication log repository.</param>
        public CommunicationLogService(ICommunicationLogRepository communicationLogRepository)
        {
            _communicationLogRepository = communicationLogRepository ?? throw new ArgumentNullException(nameof(communicationLogRepository));
        }

        /// <summary>
        /// Retrieves a list of all CommunicationLogs.
        /// </summary>
        /// <returns>A list of CommunicationLogModel instances.</returns>
        public async Task<List<CommunicationLogModel>> GetAllCommunicationLogs() => await _communicationLogRepository.GetAllCommunicationLogs();
    }
}
