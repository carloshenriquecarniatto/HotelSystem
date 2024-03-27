using HotelSystem.Api.Models.Hotel;
using HotelSystem.Application.Domain.Generic.Generics;
using HotelSystem.Application.Entities.Orders;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Api.Controllers.Orders;

[Route("api/[controller]")]
[ApiController]
public class HotelBookingController(IUnitOfWork unitOfWork)  : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = (await unitOfWork.Repository<HotelBooking>().FindAllAsync()).Select(c=> (HotelBookingResult)c);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] NewBooking newCustomer)
    {
        var addResult = await unitOfWork.Repository<HotelBooking>().Add(newCustomer);
        var result = await unitOfWork.Repository<HotelBooking>().FindById(addResult.Id);
        return Ok((HotelBookingResult)result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateHotel([FromBody] UpdateBooking updateCustomer)
    {
        await unitOfWork.Repository<HotelBooking>().Update(updateCustomer);
        var result = await unitOfWork.Repository<HotelBooking>().FindById(updateCustomer.Id);
        return Ok((HotelBookingResult)result);
    }
}