using HotelSystem.Application.Entities.Orders;

namespace HotelSystem.Application.Entities.Hotels;

public class Hotel : Entity
{
    public string Name { get;set; }
    public string Address { get;set; }
    public ICollection<HotelBooking> HotelBookings { get; set; }
}