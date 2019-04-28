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
    public class BOPOR : IBOPOR
    {
        private string _CadenaConeccion;
        public BOPOR(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }

        IList<OPOR> IBOPOR.FP_LISTAR_OPOR(string FechaIni, string FechaFin)
        {
            IQOPOR objOPORD = null;
            try
            {
                objOPORD = new DataOPOR(this._CadenaConeccion);
                return objOPORD.FP_LISTAR_OPOR(FechaIni, FechaFin);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOPORD = null;
            }
        }
        ORDR IBOPOR.FP_GET_OINV_TICKET(string Ticket)
        {
            IQOPOR objOPORD = null;
            try
            {
                objOPORD = new DataOPOR(this._CadenaConeccion);
                return objOPORD.FP_GET_OINV_TICKET(Ticket);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOPORD = null;
            }
        }

    }
}
