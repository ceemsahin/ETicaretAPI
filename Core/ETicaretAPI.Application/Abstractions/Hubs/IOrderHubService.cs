namespace ETicaretAPI.Application.Abstractions.Hubs
{
    public interface IOrderHubService
    {

        Task OrderCreatedMessageAsync(string message);

    }
}
