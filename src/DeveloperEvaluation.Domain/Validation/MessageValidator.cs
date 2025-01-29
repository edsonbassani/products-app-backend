using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace DeveloperEvaluation.Domain.Validation;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator()
    {
        RuleFor(message => message.Payload)
            .NotEmpty()
            .MinimumLength(50).WithMessage("Payload must be at least 5 characters long");
    }
}
