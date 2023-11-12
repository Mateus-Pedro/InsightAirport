using InsightAirport.Models;

namespace InsightAirport.Dtos
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for Pilot information.
    /// </summary>
    public class PilotDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the pilot.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the pilot.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the pilot.
        /// </summary>
        public DateTime DateBirth { get; set; }

        /// <summary>
        /// Gets or sets the associated airplane information for the pilot.
        /// </summary>
        public AirplaneDto? Airplane { get; set; }
    }

}