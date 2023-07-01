using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CompleteOrder
{
    public class ComplateOrderCommandHandler : IRequestHandler<ComplateOrderCommandRequest, ComplateOrderCommandResponse>
    {

        readonly IOrderService _orderService;
        readonly IMailService _mailService;
        public ComplateOrderCommandHandler(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<ComplateOrderCommandResponse> Handle(ComplateOrderCommandRequest request, CancellationToken cancellationToken)
        {
         (bool succeeded,CompletedOrderMail dto) =  await _orderService.CompleteOrderAsync(request.Id);
            if (succeeded)
              await  _mailService.SendCompletedOrderMailAsync(dto.Email,dto.OrderCode,dto.OrderDate,dto.UserName,dto.UserSurname);
            return new();
        }
    }
}
