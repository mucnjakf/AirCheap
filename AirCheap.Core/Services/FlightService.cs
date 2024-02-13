using AirCheap.Core.Models;
using AirCheap.Core.Repositories;

namespace AirCheap.Core.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
    }

    public IEnumerable<Flight> SearchFlights(FlightsGet flightGet)
    {
        flightGet.OriginLocationCode = flightGet.OriginLocationCode.ToUpper();
        flightGet.DestinationLocationCode = flightGet.DestinationLocationCode.ToUpper();

        return _flightRepository.SearchFlights(flightGet).OrderBy(flight => flight.GrandTotal);
    }
}
