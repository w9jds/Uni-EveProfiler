using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic
{
    public interface ICallMetadata
    {
        DateTime LastPulled { get; set; }
        DateTime CachedUntil { get; set; }
    }
}
