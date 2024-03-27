using HotelSystem.Api.Models.Customer;
using HotelSystem.Application.Entities.Orders;

namespace HotelSystem.Api.Models.Hotel;

public record HotelBookingResult(Guid Id, HotelResult? Hotel, CustomerResult? Customer, string Name, string Address)
{
    public static implicit operator HotelBookingResult(HotelBooking hotelBooking)
    {
        return new HotelBookingResult(hotelBooking.Id, hotelBooking.Hotel, hotelBooking.Client, hotelBooking.Name,
            hotelBooking.Address);
    }
}


public record NewBooking(Guid HotelId, Guid CustomerId, string Name, string Address)
{
    public static implicit operator HotelBooking(NewBooking hotelBooking)
    {
        return new HotelBooking
        {
            Address = hotelBooking.Address,
            ClientId = hotelBooking.CustomerId,
            HotelId = hotelBooking.HotelId,
            Name = hotelBooking.Name
        };
    }
}


public record UpdateBooking(Guid Id,Guid HotelId, Guid CustomerId, string Name, string Address)
{
    public static implicit operator HotelBooking(UpdateBooking hotelBooking)
    {
        return new HotelBooking
        {
            Id = hotelBooking.Id,
            Address = hotelBooking.Address,
            ClientId = hotelBooking.CustomerId,
            HotelId = hotelBooking.HotelId,
            Name = hotelBooking.Name
        };
    }
}