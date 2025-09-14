﻿namespace SmartSolutionsLab.YellowCarRental.Application.Contracts.Booking;

public class BookingCustomerData
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}