using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsightAirport.Models
{
    /// <summary>
    /// Represents a communication log entity.
    /// </summary>
    public class CommunicationLogModel
    {
        /// <summary>
        /// Initializes a new instance of the CommunicationLogModel class with the specified message and timestamp.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        /// <param name="timeStamp">The timestamp of the log entry.</param>
        public CommunicationLogModel(string message, DateTime timeStamp)
        {
            if (message != null)
                this.Message = message;
            else
                throw new ArgumentNullException("Message is null");

            if (timeStamp.Date != DateTime.MinValue)
                this.Timestamp = timeStamp;
            else
                throw new ArgumentNullException("Timestamp is null");
        }

        private CommunicationLogModel() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the message to be logged.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the log entry.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
