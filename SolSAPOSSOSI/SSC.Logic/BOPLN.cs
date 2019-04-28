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
    public class BOPLN : IBOPLN
    {
        private string _CadenaConeccion;
        public BOPLN(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<OPLN> IBOPLN.FP_LISTAR_OPLN()
        {
            IQOPLN objOPLND = null;
            try
            {
                objOPLND = new DataOPLN(this._CadenaConeccion);
                return objOPLND.FP_LISTAR_OPLN();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOPLND = null;
            }
        }
    }
}
