using ETicaretAPI.Application.Repositories.ProductImageFile;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.ProductImageFile
{
    public class ProductImageFileWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.ProductImageFile>, IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
