using InsightAirport.Data;
using InsightAirport.Models;
using InsightAirport.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightAirport.Repositories
{
    /// <summary>
    /// Repository class for managing pilot data in the database.
    /// </summary>
    public class PilotRepository : IPilotRepository
    {
        private readonly InsightAirportDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PilotRepository"/> class.
        /// </summary>
        /// <param name="insightAirportDBContext">The database context for interacting with the database.</param>
        public PilotRepository(InsightAirportDBContext insightAirportDBContext)
        {
            _dbContext = insightAirportDBContext ?? throw new ArgumentNullException(nameof(insightAirportDBContext));
        }

        /// <summary>
        /// Retrieves a list of all Pilots available in the system.
        /// </summary>
        /// <returns>A List of PilotModel representing the details of all Pilots.</returns>
        public async Task<List<PilotModel>> GetAllPilots()
        {
            return await _dbContext.Pilots.Include(x => x.Airplane).ToListAsync();
        }

        /// <summary>
        /// Retrieves the details of a pilot based on its unique identifier.
        /// </summary>
        /// <param name="id">The identifier of the pilot to retrieve.</param>
        /// <returns>
        /// A PilotModel representing the details of the pilot if found; 
        /// otherwise, returns null if no pilot is found with the specified identifier.
        /// </returns>
        public async Task<PilotModel?> GetPilotById(int id) => await _dbContext.Pilots.Include(x => x.Airplane).FirstOrDefaultAsync(x => x.Id == id);

        /// <summary>
        /// Adds a new pilot to the system.
        /// </summary>
        /// <param name="PilotModel">The PilotModel representing the details of the pilot to be added.</param>
        /// <returns>The PilotModel representing the added pilot.</returns>
        public async Task<PilotModel> Add(PilotModel pilotModel)
        {
            await _dbContext.Pilots.AddAsync(pilotModel);
            await _dbContext.SaveChangesAsync();
            return pilotModel;
        }

        /// <summary>
        /// Updates the details of a pilot identified by the specified ID.
        /// </summary>
        /// <param name="PilotModel">The PilotModel containing the updated details.</param>
        /// <param name="id">The ID of the pilot to be updated.</param>
        /// <returns>The PilotModel representing the updated pilot.</returns>
        public async Task<PilotModel> Update(PilotModel pilotModel, int id)
        {
            PilotModel Pilot = await GetPilotById(id) ?? throw new Exception($"ID pilot: {id} not found");

            if (pilotModel.Name != null)
                Pilot.Name = pilotModel.Name;

            if (pilotModel.DateBirth.Date != DateTime.MinValue)
                Pilot.DateBirth = pilotModel.DateBirth;

            if (pilotModel.AirplaneId != null)
                Pilot.AirplaneId = pilotModel.AirplaneId;

            _dbContext.Pilots.Update(Pilot);
            await _dbContext.SaveChangesAsync();

            return Pilot;
        }

        /// <summary>
        /// Removes a pilot from an airplane by setting the AirplaneId to null.
        /// </summary>
        /// <param name="id">The ID of the pilot to be removed from the airplane.</param>
        /// <returns>Returns the updated PilotModel after removing the association with the airplane.</returns>
        public async Task<PilotModel> RemovePilotFromAirplane(int id)
        {
            PilotModel Pilot = await GetPilotById(id) ?? throw new Exception($"ID pilot: {id} not found");

            Pilot.AirplaneId = null;

            _dbContext.Pilots.Update(Pilot);
            await _dbContext.SaveChangesAsync();

            return Pilot;
        }

        /// <summary>
        /// Adds an airplane to a pilot by updating the AirplaneId.
        /// </summary>
        /// <param name="pilotId">The ID of the pilot to whom the airplane is to be added.</param>
        /// <param name="airplaneId">The ID of the airplane to be added to the pilot.</param>
        /// <returns>Returns the updated PilotModel after adding the airplane association.</returns>
        public async Task<PilotModel> AddAirplaneToPilot(int pilotId, int airplaneId)
        {
            PilotModel Pilot = await GetPilotById(pilotId) ?? throw new Exception($"ID pilot: {pilotId} not found");

            Pilot.AirplaneId = airplaneId;

            _dbContext.Pilots.Update(Pilot);
            await _dbContext.SaveChangesAsync();

            return Pilot;
        }

        /// <summary>
        /// Deletes a pilot based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the pilot to be deleted.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>
        public async Task<bool> Delete(int id)
        {
            PilotModel? Pilot = await GetPilotById(id) ?? throw new Exception($"ID pilot: {id} not found");

            _dbContext.Pilots.Remove(Pilot);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
