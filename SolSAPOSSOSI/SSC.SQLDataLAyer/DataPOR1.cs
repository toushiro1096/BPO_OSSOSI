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
    public class DataPOR1 :  IQPOR1
    {
        private string _strCadenaConexion;
        public DataPOR1(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<POR1> IQPOR1.FP_LISTAR_POR1(int DocEntry)
        {
            List<POR1> objResult = null;
            try
            {
                using (SqlConnection cnm = new SqlConnection(_strCadenaConexion))
                {
                    cnm.Open();
                    using (SqlCommand cmd1 = new SqlCommand("SBO_SP_LGS_BUS_OrdenesCompraDet", cnm))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@DocEntry", DocEntry);
                        using (SqlDataReader dr1 = cmd1.ExecuteReader())
                        {
                            objResult = new List<POR1>();
                            while (dr1.Read())
                            {
                                POR1 mItem = new POR1();
                                mItem.WhsCode = dr1.GetInt32(dr1.GetOrdinal("WhsCode"));
                                mItem.ItemCode = dr1.GetString(dr1.GetOrdinal("ItemCode"));
                                mItem.dscription = dr1.GetString(dr1.GetOrdinal("dscription"));
                                mItem.LineNum = dr1.GetInt32(dr1.GetOrdinal("LineNum"));
                                mItem.Quantity = dr1.GetDecimal(dr1.GetOrdinal("Quantity"));
                                mItem.Price = dr1.GetDecimal(dr1.GetOrdinal("Price"));
                                mItem.CurrencyId = dr1.GetInt32(dr1.GetOrdinal("CurrencyId"));
                                mItem.OpenSum = dr1.GetDecimal(dr1.GetOrdinal("OpenSum"));
                                mItem.VatPrcnt = dr1.GetDecimal(dr1.GetOrdinal("VatPrcnt"));
                                mItem.LineVat = dr1.GetDecimal(dr1.GetOrdinal("LineVat"));
                              //  mItem.LineStatus = dr1.GetString(dr1.GetOrdinal("LineStatus"));
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
