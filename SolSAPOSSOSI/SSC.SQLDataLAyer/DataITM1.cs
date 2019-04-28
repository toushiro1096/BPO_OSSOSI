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
    public class DataITM1 :  IQITM1
    {
        private string _strCadenaConexion;
        public DataITM1(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<ITM1> IQITM1.FP_LISTAR_ITM1()
        {
            List<ITM1> objResult = new List<ITM1>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_PrecioListaArticulo", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<ITM1>();
                            while (dr.Read())
                            {
                                ITM1 mItem = new ITM1();
                                mItem.ListNum = dr.GetInt32(dr.GetOrdinal("ListNum"));
                                mItem.ListName = dr.GetString(dr.GetOrdinal("LstName"));
                                mItem.ItemCode = dr.GetString(dr.GetOrdinal("ItemCode"));
                                mItem.ItemName = dr.GetString(dr.GetOrdinal("ItemName"));
                                mItem.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                                mItem.CurrenCyId = dr.GetInt32(dr.GetOrdinal("CurrenCyId"));
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
        IList<ITM1> IQITM1.FP_LISTAR_ITM1_Filtrar(int ListNum)
        {
            List<ITM1> objResult = new List<ITM1>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_PrecioListaArticulo_FIltrar", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ListNum", ListNum);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<ITM1>();
                            while (dr.Read())
                            {
                                ITM1 mItem = new ITM1();
                                mItem.ListNum = dr.GetInt32(dr.GetOrdinal("ListNum"));
                                mItem.ListName = dr.GetString(dr.GetOrdinal("LstName"));
                                mItem.ItemCode = dr.GetString(dr.GetOrdinal("ItemCode"));
                                mItem.ItemName = dr.GetString(dr.GetOrdinal("ItemName"));
                                mItem.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                                mItem.CurrenCyId = dr.GetInt32(dr.GetOrdinal("CurrenCyId"));
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
