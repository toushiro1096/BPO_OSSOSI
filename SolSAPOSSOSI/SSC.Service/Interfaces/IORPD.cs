using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SSC.Service.Interfaces
{
    [ServiceContract]
    public interface IORPD
    {
        [OperationContract]
        List<CError> FP_Devolucion_Recibo_ORPD(List<ORPD> LstORPD, string Compania, string Key);
    }
}