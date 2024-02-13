namespace AirCheap.Core.Models;

public class Flight
{
    public string DepartureAirport { get; set; }

    public string DestinationAirport { get; set; }

    public DateTime DepartureDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public int NumberOfTransfersDeparture { get; set; }

    public int NumberOfTransfersReturn { get; set; }

    public int NumberOfBookableSeats { get; set; }

    public string Currency { get; set; }

    public double GrandTotal { get; set; }
}
