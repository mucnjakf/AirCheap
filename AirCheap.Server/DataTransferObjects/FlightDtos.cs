namespace AirCheap.Server.DataTransferObjects;

public record FlightGetDto(
    string OriginLocationCode,
    string DestinationLocationCode,
    DateTime DepartureDate,
    DateTime ReturnDate,
    int Adults,
    string CurrencyCode
);