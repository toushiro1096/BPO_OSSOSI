﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSC.Entity;

namespace SSC.Interface
{
    public interface IQOCRD
    {
        OCRD FP_BUSCAR_OCRD(OCRD ObjOCRD);
        List<OCRD> FP_Listar_OCRD(OCRD ObjOCRD);
    }

}
