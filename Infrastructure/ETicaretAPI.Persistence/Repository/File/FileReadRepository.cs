using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.File
{
    public class FileReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
