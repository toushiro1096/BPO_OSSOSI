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
    public class BITT1 : IBITT1
    {
        private string _CadenaConeccion;
        public BITT1(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<ITT1> IBITT1.FP_LISTAR_ITT1(string ItemFather)
        {
            IQITT1 objITT1D = null;
            try
            {
                objITT1D = new DataITT1(this._CadenaConeccion);
                return objITT1D.FP_LISTAR_ITT1(ItemFather);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objITT1D = null;
            }
        }
    }
}
