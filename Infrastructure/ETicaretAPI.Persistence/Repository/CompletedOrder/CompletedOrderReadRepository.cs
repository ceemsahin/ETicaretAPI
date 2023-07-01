using ETicaretAPI.Application.Repositories.CompletedOrder;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repository.CompletedOrder
{
    public class CompletedOrderReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.CompletedOrder>,ICompletedOrderReadRepository 
    {
        public CompletedOrderReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
