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
    public class DataOITM : IQOITM
    {
        private string _strCadenaConexion;
        public DataOITM(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<OITM> IQOITM.FP_LISTAR_OITM(string FechaIni, string FechaFin)
        {
            List<OITM> objResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_Articulos", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaIni", FechaIni);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OITM>();
                            while (dr.Read())
                            {
                                OITM mItem = new OITM();
                                mItem.ItemCode = dr.GetString(dr.GetOrdinal("itemCode"));
                                mItem.ItemName = dr.GetString(dr.GetOrdinal("itemName"));
                                mItem.FrgnName = dr.GetString(dr.GetOrdinal("FrgnName"));
                                mItem.BuyUnitMsr = dr.GetString(dr.GetOrdinal("buyunitmsr"));
                                mItem.FirmCode = dr.GetInt32(dr.GetOrdinal("FirmCode"));
                                mItem.FirmName = dr.GetString(dr.GetOrdinal("FirmName"));
                                //mItem.LicTradNum = dr.GetString(dr.GetOrdinal("LicTradNum"));
                                //mItem.CardName = dr.GetString(dr.GetOrdinal("CardName"));
                                mItem.Vigente = dr.GetString(dr.GetOrdinal("Vigente"));
                                mItem.CodigoDep = dr.GetInt16(dr.GetOrdinal("CodigoDep"));
                                mItem.DescDep = dr.GetString(dr.GetOrdinal("DescDep"));
                                mItem.CodigoClase = dr.GetString(dr.GetOrdinal("CodigoClase"));
                                mItem.DescClase = dr.GetString(dr.GetOrdinal("DescClase"));
                                mItem.CodigoSubClase = dr.GetString(dr.GetOrdinal("CodigoSubClase"));
                                mItem.DescSubClase = dr.GetString(dr.GetOrdinal("DescSubClase"));
                                mItem.LastPurPrcS = dr.GetDecimal(dr.GetOrdinal("LastPurPrcS"));
                                mItem.LastPurPrcD = dr.GetDecimal(dr.GetOrdinal("LastPurPrcD"));
                                //mItem.CurrencyID = dr.GetInt32(dr.GetOrdinal("CurrencyID"));
                                mItem.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                                mItem.CodeBars = dr.GetString(dr.GetOrdinal("CodeBars"));
                                mItem.ALU = dr.GetString(dr.GetOrdinal("ALU"));
                                mItem.TypeCode = dr.GetString(dr.GetOrdinal("TypeCode"));
                                //mItem.LstPrecios = new List<ITM1>();
                                //IQITM1 obj = new DataITM1(_strCadenaConexion);
                                //mItem.LstPrecios = obj.FP_LISTAR_ITM1(mItem.ItemCode).ToList();

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

        IList<OITM> IQOITM.FP_LISTAR_OITM_TOT()
        {
            List<OITM> objResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_Articulos_TOT", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OITM>();
                            while (dr.Read())
                            {
                                OITM mItem = new OITM();
                                mItem.ItemCode = dr.GetString(dr.GetOrdinal("itemCode"));
                                mItem.ItemName = dr.GetString(dr.GetOrdinal("itemName"));
                                mItem.FrgnName = dr.GetString(dr.GetOrdinal("FrgnName"));
                                mItem.BuyUnitMsr = dr.GetString(dr.GetOrdinal("buyunitmsr"));
                                mItem.FirmCode = dr.GetInt32(dr.GetOrdinal("FirmCode"));
                                mItem.FirmName = dr.GetString(dr.GetOrdinal("FirmName"));
                                //mItem.LicTradNum = dr.GetString(dr.GetOrdinal("LicTradNum"));
                                //mItem.CardName = dr.GetString(dr.GetOrdinal("CardName"));
                                mItem.Vigente = dr.GetString(dr.GetOrdinal("Vigente"));
                                mItem.CodigoDep = dr.GetInt16(dr.GetOrdinal("CodigoDep"));
                                mItem.DescDep = dr.GetString(dr.GetOrdinal("DescDep"));
                                mItem.CodigoClase = dr.GetString(dr.GetOrdinal("CodigoClase"));
                                mItem.DescClase = dr.GetString(dr.GetOrdinal("DescClase"));
                                mItem.CodigoSubClase = dr.GetString(dr.GetOrdinal("CodigoSubClase"));
                                mItem.DescSubClase = dr.GetString(dr.GetOrdinal("DescSubClase"));
                                mItem.LastPurPrcS = dr.GetDecimal(dr.GetOrdinal("LastPurPrcS"));
                                mItem.LastPurPrcD = dr.GetDecimal(dr.GetOrdinal("LastPurPrcD"));
                                //mItem.CurrencyID = dr.GetInt32(dr.GetOrdinal("CurrencyID"));
                                mItem.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                                mItem.CodeBars = dr.GetString(dr.GetOrdinal("CodeBars"));
                                mItem.ALU = dr.GetString(dr.GetOrdinal("ALU"));
                                mItem.TypeCode = dr.GetString(dr.GetOrdinal("TypeCode"));
                                //mItem.LstPrecios = new List<ITM1>();
                                //IQITM1 obj = new DataITM1(_strCadenaConexion);
                                //mItem.LstPrecios = obj.FP_LISTAR_ITM1(mItem.ItemCode).ToList();

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

        string IQOITM.FP_get_ItemCode(string ItemName)
        {
            string objResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_CodeArticulo", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objResult = dr.GetString(dr.GetOrdinal("itemCode"));
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
