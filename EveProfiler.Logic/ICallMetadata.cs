using System;

namespace EveProfiler.Logic
{
    public interface ICallMetadata
    {
        DateTime LastPulled { get; set; }
        DateTime CachedUntil { get; set; }
    }
}
