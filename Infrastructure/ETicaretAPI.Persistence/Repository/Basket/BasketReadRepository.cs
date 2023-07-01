using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.Basket
{
    public class BasketReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.Basket>, IBasketReadRepository
    {
        public BasketReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
