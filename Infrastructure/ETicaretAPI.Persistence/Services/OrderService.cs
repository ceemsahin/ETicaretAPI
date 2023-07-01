using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories.CompletedOrder;
using ETicaretAPI.Application.Repositories.OrderRepository;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderWriteRepository _completeOrderWriteRepository;
        readonly ICompletedOrderReadRepository _completeOrderReadRepository;
        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completeOrderWriteRepository, ICompletedOrderReadRepository completeOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completeOrderWriteRepository = completeOrderWriteRepository;
            _completeOrderReadRepository = completeOrderReadRepository;
        }



        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 1000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(",") + 1, orderCode.Length - orderCode.IndexOf(",") - 1);

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Adress,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                      .ThenInclude(b => b.User)
                      .Include(o => o.Basket)
                         .ThenInclude(b => b.BasketItems)
                         .ThenInclude(bi => bi.Product);



            var data = query.Skip(page * size).Take(size);
            /*.Take((page * size)..size);*/


            var data2 = from order in data
                        join compeletOrder in _completeOrderReadRepository.Table
                        on order.Id equals compeletOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CratedDate = order.CratedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false
                        };



            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CratedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };


        }

        public async Task<SingleOrder> GetOrderById(string id)
        {
            var data = _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);

            var data2 = await (
                    from order in data
                    join completeOrder in _completeOrderReadRepository.Table
                    on order.Id equals completeOrder.OrderId into co
                    from _co in co.DefaultIfEmpty()
                    select new
                    {

                        Id = order.Id,
                        CratedDate = order.CratedDate,
                        OrderCode = order.OrderCode,
                        Basket = order.Basket,
                        Completed = _co != null ? true : false,
                        Address = order.Address,
                        Description = order.Description
                    }
                ).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));


            return new()
            {
                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity
                }),
                Address = data2.Address,
                CratedDate = data2.CratedDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode,
                Completed=  data2.Completed
            };
        }



        public async Task<(bool, CompletedOrderMail)> CompleteOrderAsync(string id)
        {
            Order? order = await _orderReadRepository.Table.Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if (order != null)
            {
                await _completeOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
                return (await _completeOrderWriteRepository.SaveAsync() > 0, new()
                {
                    OrderCode = order.OrderCode,
                    OrderDate = order.CratedDate,
                    UserName = order.Basket.User.UserName,
                    UserSurname=order.Basket.User.NameSurname,
                    Email=order.Basket.User.Email
                });

            }

            return (false,null);

        }



    }
}
