using InsightAirport.Models;

namespace InsightAirport.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the communication log repository, defining methods for managing communication log data.
    /// </summary>
    public interface ICommunicationLogRepository
    {
        /// <summary>
        /// Retrieves a list of all communication logs.
        /// </summary>
        /// <returns>returning a List of CommunicationLogModel.</returns>
        Task<List<CommunicationLogModel>> GetAllCommunicationLogs();

        /// <summary>
        /// Adds a new communication log entry to the repository.
        /// </summary>
        /// <param name="CommunicationLogModel">The CommunicationLogModel to be added.</param>
        Task Add(CommunicationLogModel CommunicationLogModel);
    }
}
