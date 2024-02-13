using AirCheap.Client.ApiServices;
using AirCheap.Client.DataTransferObjects;
using AirCheap.Client.Modals;
using AirCheap.Client.Models;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace AirCheap.Client.Pages;

public partial class Dashboard
{
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private ILocalStorageService LocalStorageService { get; set; }

    [Inject]
    private IFlightApiService FlightApiService { get; set; }

    [Inject]
    private IUserApiService UserApiService { get; set; }

    [Inject]
    private IValidator<FlightGetDto> FlightGetDtoValidator { get; set; }

    [Inject]
    private IToastService ToastService { get; set; }

    [CascadingParameter]
    private IModalService ModalService { get; set; }

    private FlightGetDto FlightGetDto { get; set; } = new();

    private DateTime DepartureDate { get; set; } = DateTime.Now;

    private DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);

    private string CurrencyCode { get; set; }

    private List<Flight> Flights { get; set; } = new();

    private List<FlightGetDto> SearchQueires { get; set; } = new();

    private bool ShowErrors { get; set; }

    private List<string> Errors { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadSearchQueryHistoryAsync();
    }

    private async Task LoadSearchQueryHistoryAsync()
    {
        IEnumerable<string> allKeys = await LocalStorageService.KeysAsync();

        if (allKeys is not null)
        {
            IEnumerable<string> searchQueries = allKeys.Where(key => !key.Contains("token"));

            foreach (string query in searchQueries)
            {
                FlightGetDto dtoFromQuery = JsonSerializer.Deserialize<FlightGetDto>(query);

                FlightGetDto flightGetDto = new()
                {
                    OriginLocationCode = dtoFromQuery.OriginLocationCode,
                    DestinationLocationCode = dtoFromQuery.DestinationLocationCode,
                    DepartureDate = dtoFromQuery.DepartureDate,
                    ReturnDate = dtoFromQuery.ReturnDate,
                    Adults = dtoFromQuery.Adults,
                    CurrencyCode = dtoFromQuery.CurrencyCode
                };

                SearchQueires.Add(flightGetDto);
            }
        }
    }

    private async Task HandleSearchFlightsAsync()
    {
        ShowErrors = false;
        Errors.Clear();

        FlightGetDto.DepartureDate = DepartureDate.Date;
        FlightGetDto.ReturnDate = ReturnDate.Date;
        FlightGetDto.CurrencyCode = CurrencyCode;

        if (FormIsValid())
        {
            ModalOptions options = new() { Animation = ModalAnimation.FadeInOut(0.3), HideCloseButton = true };
            IModalReference modal = ModalService.Show<LoadingModal>("Searching flights, please wait...", options);

            string flightGetDtoJson = JsonSerializer.Serialize(FlightGetDto);
            List<Flight> searchQueryResultCache = await LocalStorageService.GetItemAsync<List<Flight>>(flightGetDtoJson);

            if (searchQueryResultCache is null)
            {
                ResultResponseDto<Flight> response = await FlightApiService.SearchFlightsAsync(FlightGetDto);

                if (response.Success)
                {
                    searchQueryResultCache = new List<Flight>();
                    searchQueryResultCache.AddRange(response.CollectionResult.ToList());

                    await LocalStorageService.SetItemAsync(flightGetDtoJson, searchQueryResultCache);

                    FlightGetDto flightGetDto = JsonSerializer.Deserialize<FlightGetDto>(flightGetDtoJson);
                    SearchQueires.Add(flightGetDto);

                    modal.Close();
                    ToastService.ShowSuccess("Flight search completed.", "Success");

                    Flights = response.CollectionResult.ToList();
                    return;
                }

                modal.Close();
                Errors.AddRange(response.Errors);
                ShowErrors = true;
                return;
            }

            Flights = searchQueryResultCache;
            modal.Close();
        }
    }

    private bool FormIsValid()
    {
        ValidationResult validationResult = FlightGetDtoValidator.Validate(FlightGetDto);

        if (validationResult.IsValid) return true;

        Errors.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));

        ShowErrors = true;

        return false;
    }

    private void OpenAccountDetailsModal(string username)
    {
        ModalParameters parameters = new();
        parameters.Add("Username", username);

        ModalOptions options = new() { Animation = ModalAnimation.FadeInOut(0.3) };

        ModalService.Show<AccountDetailsModal>("Account details", parameters, options);
    }

    private async Task OpenUpdateAccountModalAsync(string username)
    {
        ModalParameters parameters = new();
        parameters.Add("Username", username);

        ModalOptions options = new() { Animation = ModalAnimation.FadeInOut(0.3) };

        IModalReference modal = ModalService.Show<AccountUpdateModal>("Account update", parameters, options);
        ModalResult modalResult = await modal.Result;

        if (!modalResult.Cancelled)
        {
            ToastService.ShowSuccess("Account updated successfully.", "Success");
        }
    }

    private async Task OpenDeleteAccountModalAsync(string username)
    {
        ModalParameters parameters = new();
        parameters.Add("Username", username);

        ModalOptions options = new() { Animation = ModalAnimation.FadeInOut(0.3) };

        IModalReference modal = ModalService.Show<AccountDeleteModal>("Account delete", parameters, options);
        ModalResult modalResult = await modal.Result;

        if (!modalResult.Cancelled)
        {
            await UserApiService.LogoutUserAsync();
            NavigationManager.NavigateTo("/", true);
        }
    }

    private async Task OpenLogoutModalAsync()
    {
        ModalOptions options = new() { Animation = ModalAnimation.FadeInOut(0.3) };

        IModalReference modal = ModalService.Show<LogoutModal>("Logout", options);
        ModalResult modalResult = await modal.Result;

        if (!modalResult.Cancelled)
        {
            NavigationManager.NavigateTo("/", true);
        }
    }

    private async Task HandleSearchFlightsFromHistoryAsync(FlightGetDto searchQuery)
    {
        ModalOptions options = new() { Animation = ModalAnimation.FadeInOut(0.3), HideCloseButton = true };
        IModalReference modal = ModalService.Show<LoadingModal>("Searching flights, please wait...", options);

        string flightGetDtoJson = JsonSerializer.Serialize(searchQuery);
        List<Flight> searchQueryResultCache = await LocalStorageService.GetItemAsync<List<Flight>>(flightGetDtoJson);

        Flights = searchQueryResultCache;
        modal.Close();
    }
}