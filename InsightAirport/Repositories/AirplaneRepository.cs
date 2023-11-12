using InsightAirport.Data;
using InsightAirport.Enums;
using InsightAirport.Models;
using InsightAirport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport.Repositories
{
    /// <summary>
    /// Repository class for managing airplane data in the database.
    /// </summary>
    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly InsightAirportDBContext _dbContext;
        private readonly DateTime _today = DateTime.Today;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneRepository"/> class.
        /// </summary>
        /// <param name="insightAirportDBContext">The database context for interacting with the database.</param>
        public AirplaneRepository(InsightAirportDBContext insightAirportDBContext)
        {
            _dbContext = insightAirportDBContext ?? throw new ArgumentNullException(nameof(insightAirportDBContext));
        }

        /// <summary>
        /// Retrieves a list of airplanes scheduled for the current day.
        /// </summary>
        /// <returns>returning a List of AirplaneModel.</returns>
        public async Task<List<AirplaneModel>> GetAllTodayAirplanes() => await _dbContext.Airplanes
                                                                                           .Where(x => x.ArrivalTime.Date == _today
                                                                                                    && x.FlightStatus != FlightStatusEnum.EndOfRoute)
                                                                                           .OrderBy(x => x.DepartureTime)
                                                                                           .Include(x => x.Pilots)
                                                                                           .ToListAsync();

        /// <summary>
        /// Retrieves an airplane by its ID.
        /// </summary>
        /// <param name="id">The ID of the airplane to retrieve.</param>
        /// <returns>returning the AirplaneModel.</returns>
        public async Task<AirplaneModel?> GetAirplaneById(int id) => await _dbContext.Airplanes.Include(x => x.Pilots).FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Retrieves a list of airplanes currently on the runway.
        /// </summary>
        /// <returns>returning a List of AirplaneModel.</returns>
        public async Task<List<AirplaneModel>> GetAirplanesOnRunway() => await _dbContext.Airplanes
                                                                                           .Where(x => x.ArrivalTime.Date == _today
                                                                                                    && x.FlightStatus == FlightStatusEnum.OnRunway)
                                                                                           .Include(x => x.Pilots)
                                                                                           .ToListAsync();

        /// <summary>
        /// Adds a new airplane to the repository.
        /// </summary>
        /// <param name="AirplaneModel">The AirplaneModel to be added.</param>
        /// <returns>returning the added AirplaneModel.</returns>
        public async Task<AirplaneModel> Add(AirplaneModel AirplaneModel)
        {
            await _dbContext.Airplanes.AddAsync(AirplaneModel);
            await _dbContext.SaveChangesAsync();
            return AirplaneModel;
        }

        /// <summary>
        /// Updates the flight status of an airplane by its ID.
        /// </summary>
        /// <param name="status">The new flight status.</param>
        /// <param name="id">The ID of the airplane to update.</param>
        /// <returns>returning the updated AirplaneModel.</returns>
        public async Task<AirplaneModel> UpdateAirplaneStatus(FlightStatusEnum status, int id)
        {
            AirplaneModel Airplane = await GetAirplaneById(id) ?? throw new Exception($"ID Airplane: {id} not found");

            Airplane.FlightStatus = status;

            _dbContext.Airplanes.Update(Airplane);
            await _dbContext.SaveChangesAsync();
            return Airplane;
        }
    }
}
