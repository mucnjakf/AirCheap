using AirCheap.Server.DataTransferObjects;
using FluentValidation;
using FluentValidation.Results;

namespace AirCheap.Server.Validation;

public class FlightDtoValidationService : IFlightDtoValidationService
{
    private readonly IValidator<FlightGetDto> _flightGetDtoValidator;

    public FlightDtoValidationService(IValidator<FlightGetDto> flightGetDtoValidator)
    {
        _flightGetDtoValidator = flightGetDtoValidator ?? throw new ArgumentNullException(nameof(flightGetDtoValidator));
    }

    public async Task<Tuple<bool, IEnumerable<string>>> ValidateFlightGetDtoAsync(FlightGetDto flightGetDto)
    {
        ValidationResult result = await _flightGetDtoValidator.ValidateAsync(flightGetDto);

        return result.IsValid
            ? new Tuple<bool, IEnumerable<string>>(true, null)
            : new Tuple<bool, IEnumerable<string>>(false, result.Errors.Select(error => error.ErrorMessage));
    }
}
