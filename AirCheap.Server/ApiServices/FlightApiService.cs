using AirCheap.Core.Models;
using AirCheap.Core.Services;
using AirCheap.Server.DataTransferObjects;
using AirCheap.Server.Validation;
using AutoMapper;

namespace AirCheap.Server.ApiServices;

public class FlightApiService : IFlightApiService
{
    private readonly IMapper _mapper;
    private readonly IFlightDtoValidationService _flightDtoValidationService;
    private readonly IFlightService _flightService;

    public FlightApiService(IMapper mapper, IFlightDtoValidationService flightDtoValidationService, IFlightService flightService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _flightDtoValidationService = flightDtoValidationService ?? throw new ArgumentNullException(nameof(flightDtoValidationService));
        _flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));
    }

    public async Task<ResultResponseDto<Flight>> SearchFlightsAsync(FlightGetDto flightGetDto)
    {
        (bool success, IEnumerable<string> errors) = await _flightDtoValidationService.ValidateFlightGetDtoAsync(flightGetDto);

        if (!success)
        {
            return new ResultResponseDto<Flight>
            {
                Success = false,
                Errors = errors
            };
        }

        try
        {
            FlightsGet flightsGet = _mapper.Map<FlightsGet>(flightGetDto);
            IEnumerable<Flight> flights = _flightService.SearchFlights(flightsGet);

            return new ResultResponseDto<Flight>
            {
                Success = true,
                CollectionResult = flights
            };
        }
        catch (Exception e)
        {
            return new ResultResponseDto<Flight>
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }
}
