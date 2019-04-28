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
    public class BITM1 : IBITM1
    {
        private string _CadenaConeccion;
        public BITM1(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<ITM1> IBITM1.FP_LISTAR_ITM1()
        {
            IQITM1 objITM1D = null;
            try
            {
                objITM1D = new DataITM1(this._CadenaConeccion);
                return objITM1D.FP_LISTAR_ITM1();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objITM1D = null;
            }
        }
        IList<ITM1> IBITM1.FP_LISTAR_ITM1_Filtrar(int ListNum)
        {
            IQITM1 objITM1D = null;
            try
            {
                objITM1D = new DataITM1(this._CadenaConeccion);
                return objITM1D.FP_LISTAR_ITM1_Filtrar(ListNum);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objITM1D = null;
            }
        }
    }
}
