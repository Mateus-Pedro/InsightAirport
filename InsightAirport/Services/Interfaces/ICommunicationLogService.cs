using InsightAirport.Models;

namespace InsightAirport.Services.Interfaces
{
    /// <summary>
    /// Interface for communication log-related operations.
    /// </summary>
    public interface ICommunicationLogService
    {
        /// <summary>
        /// Retrieves a list of all CommunicationLogs.
        /// </summary>
        /// <returns>A list of CommunicationLogModel instances.</returns>
        Task<List<CommunicationLogModel>> GetAllCommunicationLogs();
    }
}
