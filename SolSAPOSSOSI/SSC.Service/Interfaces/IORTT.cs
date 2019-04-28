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
    public interface IORTT
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        IList<ORTT> FP_LISTAR_ORTT(string date, string Compania, string Key);
    }
}
