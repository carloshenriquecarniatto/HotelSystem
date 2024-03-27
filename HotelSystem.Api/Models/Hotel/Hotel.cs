namespace HotelSystem.Api.Models.Hotel;

public record HotelResult(Guid Id, string Name, string Address)
{
    public static implicit operator HotelResult(Application.Entities.Hotels.Hotel hotel)
    {
        return hotel is null ? null : new HotelResult(hotel.Id, hotel.Name, hotel.Address);
    }
}

public record NewHotel(string Name, string Address)
{
    public static implicit operator Application.Entities.Hotels.Hotel(NewHotel newHotel)
    {
        return new Application.Entities.Hotels.Hotel
        {
            Address = newHotel.Address,
            Name = newHotel.Name
        };
    }
}

public record UpdateHotel(Guid Id,string Name, string Address)
{
    public static implicit operator Application.Entities.Hotels.Hotel(UpdateHotel updateHotel)
    {
        return new Application.Entities.Hotels.Hotel
        {
            Id = updateHotel.Id,
            Address = updateHotel.Address,
            Name = updateHotel.Name
        };
    }
}