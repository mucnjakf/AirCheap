namespace AirCheap.Client.DataTransferObjects;

public class FlightGetDto
{
    public string OriginLocationCode { get; set; }

    public string DestinationLocationCode { get; set; }

    public DateTime DepartureDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public int Adults { get; set; }

    public string CurrencyCode { get; set; }
}
