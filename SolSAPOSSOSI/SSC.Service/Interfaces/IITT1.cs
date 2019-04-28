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
    public interface IITT1
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        IList<ITT1> FP_LISTAR_ITT1(string ItemFather, string Compania, string Key);
    }
}
