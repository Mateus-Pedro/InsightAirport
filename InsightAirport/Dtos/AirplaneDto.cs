
namespace InsightAirport.Dtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for Airplane information.
    /// </summary>
    public class AirplaneDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the airplane.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the flight status of the airplane.
        /// </summary>
        public int FlightStatus { get; set; }

        /// <summary>
        /// Gets or sets the departure time of the airplane.
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the arrival time of the airplane.
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the pilots associated with the airplane.
        /// </summary>
        public string? Pilots { get; set; }
    }
}
