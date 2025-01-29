﻿using Rebus.Handlers;
using Developerevaluation.Domain.Entities;

namespace Developerevaluation.Application.Users.Handlers
{
    public class UserCreatedMessageHandler : IHandleMessages<User>
    {
        public Task Handle(User message)
        {
            Console.WriteLine($"User created: {message.Id} {message.Username} ({message.Email})");
            return Task.CompletedTask;
        }
    }
}
