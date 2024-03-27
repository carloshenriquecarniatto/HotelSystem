using HotelSystem.Api.Models.Customer;
using HotelSystem.Application.Domain.Generic.Generics;
using HotelSystem.Application.Entities.Customer;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Api.Controllers.Customer;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = (await unitOfWork.Repository<Client>().FindAllAsync()).Select(c=> (CustomerResult)c);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatCustomer([FromBody] NewCustomer newCustomer)
    {
        var result = await unitOfWork.Repository<Client>().Add(newCustomer);
        return Ok((CustomerResult)result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomer updateCustomer)
    {
        await unitOfWork.Repository<Client>().Update(updateCustomer);
        var result = await unitOfWork.Repository<Client>().FindById(updateCustomer.Id);
        return Ok((CustomerResult)result);
    }
}