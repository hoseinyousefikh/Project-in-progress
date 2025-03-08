using App.Domain.Core.Home.Contract.AppServices.ListOrder;
using App.Domain.Core.Home.DTO;
using DwellApi.AuthFilters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DwellApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }


        [HttpGet]
        [Route("api/orders")]
        [ServiceFilter(typeof(ApiKeyAuthFilter))]
        public async Task<IActionResult> GetAllOrders(CancellationToken cancellationToken)
        {
            try
            {
                var allOrders = await _orderAppService.GetAllOrdersAsync(cancellationToken);

                if (allOrders == null || !allOrders.Any())
                {
                    return NotFound(new { message = "هیچ سفارشی پیدا نشد" });
                }

               
                var orderDtos = allOrders.Select(j => new OrderDto
                {
                    Id = j.Id,
                    ServiceTitle = j.Description,
                    Price = j.BasePrice,
                    OrderDate = j.RequestDate,
                    Status = j.OrderStatus.ToString()
                }).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                };

                var jsonString = JsonSerializer.Serialize(orderDtos, options);

                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطای سرور: " + ex.Message });
            }
        }




    }
}
