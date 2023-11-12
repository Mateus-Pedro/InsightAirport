namespace InsightAirport.Dtos
{
    public class CommunicationLogDto
    {
        /// <summary>
        /// Id
        /// </summary>
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
