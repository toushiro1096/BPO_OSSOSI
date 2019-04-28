using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbobsCOM;

namespace SSC.SBOSDK
{
    public class AppSAPbobsCOM
    {
        public static Company oCompany = null;

        public AppSAPbobsCOM()
        {
        }

        public static void InicializarCompany(string pCompany)
        {
            try
            {
                oCompany = new Company();
                oCompany.Server = @"SIGLOBDPE\SU1"; // @"SIGPEBD\SIGPEBD";// "169.54.164.162\\su1";//MODULOS.ModVariables.Server;
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                oCompany.UserName = @"sigloperu\desarro";//"interfaz";// managerap @siglope.com //SIGPET1
                oCompany.Password = "Asdf123$";// "Passw0rd!";
                oCompany.DbUserName = "desa";// "sa";// MODULOS.ModVariables.DbUserName;
                oCompany.DbPassword = "Asdf123$";// "Passw0rd";
                oCompany.CompanyDB = pCompany;
                oCompany.UseTrusted = false;
                oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                oCompany.LicenseServer = "sigloappe:30000";// "SIGPET1:30000"; //"SIGPET1:30000";

                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                if (oCompany.Connect() != 0)
                {
                    throw new Exception(oCompany.GetLastErrorDescription());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void InicializarCompany()
        {
            try
            {
                oCompany = new Company();
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                oCompany.DbUserName = "sa";
                oCompany.DbPassword = "$$SigloPeru2015!";
                oCompany.Server = "169.54.164.164\\su2";// "169.54.164.162\\su1";// "169.54.164.164\\su2";//"10.107.166.222\\su1";// "169.54.164.162\\su1";// "169.54.164.164\\su2";//"10.107.166.250\\su2"
                oCompany.CompanyDB = "PE_SBO_RDC_CACAOSOURCE_D2";//"PE_SBO_RDC_CACAOSOURCE_TEST20";"PE_SBO_RDC_CACAOSURCE"
                //oCompany.CompanyDB = "PE_SBO_RDC_CACAOSURCE";
                oCompany.UserName = "siglobpoperu\\B1cadm";
                oCompany.Password = "$$SigloPeru2015!";
                oCompany.LicenseServer = "169.54.164.172:30000";// "10.107.166.249:30000";// "169.54.164.172:30000";
                oCompany.UseTrusted = false;

                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
                if (oCompany.Connect() != 0)
                {
                    throw new Exception(oCompany.GetLastErrorDescription());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static Company ObtenerCompany(string Company)
        {
            InicializarCompany(Company);
            return oCompany;
        }
    }
}
