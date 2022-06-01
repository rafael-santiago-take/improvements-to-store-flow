using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImprovementStoreFlows.Model
{
    public class FlowIdentity
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Flow { get; set; } = "";
    }
}
