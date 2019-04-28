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
    public class BORTT : IBORTT
    {
        private string _CadenaConeccion;
        public BORTT(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<ORTT> IBORTT.FP_LISTAR_ORTT(string date)
        {
            IQORTT objORTTD = null;
            try
            {
                objORTTD = new DataORTT(this._CadenaConeccion);
                return objORTTD.FP_LISTAR_ORTT(date);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objORTTD = null;
            }
        }
    }
}
