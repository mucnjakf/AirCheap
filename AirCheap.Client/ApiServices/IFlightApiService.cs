using AirCheap.Client.DataTransferObjects;
using AirCheap.Client.Models;

namespace AirCheap.Client.ApiServices;

public interface IFlightApiService
{
    Task<ResultResponseDto<Flight>> SearchFlightsAsync(FlightGetDto flightGetDto);
}