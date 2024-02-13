using AirCheap.Core.Models;

namespace AirCheap.Core.Repositories;

public interface IFlightRepository
{
    IEnumerable<Flight> SearchFlights(FlightsGet flightGet);
}
