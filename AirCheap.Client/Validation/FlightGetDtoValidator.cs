using AirCheap.Client.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Client.Validation;

public class FlightGetDtoValidator : AbstractValidator<FlightGetDto>
{
    public FlightGetDtoValidator()
    {
        RuleFor(x => x.OriginLocationCode).NotEmpty().WithMessage("Origin airport is required.");

        RuleFor(x => x.DestinationLocationCode).NotEmpty().WithMessage("Destination airport is required.");

        RuleFor(x => x.DepartureDate).NotEmpty().WithMessage("Departure date is required.");

        RuleFor(x => x.ReturnDate).NotEmpty().WithMessage("Return date is required.");

        RuleFor(x => x.Adults).GreaterThan(0).WithMessage("At least one person is required.");

        RuleFor(x => x.CurrencyCode).NotEmpty().WithMessage("Currency is required.");
    }
}
