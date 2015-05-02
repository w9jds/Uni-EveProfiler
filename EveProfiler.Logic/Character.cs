using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.BusinessLogic
{
    public class Character : Account, ICallMetadata
    {
        public long AccountId { get; set; }
        public DateTime CachedUntil { get; set; }
        public long CharacterId { get; set; }
        public string CharacterName { get; set; }
        public DateTime LastPulled { get; set; }

        public Dictionary<string, object> Attributes = new Dictionary<string, object>();
    }
}
