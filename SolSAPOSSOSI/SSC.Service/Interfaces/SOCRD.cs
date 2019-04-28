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
    public interface SOCRD
    {
        [OperationContract]
        List<CError> SP_INSERTAR_OCRD(List<OCRD> LstOCRD, string Compania, string Key);
    }
}