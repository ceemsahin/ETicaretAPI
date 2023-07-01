using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<ProductCreateViewModel>
    {
        public CreateProductValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Lütfen ürün adaını boş geçmeyiniz").MinimumLength(2).WithMessage("Minimum 2 karakter girmelisiniz").MaximumLength(150).WithMessage("En fazla 150 karakter girebilirsiniz");

            RuleFor(c => c.Stock).NotNull().NotNull().WithMessage("Lütfen stok bilgisini boş geçmeyiniz").Must(c => c >= 0).WithMessage("Stock 0'dan küçük olamaz");

            RuleFor(c => c.Price).NotNull().NotNull().WithMessage("Lütfen fiyat bilgisini boş geçmeyiniz").Must(c => c >= 0).WithMessage("Fiyat bilgisi 0'dan küçük olamaz");
        }


    }
}
