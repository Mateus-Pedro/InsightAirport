using InsightAirport.Enums;
using InsightAirport.Models;

namespace InsightAirport.Repositories.Interfaces
{
    /// <summary>
    /// Interface for the airplane repository, defining methods for managing airplane data.
    /// </summary>
    public interface IAirplaneRepository
    {
        /// <summary>
        /// Retrieves a list of airplanes scheduled for the current day.
        /// </summary>
        /// <returns>returning a List of AirplaneModel.</returns>
        Task<List<AirplaneModel>> GetAllTodayAirplanes();

        /// <summary>
        /// Retrieves an airplane by its ID.
        /// </summary>
        /// <param name="id">The ID of the airplane to retrieve.</param>
        /// <returns>returning the AirplaneModel.</returns>
        Task<AirplaneModel?> GetAirplaneById(int id);

        /// <summary>
        /// Retrieves a list of airplanes currently on the runway.
        /// </summary>
        /// <returns>returning a List of AirplaneModel.</returns>
        Task<List<AirplaneModel>> GetAirplanesOnRunway();

        /// <summary>
        /// Adds a new airplane to the repository.
        /// </summary>
        /// <param name="AirplaneModel">The AirplaneModel to be added.</param>
        /// <returns>returning the added AirplaneModel.</returns>
        Task<AirplaneModel> Add(AirplaneModel AirplaneModel);

        /// <summary>
        /// Updates the flight status of an airplane by its ID.
        /// </summary>
        /// <param name="status">The new flight status.</param>
        /// <param name="id">The ID of the airplane to update.</param>
        /// <returns>returning the updated AirplaneModel.</returns>
        Task<AirplaneModel> UpdateAirplaneStatus(FlightStatusEnum status, int id);
    }
}
