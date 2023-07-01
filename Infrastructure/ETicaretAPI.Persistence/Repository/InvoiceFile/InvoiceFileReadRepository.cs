using ETicaretAPI.Application.Repositories.InvoiceFile;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.InvoiceFile
{
    public class InvoiceFileReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
