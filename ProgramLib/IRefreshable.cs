using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramLib
{
    public interface IRefreshable
    {
        void RefreshLists(List<Program> programs, List<ArbeitsAuftrag> work_orders);
    }
}
