using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Interface
{
    public interface IQPOR1
    {
        IList<POR1> FP_LISTAR_POR1(int DocEntry);
    }
}
