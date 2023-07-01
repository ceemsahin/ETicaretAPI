using ETicaretAPI.Application.Repositories.InvoiceFile;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.InvoiceFile
{
    public class InvoiceFileWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
