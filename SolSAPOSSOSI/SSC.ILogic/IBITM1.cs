using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.ILogic
{
    public interface IBITM1
    {
        IList<ITM1> FP_LISTAR_ITM1();
        IList<ITM1> FP_LISTAR_ITM1_Filtrar(int ListNum);
    }
}
