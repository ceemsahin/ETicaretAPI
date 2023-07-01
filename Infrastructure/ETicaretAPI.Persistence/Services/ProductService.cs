using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories.ProductRepository;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductService : IProductService
    {

        readonly IProductReadRepository _readRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IQRCodeService _qrCodeService;



        public ProductService(IProductReadRepository readRepository, IQRCodeService qrCodeService, IProductWriteRepository productWriteRepository)
        {
            _readRepository = readRepository;
            _qrCodeService = qrCodeService;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<byte[]> QRCodeToProductAsync(string productId)
        {
            Product product = await _readRepository.GetByIdAsync(productId);

            if (product==null)
            {
                throw new Exception("Product not found");
            }

            var plaintObject = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.Stock,
                product.CratedDate
            };

            string plainCode = JsonSerializer.Serialize(plaintObject);


            return _qrCodeService.GenerateQRCode(plainCode);


        
        }

        public async Task StockUpdateToProductAsync(string productId, int stock)
        {
            Product product = await _readRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }
            product.Stock = stock;
            await _productWriteRepository.SaveAsync();
        }
    }
}
