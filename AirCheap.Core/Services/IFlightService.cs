using AirCheap.Core.Models;

namespace AirCheap.Core.Services;

public interface IFlightService
{
    IEnumerable<Flight> SearchFlights(FlightsGet flightGet);
}