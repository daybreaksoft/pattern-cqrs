using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daybreaksoft.Pattern.CQRS
{
    public enum AggregateState
    {
        Unchanged,
        Added,
        Modified,
        Deleted
    }
}
