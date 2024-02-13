using AirCheap.Core.Models;
using AirCheap.Core.Repositories;
using AirCheap.DAL.Entities;
using amadeus;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AirCheap.DAL.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly IConfiguration _configuration;

    public FlightRepository(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IEnumerable<Flight> SearchFlights(FlightsGet flightGet)
    {
        Amadeus amadeusApi = Amadeus
            .builder(_configuration["Amadeus:ClientId"], _configuration["Amadeus:ClientSecret"])
            .build();

        Response amadeusResponse = amadeusApi.get("/v2/shopping/flight-offers",
            Params.with("originLocationCode", flightGet.OriginLocationCode)
            .and("destinationLocationCode", flightGet.DestinationLocationCode)
            .and("departureDate", flightGet.DepartureDate.ToString("yyyy-MM-dd"))
            .and("returnDate", flightGet.ReturnDate.ToString("yyyy-MM-dd"))
            .and("adults", flightGet.Adults.ToString())
            .and("currencyCode", flightGet.CurrencyCode));

        RootEntity rootEntity = JsonSerializer.Deserialize<RootEntity>(amadeusResponse.body);

        List<Flight> flights = new();

        for (int i = 0; i < rootEntity.Data.Count; i++)
        {
            int destinationAirportSegmentsIndex = rootEntity.Data[i].Itineraries[0].Segments.Count - 1;
            int returnDateItinerariesIndex = rootEntity.Data[i].Itineraries.Count - 1;
            int returnDateSegmentsIndex = rootEntity.Data[i].Itineraries[returnDateItinerariesIndex].Segments.Count - 1;

            Flight flight = new()
            {
                DepartureAirport = rootEntity.Data[i].Itineraries[0].Segments[0].Departure.IataCode,
                DestinationAirport = rootEntity.Data[i].Itineraries[0].Segments[destinationAirportSegmentsIndex].Arrival.IataCode,

                DepartureDate = rootEntity.Data[i].Itineraries[0].Segments[0].Departure.At,
                ReturnDate = rootEntity.Data[i].Itineraries[returnDateItinerariesIndex].Segments[returnDateSegmentsIndex].Arrival.At,

                NumberOfTransfersDeparture = rootEntity.Data[i].Itineraries[0].Segments.Count - 1,
                NumberOfTransfersReturn = rootEntity.Data[i].Itineraries[1].Segments.Count - 1,

                NumberOfBookableSeats = rootEntity.Data[i].NumberOfBookableSeats,
                Currency = rootEntity.Data[i].Price.Currency,
                GrandTotal = double.Parse(rootEntity.Data[i].Price.GrandTotal),
            };

            flights.Add(flight);
        }

        return flights;
    }
}