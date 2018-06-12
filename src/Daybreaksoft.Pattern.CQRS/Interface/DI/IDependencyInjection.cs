using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public interface IDependencyInjection
    {
        TService GetService<TService>();
    }
}
