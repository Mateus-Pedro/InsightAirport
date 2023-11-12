using InsightAirport.Models;

namespace InsightAirport.Services.Interfaces
{
    /// <summary>
    /// Service interface for managing airplane-related operations.
    /// </summary>
    public interface IAirplaneService
    {
        /// <summary>
        /// Initiates a takeoff request for the specified airplane.
        /// </summary>
        /// <param name="airplaneId">The ID of the airplane requesting takeoff.</param>
        /// <returns>Returns the updated information about the airplane after the takeoff request.</returns>
        /// <exception cref="Exception">Thrown if the airplane is not found or if the takeoff request is denied.</exception>
        Task<AirplaneModel> RequestTakeoff(int airplaneId);

        /// <summary>
        /// Requests landing for the specified airplane.
        /// </summary>
        /// <param name="airplaneId">The ID of the airplane.</param>
        /// <returns>The updated status of the airplane after requesting landing.</returns>
        /// <exception cref="Exception">Thrown when the airplane is not found or if the landing request is denied.</exception>
        Task<AirplaneModel> RequestLanding(int airplaneId);

        /// <summary>
        /// Retrieves a list of airplane models scheduled for the current day.
        /// </summary>
        /// <returns>A list of airplane models.</returns>
        Task<List<AirplaneModel>> GetAllTodayAirplanes();

        /// <summary>
        /// Adds an airplane model using the provided AirplaneModel instance.
        /// </summary>
        /// <param name="AirplaneModel">The AirplaneModel to be added.</param>
        /// <returns>The added AirplaneModel.</returns>
        Task<AirplaneModel> Add(AirplaneModel AirplaneModel);
    }
}
