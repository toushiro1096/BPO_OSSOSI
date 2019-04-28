using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Interface
{
    public interface IQOPOR
    {
        IList<OPOR> FP_LISTAR_OPOR(string FechaIni, string FechaFin);
        ORDR FP_GET_OINV_TICKET(string Ticket);
    }
}
