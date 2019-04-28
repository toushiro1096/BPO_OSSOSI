using SSC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SSC.Service.Interfaces
{
    [ServiceContract]
    public interface SOJDT
    {
        [OperationContract]
        List<CError> SP_INSERTAR_OJDT(List<OJDT> LstOJDT, string Compania, string Key);
    }
}
