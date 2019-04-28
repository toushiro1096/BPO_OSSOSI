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
    public interface IOPOR
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        IList<OPOR> FP_LISTAR_OPOR(string FechaIni, string FechaFin, string Compania, string Key);
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json, 
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        IList<OPRQ> FP_LISTAR_OPOR_Open(string CardCode, string docEntry, string Compania, string Key);
    }

    [ServiceContract]
    public interface SOPOR
    {
        [OperationContract]
        List<CError> FP_Orden_Compra_OPOR(List<OPRQ> LstOPRQ, string Compania, string Key);
        [OperationContract]
        string lasgetItemCode(string ItemCode, string Compania);
    }

}
