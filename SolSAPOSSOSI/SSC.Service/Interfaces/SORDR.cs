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
    public interface SORDR
    {
        /*
        [OperationContract]
        int FP_Solicitud_Venta_ORDR(ORDR ObjORDR, out string docnum, string Compania, string Key);
        */
        [OperationContract]
        List<CError> FP_Solicitud_Venta_Masiva_ORDR(List<ORDR> LstORDR, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Nota_Credito_ORDR(List<ORDR> LstORDR, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Guia_Remision(List<ORDR> LstORDR, string Compania, string Key);
        [OperationContract]
        List<CError> FP_Cobranzas_Masiva_ORDR(List<ORCT> LstORCT, string Compania, string Key);
    }
}
