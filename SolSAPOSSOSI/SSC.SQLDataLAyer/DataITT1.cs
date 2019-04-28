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
    public class DataITT1 : IQITT1
    {
        private string _strCadenaConexion;
        public DataITT1(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<ITT1> IQITT1.FP_LISTAR_ITT1(string ItemFather)
        {
            List<ITT1> objResult = new List<ITT1>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_ITT1", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@itemFather", ItemFather);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<ITT1>();
                            while (dr.Read())
                            {
                                ITT1 mItem = new ITT1();
                                mItem.Father = dr.GetString(dr.GetOrdinal("Father"));
                                //mItem.FatherName = dr.GetString(dr.GetOrdinal("FatherName"));
                                //mItem.ChildNum = dr.GetInt32(dr.GetOrdinal("ChildNum"));
                                mItem.Children = dr.GetString(dr.GetOrdinal("Children"));
                                mItem.ChildrenName = dr.GetString(dr.GetOrdinal("ChildrenName"));
                                mItem.Childrenbuyunitmsr = dr.GetString(dr.GetOrdinal("Childrenbuyunitmsr"));
                                mItem.Quantity = dr.GetDecimal(dr.GetOrdinal("Quantity"));
                                //mItem.Warehouse = dr.GetString(dr.GetOrdinal("Warehouse"));
                                //mItem.Price = dr.GetDecimal(dr.GetOrdinal("Price"));
                                //mItem.Currency = dr.GetString(dr.GetOrdinal("Currency"));
                                //mItem.PriceList = dr.GetInt32(dr.GetOrdinal("PriceList"));
                                //mItem.OrigPrice = dr.GetDecimal(dr.GetOrdinal("OrigPrice"));
                                //mItem.OrigCurr = dr.GetString(dr.GetOrdinal("OrigCurr"));
                                //mItem.IssueMthd = dr.GetString(dr.GetOrdinal("IssueMthd"));
                                //mItem.Uom = dr.GetString(dr.GetOrdinal("Uom"));
                                //mItem.Comment = dr.GetString(dr.GetOrdinal("Comment"));
                                //mItem.LogInstanc = dr.GetInt32(dr.GetOrdinal("LogInstanc"));
                                //mItem.Object = dr.GetString(dr.GetOrdinal("Object"));
                                //mItem.OcrCode = dr.GetString(dr.GetOrdinal("OcrCode"));
                                //mItem.OcrCode2 = dr.GetString(dr.GetOrdinal("OcrCode2"));
                                //mItem.OcrCode3 = dr.GetString(dr.GetOrdinal("OcrCode3"));
                                //mItem.OcrCode4 = dr.GetString(dr.GetOrdinal("OcrCode4"));
                                //mItem.OcrCode5 = dr.GetString(dr.GetOrdinal("OcrCode5"));
                                //mItem.PrncpInput = dr.GetString(dr.GetOrdinal("PrncpInput"));
                                //mItem.Project = dr.GetString(dr.GetOrdinal("Project"));
                                //mItem.Type = dr.GetInt32(dr.GetOrdinal("Type"));
                                //mItem.WipActCode = dr.GetString(dr.GetOrdinal("WipActCode"));
                                //mItem.AddQuantit = dr.GetDecimal(dr.GetOrdinal("AddQuantit"));
                                //mItem.LineText = dr.GetString(dr.GetOrdinal("LineText"));
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
