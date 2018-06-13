using System;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IDependencyInjection
    {
        object GetService(Type serviceType);

        TService GetService<TService>();
    }
}
