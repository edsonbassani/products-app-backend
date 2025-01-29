using Rebus.Handlers;
using DeveloperEvaluation.Domain.Entities;

namespace DeveloperEvaluation.Application.Users.Handlers
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
