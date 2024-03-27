using HotelSystem.Application.Entities.Customer;
using HotelSystem.Application.Entities.Hotels;

namespace HotelSystem.Application.Entities.Orders;

public class HotelBooking : Entity
{
    public Guid HotelId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; }
    public Hotel Hotel { get; set; }
}