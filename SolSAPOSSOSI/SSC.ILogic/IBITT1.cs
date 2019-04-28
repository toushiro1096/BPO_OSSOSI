using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.ILogic
{
    public interface IBITT1
    {
        IList<ITT1> FP_LISTAR_ITT1(string ItemFather);
    }
}
