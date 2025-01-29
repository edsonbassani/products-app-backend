using Developerevaluation.Domain.Entities;
using Developerevaluation.Domain.Enums;

namespace Developerevaluation.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
