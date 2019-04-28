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
    public class BOWHS : IBOWHS
    {
        private string _CadenaConeccion;
        public BOWHS(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<OWHS> IBOWHS.FP_LISTAR_OWHS()
        {
            IQOWHS objOWHSD = null;
            try
            {
                objOWHSD = new DataOWHS(this._CadenaConeccion);
                return objOWHSD.FP_LISTAR_OWHS();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOWHSD = null;
            }
        }
    }
}
