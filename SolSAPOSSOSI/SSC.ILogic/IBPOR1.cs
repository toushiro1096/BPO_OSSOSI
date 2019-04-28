using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.ILogic
{
    public interface IBPOR1
    {
        IList<POR1> FP_LISTAR_POR1(int DocEntry);
    }
}
