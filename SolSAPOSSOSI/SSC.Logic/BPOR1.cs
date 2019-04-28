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
    public class BPOR1 : IBPOR1
    {
        private string _CadenaConeccion;
        public BPOR1(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }

        IList<POR1> IBPOR1.FP_LISTAR_POR1(int DocEntry)
        {
            IQPOR1 objPOR1D = null;
            try
            {
                objPOR1D = new DataPOR1(this._CadenaConeccion);
                return objPOR1D.FP_LISTAR_POR1(DocEntry);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objPOR1D = null;
            }
        }
    }
}
