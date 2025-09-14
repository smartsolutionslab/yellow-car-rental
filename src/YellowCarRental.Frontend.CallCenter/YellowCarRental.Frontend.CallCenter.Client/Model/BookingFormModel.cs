using MudBlazor;
using SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

namespace SmartSolutionsLab.YellowCarRental.Frontend.Shared.Components.Models;

public class BookingFormModel
{
    public Guid? Id { get; set; }
    public Guid VehicleId { get; set; } = Guid.Empty;
    public string VehicleName { get; set; } = null!;
    public BookingCustomerData? Customer { get; set; }
    public Guid StationId { get; set; } = Guid.Empty;

    public MudBlazor.DateRange Period { get; set; } = new DateRange();
    public decimal TotalPrice { get; set; }
    public string TotalPriceCurrency { get; set; }  = "EUR";
}

public enum CustomerMode
{
    New,
    Existing
}