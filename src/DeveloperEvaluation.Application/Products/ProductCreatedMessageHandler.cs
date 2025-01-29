using Rebus.Handlers;
using Developerevaluation.Domain.Entities;

namespace Developerevaluation.Application.Users.Handlers
{
    public class ProductCreatedMessageHandler : IHandleMessages<Product>
    {
        public Task Handle(Product message)
        {
            Console.WriteLine($"Produt created: {message.Id} {message.Name} ({message.Price})");
            return Task.CompletedTask;
        }
    }
}
