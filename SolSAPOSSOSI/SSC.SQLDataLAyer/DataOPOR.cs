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
using System.Globalization;

namespace SSC.SQLDataLAyer
{
    public class DataOPOR : IQOPOR
    {
        private string _strCadenaConexion;
        public DataOPOR(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }

        IList<OPOR> IQOPOR.FP_LISTAR_OPOR(string FechaIni, string FechaFin)
        {
            List<OPOR> objResult = null;
            IQPOR1 objPOR1B = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_OrdenesCompraCab", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaIni", FechaIni);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OPOR>();
                            objPOR1B = new DataPOR1(_strCadenaConexion);
                            while (dr.Read())
                            {
                                OPOR mItem = new OPOR();
                                mItem.DocEntry = dr.GetInt32(dr.GetOrdinal("DocEntry"));
                                mItem.LicTradNum = dr.GetString(dr.GetOrdinal("LicTradNum"));
                                mItem.CardName = dr.GetString(dr.GetOrdinal("CardName"));
                                mItem.CurrencyId = dr.GetInt32(dr.GetOrdinal("CurrencyId"));
                                mItem.DocRate = dr.GetDecimal(dr.GetOrdinal("DocRate"));
                                mItem.DocNum = dr.GetInt32(dr.GetOrdinal("DocNum"));
                                mItem.DocDate = dr.GetDateTime(dr.GetOrdinal("DocDate"));
                                mItem.DocDueDate = dr.GetDateTime(dr.GetOrdinal("DocDueDate"));
                                mItem.DocStatus = dr.GetString(dr.GetOrdinal("DocStatus"));
                                mItem.U_RDC_CoEC = dr.GetString(dr.GetOrdinal("U_RDC_CoEC"));
                                mItem.U_RDC_CoECName = dr.GetString(dr.GetOrdinal("U_RDC_CoECName"));
                                mItem.CostTotal = dr.GetDecimal(dr.GetOrdinal("CostTotal"));
                                mItem.DiscTotal = dr.GetDecimal(dr.GetOrdinal("DiscTotal"));
                                mItem.VatSum = dr.GetDecimal(dr.GetOrdinal("VatSum"));
                                mItem.DocTotal = dr.GetDecimal(dr.GetOrdinal("DocTotal"));
                                mItem.DocEntry = dr.GetInt32(dr.GetOrdinal("DocEntry"));
                                mItem.Comments = dr.GetString(dr.GetOrdinal("Comments"));
                                mItem.ListPOR1 = objPOR1B.FP_LISTAR_POR1(mItem.DocEntry).ToList();
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
        ORDR IQOPOR.FP_GET_OINV_TICKET(string Ticket)
        {
            ORDR result = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_TICKET", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Ticket", Ticket);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result = new ORDR();
                                result.DocEntry = dr.GetInt32(dr.GetOrdinal("DocEntry")).ToString();
                                result.DocTotal = double.Parse(dr.GetDecimal(dr.GetOrdinal("DocTotal")).ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
