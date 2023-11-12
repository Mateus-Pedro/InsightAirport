using System.ComponentModel;

namespace InsightAirport.Enums
{
    /// <summary>
    /// Enumeration representing various states of a flight.
    /// </summary>
    public enum FlightStatusEnum
    {
        /// <summary>
        /// Requesting Exit
        /// </summary>
        [Description("Requesting Exit")]
        RequestingExit = 1,

        /// <summary>
        /// On Runway
        /// </summary>
        [Description("On Runway")]
        OnRunway = 2,

        /// <summary>
        /// "In Flight
        /// </summary>
        [Description("In Flight")]
        InFlight = 3,

        /// <summary>
        /// Requesting Landing
        /// </summary>
        [Description("Requesting Landing")]
        RequestingLanding = 4,

        /// <summary>
        /// Landing
        /// </summary>
        [Description("Landing")]
        Landing = 5,

        /// <summary>
        /// End of Route
        /// </summary>
        [Description("End of Route")]
        EndOfRoute = 6
    }
}
