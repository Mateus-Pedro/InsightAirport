using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsightAirport.Models
{
    /// <summary>
    /// Represents a pilot entity.
    /// </summary>
    public class PilotModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// Gets or sets the ID of the associated airplane.
        /// </summary>
        public int? AirplaneId { get; set; }

        /// <summary>
        /// Gets or sets the associated airplane.
        /// </summary>
        public virtual AirplaneModel? Airplane { get; set; }
    }
}
