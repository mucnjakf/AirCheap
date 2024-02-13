namespace AirCheap.Core.Models;

public class FlightsGet
{
    public string OriginLocationCode { get; set; }

    public string DestinationLocationCode { get; set; }

    public DateTime DepartureDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public int Adults { get; set; }

    public string CurrencyCode { get; set; }
}