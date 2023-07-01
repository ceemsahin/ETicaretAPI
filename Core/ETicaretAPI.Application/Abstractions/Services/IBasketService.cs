using ETicaretAPI.Application.ViewModels.Baskets;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();

        public Task AddItemToBasketAsync(CreateBasketItemViewModel basketItem);

        public Task RemoveItemFromBasketAsync(string basketItemId);
        public Task UpdateQuantityAsync(UpdateBasketItemViewModel basketItem);


        public Basket? GetUserActiveBasket { get; }

    }
}
