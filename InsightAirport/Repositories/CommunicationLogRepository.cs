using InsightAirport.Data;
using InsightAirport.Models;
using InsightAirport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport.Repositories
{
    /// <summary>
    /// Repository class for managing communication log data in the database.
    /// </summary>
    public class CommunicationLogRepository : ICommunicationLogRepository
    {
        private readonly InsightAirportDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationLogRepository"/> class.
        /// </summary>
        /// <param name="insightAirportDBContext">The database context for interacting with the database.</param>
        public CommunicationLogRepository(InsightAirportDBContext insightAirportDBContext)
        {
            _dbContext = insightAirportDBContext ?? throw new ArgumentNullException(nameof(insightAirportDBContext));
        }

        /// <summary>
        /// Retrieves a list of all Communications Logs available in the system.
        /// </summary>
        /// <returns>A list of CommunicationLogModel representing the details of all Communications Logs.</returns>
        public async Task<List<CommunicationLogModel>> GetAllCommunicationLogs()
        {
            return await _dbContext.CommunicationLogs.OrderByDescending(x => x.Timestamp).ToListAsync();
        }

        /// <summary>
        /// Adds a new communication log entry to the repository.
        /// </summary>
        /// <param name="CommunicationLogModel">The CommunicationLogModel to be added.</param>
        public async Task Add(CommunicationLogModel CommunicationLogModel)
        {
            await _dbContext.CommunicationLogs.AddAsync(CommunicationLogModel);
            await _dbContext.SaveChangesAsync();
        }

    }
}
