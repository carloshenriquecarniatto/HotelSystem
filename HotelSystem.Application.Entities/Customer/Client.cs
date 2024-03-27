using HotelSystem.Application.Entities.Orders;

namespace HotelSystem.Application.Entities.Customer;

public class Client : Entity
{
    public string Name { get;  set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public ICollection<HotelBooking> HotelBookings { get; set; }
}