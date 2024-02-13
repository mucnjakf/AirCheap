using AirCheap.Core.Models;
using AirCheap.Server.ApiServices;
using AirCheap.Server.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirCheap.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/flights")]
public class FlightController : ControllerBase
{
    private readonly IFlightApiService _flightApiService;

    public FlightController(IFlightApiService flightApiService)
    {
        _flightApiService = flightApiService ?? throw new ArgumentNullException(nameof(flightApiService));
    }

    [HttpPost]
    public async Task<ResultResponseDto<Flight>> SearchFlightsAsync([FromBody] FlightGetDto flightsGetDto)
        => await _flightApiService.SearchFlightsAsync(flightsGetDto);
}
