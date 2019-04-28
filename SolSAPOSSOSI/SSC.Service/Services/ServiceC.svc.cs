using Microsoft.Web.Administration;
using SSC.Entity;
using SSC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAPbobsCOM;
using SSC.SBOSDK;
using System.Globalization;
using SSC.Service.MODULOS;
using SSC.ILogic;
using SSC.Logic;

namespace SSC.Service.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceC" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceC.svc o ServiceC.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceC : IOCRD, IOITM, IORTT
    {
        #region Variables

        private SAPbobsCOM.Company oCompany;

        #endregion

        #region Metodos del objeto Auxliares

        private bool Validar(string texto, string compania, out string messageError)
        {
            if (compania == null) { messageError = "Parametro Compania null"; return false; }
            if (texto == null) { messageError = "Parametro Key null"; ; return false; }
            messageError = "";
            return ModVariables.ValidarKey(texto);
        }

        void iniciarSAPCompany(string Company)
        {
            try
            {
                oCompany = new Company();
                oCompany = AppSAPbobsCOM.ObtenerCompany(Company);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        void FinalizarSAPCómpany()
        {
            try
            {
                oCompany.Disconnect();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region Metodos del objeto OCRD

        OCRD IOCRD.FP_BUSCAR_OCRD(OCRD ObjOCRD, string Compania, string Key)
        {
            IBOCRD OCRDB = null;
            try
            {
                string error;
                if (!this.Validar(Key, Compania, out error)) throw new Exception(error);
                if (ObjOCRD == null) throw new Exception("ObjOCRD vacio");
                if (ObjOCRD.LicTradNum == null || ObjOCRD.LicTradNum.Trim() == "") throw new Exception("LicTradNum vacio");
                if (ObjOCRD.Tipo == null || ObjOCRD.Tipo.Trim() == "") throw new Exception("Tipo vacio");
                OCRDB = new BOCRD(ModVariables.getCompanyConnection(Compania));
                return OCRDB.FP_BUSCAR_OCRD(ObjOCRD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OCRDB = null;
            }
        }

        List<OCRD> IOCRD.FP_Listar_OCRD(OCRD ObjOCRD, string Compania, string Key)
        {
            IBOCRD OCRDB = null;
            try
            {
                string error;
                if (!this.Validar(Key, Compania, out error)) throw new Exception(error);
                if (ObjOCRD == null) throw new Exception("ObjOCRD vacio");
                if (ObjOCRD.Tipo == null || ObjOCRD.Tipo.Trim() == "") throw new Exception("Tipo vacio");
                OCRDB = new BOCRD(ModVariables.getCompanyConnection(Compania));
                return OCRDB.FP_Listar_OCRD(ObjOCRD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OCRDB = null;
            }
        }

        #endregion

        #region Metodos del objeto OITM

        IList<OITM> IOITM.FP_LISTAR_OITM(string FechaIni, string FechaFin, string Compania, string Key)
        {
            IBOITM OITMB = null;
            try
            {
                string error;
                if (!this.Validar(Key, Compania, out error)) throw new Exception(error);
                if (FechaIni == null || FechaIni.Trim() == "") throw new Exception("FechaIni vacio");
                if (FechaFin == null || FechaFin.Trim() == "") throw new Exception("FechaFin vacio");
                OITMB = new BOITM(ModVariables.getCompanyConnection(Compania));
                return OITMB.FP_LISTAR_OITM(FechaIni, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OITMB = null;
            }
        }

        IList<OITM> IOITM.FP_LISTAR_OITM_TOT(string Compania, string Key)
        {
            IBOITM OITMB = null;
            try
            {
                string error;
                if (!this.Validar(Key, Compania, out error)) throw new Exception(error);
                OITMB = new BOITM(ModVariables.getCompanyConnection(Compania));
                return OITMB.FP_LISTAR_OITM_TOT();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OITMB = null;
            }
        }

        #endregion

        #region Metodos del objeto ORTT

        IList<ORTT> IORTT.FP_LISTAR_ORTT(string date, string Compania, string Key)
        {
            IBORTT ORTTB = null;
            try
            {
                string error;
                if (!this.Validar(Key, Compania, out error)) throw new Exception(error);
                if (date == null || date.Trim() == "") throw new Exception("date vacio");
                ORTTB = new BORTT(ModVariables.getCompanyConnection(Compania));
                return ORTTB.FP_LISTAR_ORTT(date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ORTTB = null;
            }
        }

        #endregion

    }
}
