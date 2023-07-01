using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderSevice;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;
        public CreateOrderCommandHandler(IOrderService orderSevice, IBasketService basketService, IOrderHubService orderHubService)
        {
            _orderSevice = orderSevice;
            _basketService = basketService;
            _orderHubService = orderHubService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            await _orderSevice.CreateOrderAsync(new()
            {
                Adress = request.Address,
                Description = request.Description,
                BasketId = _basketService.GetUserActiveBasket?.Id.ToString()

            });
            await _orderHubService.OrderCreatedMessageAsync("Yeni bir sipariş gelmiştir!");
            return new();
        }
    }
}
