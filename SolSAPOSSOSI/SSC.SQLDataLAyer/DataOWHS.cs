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
    public class DataOWHS : IQOWHS
    {
        private string _strCadenaConexion;
        public DataOWHS(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<OWHS> IQOWHS.FP_LISTAR_OWHS()
        {
            List<OWHS> objResult = new List<OWHS>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_Almacenes", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OWHS>();
                            while (dr.Read())
                            {
                                OWHS mItem = new OWHS();
                                mItem.WhsCode = dr.GetString(dr.GetOrdinal("WhsCode"));
                                mItem.WhsName = dr.GetString(dr.GetOrdinal("WhsName"));
                                mItem.Street = dr.GetString(dr.GetOrdinal("Street"));
                                mItem.City = dr.GetString(dr.GetOrdinal("City"));
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
