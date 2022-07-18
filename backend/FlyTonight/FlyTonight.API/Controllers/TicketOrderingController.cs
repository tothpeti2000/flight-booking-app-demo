using System.Security.Claims;
using FlyTonight.Application.Feature.TicketOrdering;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlyTonight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketOrderingController : ControllerBase
    {
        private readonly IMediator mediator;

        public TicketOrderingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize]
        [HttpPost("order")]
        public async Task<PlaceOrderResponse> PlaceOrder([FromBody] PlaceOrderCommand dto)
        {
            dto.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await mediator.Send(dto);
        }

        [Authorize]
        [HttpGet("flights")]
        public async Task<GetAvailableFlightsResponse> GetAvailableFlights([FromQuery] GetAvailableFlightsRequest dto)
        {
            return await mediator.Send(dto);
        }

        [Authorize]
        [HttpGet("seats")]
        public async Task<GetAvailableSeatsResponse> GetAvailableSeats([FromQuery] GetAvailableSeatsRequest dto)
        {
            return await mediator.Send(dto);
        }

        [Authorize]
        [HttpDelete("{orderId}")]
        public Task DeleteOrder(Guid orderId)
        {
            return mediator.Send(new DeleteOrderCommand { Id = orderId });
        }

        [Authorize]
        [HttpGet]
        public async Task<GetOrdersResponse> GetOrders()
        {
            var dto = new GetOrdersRequest
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };

            return await mediator.Send(dto);
        }

        [Authorize]
        [HttpGet("details/{orderId}")]
        public async Task<GetOrderDetailsResponse> GetOrderDetails(Guid orderId)
        {            
            return await mediator.Send(new GetOrderDetailsRequest { Id = orderId });
        }

        [Authorize]
        [HttpPut("{orderId}")]
        public Task UpdateOrder(Guid orderId, [FromBody] UpdateOrderCommand dto)
        {
            dto.OrderId = orderId;
            return mediator.Send(dto);
        }
    }
}
