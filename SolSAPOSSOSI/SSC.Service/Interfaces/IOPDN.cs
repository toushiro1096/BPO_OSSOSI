using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SSC.Service.Interfaces
{

    [ServiceContract]
    public interface IOPDN
    {
        [OperationContract]
        List<CError> FP_Solicitud_Recibo_OPDN(List<OPDN> LstOPDN, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Solicitud_Salida_OPDN(List<OPDN> LstOPDN, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Solicitud_Ajuste_Salida_OPDN(List<OPDN> LstOPDN, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Solicitud_Ajuste_Ingreso_OPDN(List<OPDN> LstOPDN, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Solicitud_Transformacion_Ingreso(List<OWOR> LstOWOR, string Compania, string Key);
    }

}