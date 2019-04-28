using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SSC.Service
{

    [ServiceContract]
    public interface Iconfig
    {
        [OperationContract]
        void RefreshMemory();
    }
}
