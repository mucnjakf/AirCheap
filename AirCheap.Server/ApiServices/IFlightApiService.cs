using AirCheap.Core.Models;
using AirCheap.Server.DataTransferObjects;

namespace AirCheap.Server.ApiServices;

public interface IFlightApiService
{
    Task<ResultResponseDto<Flight>> SearchFlightsAsync(FlightGetDto flightGetDto);
}