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
    public interface IOITM
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        IList<OITM> FP_LISTAR_OITM(string FechaIni, string FechaFin, string Compania, string Key);
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        IList<OITM> FP_LISTAR_OITM_TOT(string Compania, string Key);
    }
}
