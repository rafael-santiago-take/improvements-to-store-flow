using ImprovementStoreFlows.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImprovementStoreFlows.DAO
{
    public interface IDAO
    {
        Task InsertAsync(FlowIdentity flow);
        Task UpdateAsync(FlowIdentity flow);
        Task DeleteAsync(Guid id);

        Task<FlowIdentity?> GetAsync(Guid id);
    }
}
