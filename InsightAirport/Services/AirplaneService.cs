using InsightAirport.Models;
using InsightAirport.Repositories.Interfaces;
using InsightAirport.Services.Interfaces;

namespace InsightAirport.Services
{
    /// <summary>
    /// Service class for managing airplane-related operations.
    /// </summary>
    public class AirplaneService : IAirplaneService
    {
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly ICommunicationLogRepository _communicationLogRepository;
        private readonly IPilotRepository _pilotRepository;
        private readonly ILogger<AirplaneService> _logger;

        /// <summary>
        /// Constructor for the AirplaneService class.
        /// </summary>
        /// <param name="airplaneRepository">The repository for airplane-related operations.</param>
        /// <param name="logger">The logger for logging events.</param>
        /// <param name="communicationLogRepository">The repository for communication log-related operations.</param>
        public AirplaneService(IAirplaneRepository airplaneRepository,
                               ILogger<AirplaneService> logger,
                               ICommunicationLogRepository communicationLogRepository,
                               IPilotRepository pilotRepository)
        {
            _airplaneRepository = airplaneRepository ?? throw new ArgumentNullException(nameof(airplaneRepository));
            _communicationLogRepository = communicationLogRepository ?? throw new ArgumentNullException(nameof(communicationLogRepository));
            _pilotRepository = pilotRepository ?? throw new ArgumentNullException(nameof(pilotRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Initiates a takeoff request for the specified airplane.
        /// </summary>
        /// <param name="airplaneId">The ID of the airplane requesting takeoff.</param>
        /// <returns>Returns the updated information about the airplane after the takeoff request.</returns>
        /// <exception cref="Exception">Thrown if the airplane is not found or if the takeoff request is denied.</exception>
        public async Task<AirplaneModel> RequestTakeoff(int airplaneId)
        {
            AirplaneModel? airplane = await _airplaneRepository.GetAirplaneById(airplaneId) ?? throw new Exception($"Airplane {airplaneId} not found.");
            string pilotsNames = string.Empty;

            if (airplane.Pilots != null && airplane.Pilots.Count == 0)
            {
                string message = $"The airplane with ID  {airplane.Id}  and name  {airplane.Name} cannot request takeoff without a pilot.";
                _logger.LogInformation(message);
                await _communicationLogRepository.Add(new CommunicationLogModel(message, DateTime.Now));
                throw new Exception(message);
            }
            else
                pilotsNames = airplane.Pilots != null ? string.Join(", ", airplane.Pilots.Select(x => x.Name)) : "";

            _logger.LogInformation($"The airplane with ID  {airplane.Id}  and name  {airplane.Name} piloted by ${pilotsNames} has requested takeoff.");
            await _communicationLogRepository.Add(new CommunicationLogModel($"The airplane id: {airplane.Id} name: {airplane.Name} has requested takeoff.", DateTime.Now));

            // Check if the runway is clear and if takeoff is possible
            if (await IsNextDeparture(airplaneId))
            {
                string errorMessage = $"Takeoff request denied for airplane ID: {airplane.Id}, Name: {airplane.Name}. The runway is currently occupied. Please wait for clearance.";

                _logger.LogInformation(errorMessage);
                await _communicationLogRepository.Add(new CommunicationLogModel(errorMessage, DateTime.Now));

                throw new Exception(errorMessage);
            }


            // Update the flight status
            await _airplaneRepository.UpdateAirplaneStatus(Enums.FlightStatusEnum.OnRunway, airplane.Id);

            string messageStatus = $"The airplane with ID {airplane.Id} and name {airplane.Name} is on the runway.";

            _logger.LogInformation(messageStatus);
            await _communicationLogRepository.Add(new CommunicationLogModel(messageStatus, DateTime.Now));

            // Update the flight status
            AirplaneModel inFlight = await _airplaneRepository.UpdateAirplaneStatus(Enums.FlightStatusEnum.InFlight, airplane.Id);

            string messageInFlightStatus = $"The airplane with ID {inFlight.Id} and name {inFlight.Name} is now in flight.";

            _logger.LogInformation(messageInFlightStatus);
            await _communicationLogRepository.Add(new CommunicationLogModel(messageInFlightStatus, DateTime.Now));

            return inFlight;
        }

        /// <summary>
        /// Requests landing for the specified airplane.
        /// </summary>
        /// <param name="airplaneId">The ID of the airplane.</param>
        /// <returns>The updated status of the airplane after requesting landing.</returns>
        /// <exception cref="Exception">Thrown when the airplane is not found or if the landing request is denied.</exception>
        public async Task<AirplaneModel> RequestLanding(int airplaneId)
        {
            AirplaneModel? airplane = await _airplaneRepository.GetAirplaneById(airplaneId) ?? throw new Exception($"Airplane {airplaneId} not found.");

            _logger.LogInformation($"The airplane with ID {airplane.Id} and name {airplane.Name} has requested landing.");
            await _communicationLogRepository.Add(new CommunicationLogModel($"The airplane with ID {airplane.Id} and name {airplane.Name} has requested landing.", DateTime.Now));

            // Update the flight status to requested landing
            await _airplaneRepository.UpdateAirplaneStatus(Enums.FlightStatusEnum.OnRunway, airplane.Id);

            string messageStatus = $"The airplane with ID {airplane.Id} and name {airplane.Name} has requested landing.";
            _logger.LogInformation(messageStatus);
            await _communicationLogRepository.Add(new CommunicationLogModel(messageStatus, DateTime.Now));

            // Check if the runway is clear and if Landing is possible
            if (await IsNextDeparture(airplaneId))
            {
                string errorMessage = $"Landing request denied for airplane ID: {airplane.Id}, Name: {airplane.Name}. The runway is currently occupied. Please wait for clearance.";

                _logger.LogInformation(errorMessage);
                await _communicationLogRepository.Add(new CommunicationLogModel(errorMessage, DateTime.Now));

                throw new InvalidOperationException(errorMessage);
            }

            // Update the flight status
            await _airplaneRepository.UpdateAirplaneStatus(Enums.FlightStatusEnum.OnRunway, airplane.Id);

            string messageStatusOnRunway = $"The airplane with ID {airplane.Id} and name {airplane.Name} is on the runway.";

            _logger.LogInformation(messageStatusOnRunway);
            await _communicationLogRepository.Add(new CommunicationLogModel(messageStatusOnRunway, DateTime.Now));

            // End flight
            AirplaneModel updatedAirplane = await _airplaneRepository.UpdateAirplaneStatus(Enums.FlightStatusEnum.EndOfRoute, airplane.Id);

            string messageStatusEndOfRoute = $"The airplane with ID {airplane.Id} and name {airplane.Name} has completed its route and landed.";

            _logger.LogInformation(messageStatusEndOfRoute);
            await _communicationLogRepository.Add(new CommunicationLogModel(messageStatusEndOfRoute, DateTime.Now));

            //Remove pilot

            if (airplane.Pilots != null)
                foreach (PilotModel pilot in airplane.Pilots)
                {
                    await _pilotRepository.RemovePilotFromAirplane(pilot.Id);
                }

            return updatedAirplane;
        }

        /// <summary>
        /// Retrieves a list of airplane models scheduled for the current day.
        /// </summary>
        /// <returns>A list of airplane models.</returns>
        public async Task<List<AirplaneModel>> GetAllTodayAirplanes() => await _airplaneRepository.GetAllTodayAirplanes();

        /// <summary>
        /// Adds an airplane model using the provided AirplaneModel instance.
        /// </summary>
        /// <param name="AirplaneModel">The AirplaneModel to be added.</param>
        /// <returns>The added AirplaneModel.</returns>
        public async Task<AirplaneModel> Add(AirplaneModel AirplaneModel)
        {
            if (AirplaneModel.DepartureTime < DateTime.Today)
                throw new InvalidOperationException("Departure time must be from today onwards.");

            else if (AirplaneModel.DepartureTime > AirplaneModel.ArrivalTime)
                throw new InvalidOperationException("The departure time must be less than the arrival time.");

            return await _airplaneRepository.Add(AirplaneModel);
        }

        /// <summary>
        /// Checks if the specified airplane is the next departure based on its departure time.
        /// </summary>
        /// <param name="airplaneId">The ID of the airplane to check.</param>
        /// <returns>True if the specified airplane is the next departure; otherwise, false.</returns>
        private async Task<bool> IsNextDeparture(int airplaneId)
        {
            var targetAirplane = await _airplaneRepository.GetAirplaneById(airplaneId);

            if (targetAirplane == null) return false;

            DateTime currentTime = DateTime.Now;
            TimeSpan closestDifference = TimeSpan.MaxValue;
            bool isNextDeparture = false;

            foreach (var airplane in await GetAllTodayAirplanes())
            {
                if (airplane.DepartureTime > currentTime)
                {
                    TimeSpan timeDifference = airplane.DepartureTime - currentTime;

                    if (timeDifference < closestDifference)
                    {
                        closestDifference = timeDifference;
                        isNextDeparture = airplane.Id == airplaneId;
                    }
                }
            }

            return isNextDeparture;
        }

    }
}