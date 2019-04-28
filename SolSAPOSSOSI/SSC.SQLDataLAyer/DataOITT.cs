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
    public class DataOITT : IQOITT
    {
        private string _strCadenaConexion;
        public DataOITT(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<OITT> IQOITT.FP_LISTAR_OITT()
        {
            List<OITT> objResult = new List<OITT>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_OITT", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OITT>();
                            while (dr.Read())
                            {
                                OITT mItem = new OITT();
                                mItem.Code = dr.GetString(dr.GetOrdinal("Code"));
                                mItem.Name = dr.GetString(dr.GetOrdinal("Name"));
                                //mItem.TreeType = dr.GetString(dr.GetOrdinal("TreeType"));
                                mItem.PriceList = dr.GetInt32(dr.GetOrdinal("PriceList"));
                                mItem.Qauntity = dr.GetDecimal(dr.GetOrdinal("Qauntity"));
                                //mItem.UseFthrWhs = dr.GetString(dr.GetOrdinal("UseFthrWhs"));
                                //mItem.CreateDate = dr.GetDateTime(dr.GetOrdinal("CreateDate"));
                                //mItem.UpdateDate = dr.GetDateTime(dr.GetOrdinal("UpdateDate"));
                                //mItem.Transfered = dr.GetString(dr.GetOrdinal("Transfered"));
                                //mItem.DataSource = dr.GetString(dr.GetOrdinal("DataSource"));
                                //mItem.UserSign = dr.GetInt32(dr.GetOrdinal("UserSign"));
                                //mItem.SCNCounter = dr.GetInt32(dr.GetOrdinal("SCNCounter"));
                                //mItem.DispCurr = dr.GetString(dr.GetOrdinal("DispCurr"));
                                //mItem.ToWH = dr.GetString(dr.GetOrdinal("ToWH"));
                                //mItem.LogInstac = dr.GetInt32(dr.GetOrdinal("LogInstac"));
                                //mItem.UserSign2 = dr.GetInt32(dr.GetOrdinal("UserSign2"));
                                //mItem.OcrCode = dr.GetString(dr.GetOrdinal("OcrCode"));
                                //mItem.HideComp = dr.GetString(dr.GetOrdinal("HideComp"));
                                //mItem.OcrCode2 = dr.GetString(dr.GetOrdinal("OcrCode2"));
                                //mItem.OcrCode3 = dr.GetString(dr.GetOrdinal("OcrCode3"));
                                //mItem.OcrCode4 = dr.GetString(dr.GetOrdinal("OcrCode4"));
                                //mItem.OcrCode5 = dr.GetString(dr.GetOrdinal("OcrCode5"));
                                //mItem.UpdateTime = dr.GetInt32(dr.GetOrdinal("UpdateTime"));
                                //mItem.Project = dr.GetString(dr.GetOrdinal("Project"));
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
