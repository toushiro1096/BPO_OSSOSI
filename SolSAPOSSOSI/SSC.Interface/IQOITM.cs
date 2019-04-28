﻿using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Interface
{
    public interface IQOITM
    {
        IList<OITM> FP_LISTAR_OITM(string FechaIni, string FechaFin);
        IList<OITM> FP_LISTAR_OITM_TOT();
        string FP_get_ItemCode(string ItemName);
    }
}
