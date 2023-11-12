using InsightAirport.Models;

namespace InsightAirport.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing pilots in the airport system.
    /// </summary>
    public interface IPilotService
    {
        /// <summary>
        /// Retrieves a list of all Pilots.
        /// </summary>
        /// <returns>A list of PilotModel instances.</returns>
        Task<List<PilotModel>> GetAllPilots();

        /// <summary>
        /// Retrieves a Pilot by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Pilot.</param>
        /// <returns>A PilotModel instance if found, or null if not found.</returns>
        Task<PilotModel?> GetPilotById(int id);

        /// <summary>
        /// Adds a new Pilot to the system.
        /// </summary>
        /// <param name="pilotModel">The PilotModel representing the details of the Pilot to be added.</param>
        /// <returns>The added PilotModel with its updated information.</returns>
        Task<PilotModel> Add(PilotModel pilotModel);

        /// <summary>
        /// Updates the details of a Pilot identified by the specified ID.
        /// </summary>
        /// <param name="pilotModel">The PilotModel containing the updated details.</param>
        /// <param name="id">The ID of the Pilot to be updated.</param>
        /// <returns>The PilotModel representing the updated Pilot.</returns>
        Task<PilotModel> Update(PilotModel pilotModel, int id);

        /// <summary>
        /// Removes a pilot from the associated airplane if the airplane is in the 'RequestingExit' flight status.
        /// </summary>
        /// <param name="id">The ID of the pilot to be removed.</param>
        /// <returns>Returns the updated PilotModel after removing the pilot from the airplane.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the pilot cannot be removed because the associated airplane has already departed on its route.</exception>
        Task<PilotModel> RemovePilotFromAirplane(int id);

        /// <summary>
        /// Associates an airplane with a pilot.
        /// </summary>
        /// <param name="pilotId">The ID of the pilot.</param>
        /// <param name="airplaneId">The ID of the airplane to be associated with the pilot.</param>
        /// <returns>Returns the updated PilotModel after associating the airplane with the pilot.</returns>
        Task<PilotModel> AddAirplaneToPilot(int pilotId, int airplaneId);

        /// <summary>
        /// Deletes a pilot with the specified ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the pilot to be deleted.</param>
        /// <returns>Returns a boolean indicating the success of the deletion operation.</returns>
        Task<bool> Delete(int id);
    }
}
