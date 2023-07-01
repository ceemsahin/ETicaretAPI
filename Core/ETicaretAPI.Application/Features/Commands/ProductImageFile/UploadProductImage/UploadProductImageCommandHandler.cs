using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories.ProductImageFile;
using ETicaretAPI.Application.Repositories.ProductRepository;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            //var datas = await _fileService.UploadAsync("resource/product-images", Request.Form.Files);


            //var datas = await _storageService.UploadAsync("files", Request.Form.Files);

            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName
            //}).ToList());

            //await _productImageFileWriteRepository.SaveAsync();

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("product-image", request.FormFiles);

            ETicaretAPI.Domain.Entities.Product product = await _productReadRepository.GetByIdAsync(request.Id);


            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new
            ETicaretAPI.Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<ETicaretAPI.Domain.Entities.Product>()
                {
                    product
                }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
