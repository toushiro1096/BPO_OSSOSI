using SSC.Entity;
using SSC.ILogic;
using SSC.Interface;
using SSC.SQLDataLAyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Logic
{
    public class BOSLP : IBOSLP
    {
        private string _CadenaConeccion;
        public BOSLP(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }

        IList<OSLP> IBOSLP.FP_LISTAR_OSLP()
        {
            IQOSLP objOSLPD = null;
            try
            {
                objOSLPD = new DataOSLP(this._CadenaConeccion);
                return objOSLPD.FP_LISTAR_OSLP();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOSLPD = null;
            }
        }
    }
}
