using HotelSystem.Application.Entities.Customer;

namespace HotelSystem.Api.Models.Customer;

public record CustomerResult(Guid Id, string Name, string Address, string Phone)
{
    public static implicit operator CustomerResult(Client client) => client is null ? null :
        new(client.Id, client.Name, client.Address, client.Phone);
}

public record NewCustomer(string Name, string Address, string Phone)
{
    public static implicit operator Client(NewCustomer newCustomer) => new Client
    {
        Name = newCustomer.Name,
        Address = newCustomer.Address,
        Phone = newCustomer.Phone
    };
}

public record UpdateCustomer(Guid Id, string Name, string Address, string Phone)
{
    public static implicit operator Client(UpdateCustomer updateCustomer) => new Client
    {
        Id = updateCustomer.Id,
        Name = updateCustomer.Name,
        Address = updateCustomer.Address,
        Phone = updateCustomer.Phone
    };
}