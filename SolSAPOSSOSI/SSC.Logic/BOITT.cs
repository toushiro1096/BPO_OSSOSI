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
    public class BOITT: IBOITT
    {
        private string _CadenaConeccion;
        public BOITT(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<OITT> IBOITT.FP_LISTAR_OITT()
        {
            IQOITT objOITTD = null;
            try
            {
                objOITTD = new DataOITT(this._CadenaConeccion);
                return objOITTD.FP_LISTAR_OITT();
            }
            catch (Exception ex) 
            {
                throw;
            }
            finally
            {
                objOITTD = null;
            }
        }
    }
}
