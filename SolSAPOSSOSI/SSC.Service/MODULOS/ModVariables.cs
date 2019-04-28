using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using SSC.SBOSDK;

namespace SSC.Service.MODULOS
{
    public class ModVariables
    {
        private static string _argCadenaConexion = ConfigurationManager.ConnectionStrings["Server"].ToString();
        private static string _Key = ConfigurationManager.AppSettings["Key"].ToString();
        public static string argCadenaConexion
        {
            get { return _argCadenaConexion; }
            set { _argCadenaConexion = value; }
        }

        public static string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        public static bool ValidarKey(string key)
        {
            if (ModelAPP.Desencriptar(key) == ModelAPP.Encriptar(_Key))
                return true;
            else
                return false;
        }
        public static string getCompanyConnection(string compania)
        {
            SqlConnectionStringBuilder Testcnn = new SqlConnectionStringBuilder(argCadenaConexion);
            Testcnn.InitialCatalog = compania;
            return Testcnn.ConnectionString;
        }

    }
}