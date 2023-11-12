using InsightAirport.Models;
using InsightAirport.Repositories.Interfaces;
using InsightAirport.Services.Interfaces;

namespace InsightAirport.Services
{
    /// <summary>
    /// Service class for managing pilots in the airport system.
    /// </summary>
    public class PilotService : IPilotService
    {
        private readonly IPilotRepository _PilotRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PilotService"/> class.
        /// </summary>
        /// <param name="PilotRepository">The repository for pilot operations.</param>
        public PilotService(IPilotRepository PilotRepository)
        {
            _PilotRepository = PilotRepository ?? throw new ArgumentNullException(nameof(PilotRepository));
        }

        /// <summary>
        /// Retrieves a list of all Pilots.
        /// </summary>
        /// <returns>A list of PilotModel instances.</returns>
        public async Task<List<PilotModel>> GetAllPilots() => await _PilotRepository.GetAllPilots();

        /// <summary>
        /// Retrieves a Pilot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Pilot.</param>
        /// <returns>A PilotModel instance if found, or null if not found.</returns>
        public async Task<PilotModel?> GetPilotById(int id) => await _PilotRepository.GetPilotById(id);

        /// <summary>
        /// Adds a new Pilot to the system.
        /// </summary>
        /// <param name="pilotModel">The PilotModel representing the details of the Pilot to be added.</param>
        /// <returns>The added PilotModel with its updated information.</returns>
        public async Task<PilotModel> Add(PilotModel pilotModel) => await _PilotRepository.Add(pilotModel);

        /// <summary>
        /// Updates the details of a Pilot identified by the specified ID.
        /// </summary>
        /// <param name="pilotModel">The PilotModel containing the updated details.</param>
        /// <param name="id">The ID of the Pilot to be updated.</param>
        /// <returns>The PilotModel representing the updated Pilot.</returns>
        public async Task<PilotModel> Update(PilotModel pilotModel, int id) => await _PilotRepository.Update(pilotModel, id);

        /// <summary>
        /// Removes a pilot from the associated airplane if the airplane is in the 'RequestingExit' flight status.
        /// </summary>
        /// <param name="id">The ID of the pilot to be removed.</param>
        /// <returns>Returns the updated PilotModel after removing the pilot from the airplane.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the pilot cannot be removed because the associated airplane has already departed on its route.</exception>
        public async Task<PilotModel> RemovePilotFromAirplane(int id)
        {
            PilotModel? pilotModel = await _PilotRepository.GetPilotById(id);

            if (pilotModel != null && pilotModel.Airplane != null)
            {
                if (pilotModel.Airplane.FlightStatus != Enums.FlightStatusEnum.RequestingExit)
                {
                    throw new InvalidOperationException("Cannot remove the pilot from the airplane after the airplane has already departed on its route.");
                }
            }

            return await _PilotRepository.RemovePilotFromAirplane(id);
        }

        /// <summary>
        /// Associates an airplane with a pilot.
        /// </summary>
        /// <param name="pilotId">The ID of the pilot.</param>
        /// <param name="airplaneId">The ID of the airplane to be associated with the pilot.</param>
        /// <returns>Returns the updated PilotModel after associating the airplane with the pilot.</returns>
        public async Task<PilotModel> AddAirplaneToPilot(int pilotId, int airplaneId) => await _PilotRepository.AddAirplaneToPilot(pilotId, airplaneId);

        /// <summary>
        /// Deletes a pilot with the specified ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the pilot to be deleted.</param>
        /// <returns>Returns a boolean indicating the success of the deletion operation.</returns>
        public async Task<bool> Delete(int id)
        {
            PilotModel? pilotModel = await _PilotRepository.GetPilotById(id);

            if (pilotModel != null && pilotModel.Airplane != null)
            {
                if (pilotModel.Airplane.FlightStatus != Enums.FlightStatusEnum.RequestingExit)
                {
                    throw new InvalidOperationException("Cannot remove the pilot from the airplane after the airplane has already departed on its route.");
                }
            }

            return await _PilotRepository.Delete(id);
        }
    }
}
