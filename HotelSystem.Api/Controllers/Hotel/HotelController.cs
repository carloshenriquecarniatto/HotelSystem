using HotelSystem.Api.Models.Hotel;
using HotelSystem.Application.Domain.Generic.Generics;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Api.Controllers.Hotel;

[ApiController]
[Route("api/[controller]")]
public class HotelController(IUnitOfWork unitOfWork) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = (await unitOfWork.Repository<Application.Entities.Hotels.Hotel>().FindAllAsync()).Select(c=> (HotelResult)c);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] NewHotel newCustomer)
    {
        var result = await unitOfWork.Repository<Application.Entities.Hotels.Hotel>().Add(newCustomer);
        return Ok((HotelResult)result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateHotel([FromBody] UpdateHotel updateCustomer)
    {
        await unitOfWork.Repository<Application.Entities.Hotels.Hotel>().Update(updateCustomer);
        var result = await unitOfWork.Repository<Application.Entities.Hotels.Hotel>().FindById(updateCustomer.Id);
        return Ok((HotelResult)result);
    }
}