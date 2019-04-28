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
    public interface SOSLP
    {
        [OperationContract]
        List<CError> SP_INSERTAR_OSLP(List<OSLP> LstOSLP, string Compania, string Key);
    }
}
