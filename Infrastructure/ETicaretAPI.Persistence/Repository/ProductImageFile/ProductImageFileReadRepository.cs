using ETicaretAPI.Application.Repositories.ProductImageFile;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.ProductImageFile
{
    public class ProductImageFileReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
