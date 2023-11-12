using InsightAirport.Models;

namespace InsightAirport.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the pilot repository, defining methods for managing pilot data.
    /// </summary>
    public interface IPilotRepository
    {
        /// <summary>
        /// Retrieves a list of all pilots.
        /// </summary>
        /// <returns>A List of PilotModel.</returns>
        Task<List<PilotModel>> GetAllPilots();

        /// <summary>
        /// Retrieves a pilot by its ID.
        /// </summary>
        /// <param name="id">The ID of the pilot to retrieve.</param>
        /// <returns>The PilotModel object.</returns>
        Task<PilotModel?> GetPilotById(int id);

        /// <summary>
        /// Adds a new pilot to the repository.
        /// </summary>
        /// <param name="pilotModel">The PilotModel to be added.</param>
        /// <returns>The added PilotModel.</returns>
        Task<PilotModel> Add(PilotModel pilotModel);

        /// <summary>
        /// Updates an existing pilot by its ID.
        /// </summary>
        /// <param name="pilotModel">The updated PilotModel.</param>
        /// <param name="id">The ID of the pilot to update.</param>
        /// <returns>The updated PilotModel.</returns>
        Task<PilotModel> Update(PilotModel pilotModel, int id);

        /// <summary>
        /// Deletes a pilot by its ID.
        /// </summary>
        /// <param name="id">The ID of the pilot to delete.</param>
        /// <returns>A boolean indicating the success of the operation.</returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Removes a pilot from an airplane by setting the AirplaneId to null.
        /// </summary>
        /// <param name="id">The ID of the pilot to be removed from the airplane.</param>
        /// <returns>Returns the updated PilotModel after removing the association with the airplane.</returns>
        Task<PilotModel> RemovePilotFromAirplane(int id);

        /// <summary>
        /// Adds an airplane to a pilot by updating the AirplaneId.
        /// </summary>
        /// <param name="pilotId">The ID of the pilot to whom the airplane is to be added.</param>
        /// <param name="airplaneId">The ID of the airplane to be added to the pilot.</param>
        /// <returns>Returns the updated PilotModel after adding the airplane association.</returns>
        Task<PilotModel> AddAirplaneToPilot(int pilotId, int airplaneId);
    }
}
