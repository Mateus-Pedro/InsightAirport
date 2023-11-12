using InsightAirport.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InsightAirport.Models
{
    /// <summary>
    /// Represents an airplane entity.
    /// </summary>
    public class AirplaneModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the flight status of the airplane.
        /// </summary>
        public FlightStatusEnum FlightStatus { get; set; }

        /// <summary>
        /// Gets or sets the departure time of the airplane.
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the arrival time of the airplane.
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the collection of pilots associated with the airplane.
        /// </summary>
        public virtual ICollection<PilotModel>? Pilots { get; set; }

    }
}
