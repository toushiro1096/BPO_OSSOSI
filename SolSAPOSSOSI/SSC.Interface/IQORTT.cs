using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Interface
{
    public interface IQORTT
    {
        IList<ORTT> FP_LISTAR_ORTT(string date);
    }
}
