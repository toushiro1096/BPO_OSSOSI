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
    public class BOCRD : IBOCRD
    {
        private string _CadenaConeccion;
        public BOCRD(string ArgCadenaConeccion)
        {
            _CadenaConeccion = ArgCadenaConeccion;
        }

        List<OCRD> IBOCRD.FP_Listar_OCRD(OCRD ObjOCRD)
        {
            IQOCRD objOCRDD = null;
            try
            {
                objOCRDD = new DataOCRD(this._CadenaConeccion);
                return objOCRDD.FP_Listar_OCRD(ObjOCRD);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOCRDD = null;
            }
        }

        OCRD IBOCRD.FP_BUSCAR_OCRD(OCRD ObjOCRD)
        {
            IQOCRD objOCRDD = null;
            try
            {
                objOCRDD = new DataOCRD(this._CadenaConeccion);
                return objOCRDD.FP_BUSCAR_OCRD(ObjOCRD);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                objOCRDD = null;
            }
        }
    }
}
