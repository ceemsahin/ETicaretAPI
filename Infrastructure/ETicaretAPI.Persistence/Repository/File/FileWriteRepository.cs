using ETicaretAPI.Application.Repositories.File;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository.File
{
    public class FileWriteRepository : WriteRepository<ETicaretAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
