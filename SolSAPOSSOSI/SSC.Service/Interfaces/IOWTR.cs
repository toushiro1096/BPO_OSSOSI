using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SSC.Service.Interfaces
{
    [ServiceContract]
    public interface IOWTR
    {
        //[OperationContract]
        //int FP_Solicitud_Traslado_OWTR(OWTR ObjOWTR, out string DocEntry);
        [OperationContract]
        List<CError> FP_Transferencia_Stock_OWTR(List<OWTR> LstOWTR, string Compania, string Key);
    }
}
