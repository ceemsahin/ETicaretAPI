using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.BasketItem
{
    public class BasketItemReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
