using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Enums;
using FluentValidation;

namespace Developerevaluation.Domain.Validation;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator()
    {
        RuleFor(message => message.Payload)
            .NotEmpty()
            .MinimumLength(50).WithMessage("Payload must be at least 5 characters long");
    }
}
