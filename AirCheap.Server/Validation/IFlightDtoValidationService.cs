using AirCheap.Server.DataTransferObjects;

namespace AirCheap.Server.Validation;

public interface IFlightDtoValidationService
{
    Task<Tuple<bool, IEnumerable<string>>> ValidateFlightGetDtoAsync(FlightGetDto flightGetDto);
}