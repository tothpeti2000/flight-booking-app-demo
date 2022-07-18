using FlyTonight.Domain.Interfaces;
using MediatR;

namespace FlyTonight.Application.Feature.TicketOrdering
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly IOrderRepository orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetAsync(request.Id, cancellationToken);

            orderRepository.Delete(order);

            var timeDifference = DateTime.Now - order.DateOfPurchase;
            var returnPrice = timeDifference.Days <= 30 ? order.Price : (order.Price / 2);

            return returnPrice;
        }
    }
}
