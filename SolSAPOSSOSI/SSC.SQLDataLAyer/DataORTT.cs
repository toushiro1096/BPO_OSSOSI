using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSC.Entity;
using SSC.Interface;
using System.Data.SqlClient;
using System.Data;

namespace SSC.SQLDataLAyer
{
    public class DataORTT : IQORTT
    {
        private string _strCadenaConexion;
        public DataORTT(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }

        IList<ORTT> IQORTT.FP_LISTAR_ORTT(string date)
        {
            List<ORTT> objResult = new List<ORTT>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_BUS_ListaTipoCambio", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@date",date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<ORTT>();
                            while (dr.Read())
                            {
                                ORTT mItem = new ORTT();
                                mItem.RateDate = dr.GetDateTime(dr.GetOrdinal("RateDate")).ToShortDateString();
                                mItem.Currency = dr.GetString(dr.GetOrdinal("Currency"));
                                mItem.Rate = dr.GetDecimal(dr.GetOrdinal("Rate"));
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
