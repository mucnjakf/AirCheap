using AirCheap.Client.DataTransferObjects;
using AirCheap.Client.Models;
using System.Text;
using System.Text.Json;

namespace AirCheap.Client.ApiServices;

public class FlightApiService : IFlightApiService
{
    private readonly HttpClient _httpClient;

    public FlightApiService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<ResultResponseDto<Flight>> SearchFlightsAsync(FlightGetDto flightGetDto)
    {
        try
        {
            StringContent data = new(JsonSerializer.Serialize(flightGetDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/flights", data);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            ResultResponseDto<Flight> response = JsonSerializer.Deserialize<ResultResponseDto<Flight>>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
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
