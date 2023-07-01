using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.BasketItem
{
    public class BasketItemWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.BasketItem>, IBasketItemWriteRepository
    {
        public BasketItemWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
