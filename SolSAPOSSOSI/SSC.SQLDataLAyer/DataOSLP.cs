using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSC.Entity;
using SSC.Interface;
using System.Data.SqlClient;
using System.Data;
using SAPbobsCOM;
using SSC.SBOSDK;

namespace SSC.SQLDataLAyer
{
    public class DataOSLP : IQOSLP
    {
        private string _strCadenaConexion;
        public DataOSLP(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }

        IList<OSLP> IQOSLP.FP_LISTAR_OSLP()
        {
            List<OSLP> objResult = new List<OSLP>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_Empleado", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OSLP>();
                            while (dr.Read())
                            {
                                OSLP mItem = new OSLP();
                                mItem.SlpCode = dr.GetInt32(dr.GetOrdinal("SlpCode"));
                                mItem.SlpNAme = dr.GetString(dr.GetOrdinal("SlpNAme"));
                                mItem.U_RDC_Alma = dr.GetString(dr.GetOrdinal("U_RDC_Alma"));
                                objResult.Add(mItem);
                            }
                        }
                        return objResult;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
