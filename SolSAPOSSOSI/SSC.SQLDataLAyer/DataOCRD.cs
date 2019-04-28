using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSC.Entity;
using SSC.Interface;
using System.Data.SqlClient;
using System.Data;
using SSC.SBOSDK;
using SAPbobsCOM;

namespace SSC.SQLDataLAyer
{
    public class DataOCRD : IQOCRD
    {
        private string _strCadenaConexion;
        public DataOCRD(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }

        List<OCRD> IQOCRD.FP_Listar_OCRD(OCRD ObjOCRD)
        {
            List<OCRD> objResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_LIS_Proveedor", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Tipo", ObjOCRD.Tipo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OCRD>();
                            while (dr.Read())
                            {
                                OCRD item = new OCRD();
                                item.CardCode = dr.GetString(dr.GetOrdinal("CardCode"));
                                item.CardName = dr.GetString(dr.GetOrdinal("CardName"));
                                item.Address = dr.GetString(dr.GetOrdinal("Address"));
                                item.Notes = dr.GetString(dr.GetOrdinal("Notes"));
                                item.LicTradNum = dr.GetString(dr.GetOrdinal("LicTradNum"));
                                item.City = dr.GetString(dr.GetOrdinal("City"));
                                item.County = dr.GetString(dr.GetOrdinal("County"));
                                item.Country = dr.GetString(dr.GetOrdinal("Country"));
                                item.MailCity = dr.GetString(dr.GetOrdinal("MailCity"));
                                item.MailCounty = dr.GetString(dr.GetOrdinal("MailCounty"));
                                item.MailCountr = dr.GetString(dr.GetOrdinal("MailCountr"));
                                item.E_Mail = dr.GetString(dr.GetOrdinal("E_Mail"));
                                objResult.Add(item);
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

        OCRD IQOCRD.FP_BUSCAR_OCRD(OCRD ObjOCRD)
        {
            OCRD objResult = new OCRD();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_Proveedor", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CardCode", ObjOCRD.LicTradNum);
                        cmd.Parameters.AddWithValue("@Tipo", ObjOCRD.Tipo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                objResult = new OCRD();
                                objResult.CardCode = dr.GetString(dr.GetOrdinal("CardCode"));
                                objResult.CardName = dr.GetString(dr.GetOrdinal("CardName"));
                                objResult.Address = dr.GetString(dr.GetOrdinal("Address"));
                                objResult.Notes = dr.GetString(dr.GetOrdinal("Notes"));
                                objResult.LicTradNum = dr.GetString(dr.GetOrdinal("LicTradNum"));
                                objResult.City = dr.GetString(dr.GetOrdinal("City"));
                                objResult.County = dr.GetString(dr.GetOrdinal("County"));
                                objResult.Country = dr.GetString(dr.GetOrdinal("Country"));
                                objResult.MailCity = dr.GetString(dr.GetOrdinal("MailCity"));
                                objResult.MailCounty = dr.GetString(dr.GetOrdinal("MailCounty"));
                                objResult.MailCountr = dr.GetString(dr.GetOrdinal("MailCountr"));
                                objResult.E_Mail = dr.GetString(dr.GetOrdinal("E_Mail"));
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
