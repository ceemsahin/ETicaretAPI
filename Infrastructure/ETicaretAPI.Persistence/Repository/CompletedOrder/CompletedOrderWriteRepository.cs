using ETicaretAPI.Application.Repositories.CompletedOrder;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repository.CompletedOrder
{
    public class CompletedOrderWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
