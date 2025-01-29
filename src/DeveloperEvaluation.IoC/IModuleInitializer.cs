using Microsoft.AspNetCore.Builder;

namespace Developerevaluation.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder);
}
