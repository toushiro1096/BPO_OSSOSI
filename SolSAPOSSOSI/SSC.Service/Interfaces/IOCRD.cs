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
    public interface IOCRD
    {

        [OperationContract]
        OCRD FP_BUSCAR_OCRD(OCRD ObjOCRD, string Compania, string Key);
        [OperationContract]
        [WebGet(UriTemplate = "/GetData",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<OCRD> FP_Listar_OCRD(OCRD ObjOCRD, string Compania, string Key);
    }
}
