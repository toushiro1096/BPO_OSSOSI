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
    public class BOITM : IBOITM
    {
        private string _CadenaConeccion;
        public BOITM(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }
        IList<OITM> IBOITM.FP_LISTAR_OITM(string FechaIni, string FechaFin)
        {
            IQOITM objOITMD = null;
            try
            {
                objOITMD = new DataOITM(this._CadenaConeccion);
                return objOITMD.FP_LISTAR_OITM(FechaIni, FechaFin);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOITMD = null;
            }
        }
        IList<OITM> IBOITM.FP_LISTAR_OITM_TOT()
        {
            IQOITM objOITMD = null;
            try
            {
                objOITMD = new DataOITM(this._CadenaConeccion);
                return objOITMD.FP_LISTAR_OITM_TOT();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOITMD = null;
            }
        }
        string IBOITM.FP_get_ItemCode(string ItemName)
        {
            IQOITM objOITMD = null;
            try
            {
                objOITMD = new DataOITM(this._CadenaConeccion);
                return objOITMD.FP_get_ItemCode(ItemName);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOITMD = null;
            }
        }
    }
}
