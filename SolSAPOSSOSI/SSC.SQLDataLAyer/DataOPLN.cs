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
    public class DataOPLN : IQOPLN
    {
        private string _strCadenaConexion;
        public DataOPLN(string ArgCadenaConeccion)
        {
            _strCadenaConexion = ArgCadenaConeccion;
        }
        IList<OPLN> IQOPLN.FP_LISTAR_OPLN()
        {
            List<OPLN> objResult = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(_strCadenaConexion))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand("SBO_SP_LGS_BUS_ListaPrecio", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            objResult = new List<OPLN>();
                            while (dr.Read())
                            {
                                OPLN mItem = new OPLN();
                                mItem.ListNum = dr.GetInt16(dr.GetOrdinal("ListNum"));
                                mItem.ListName = dr.GetString(dr.GetOrdinal("ListName"));
                                mItem.BASE_NUM = dr.GetInt16(dr.GetOrdinal("BASE_NUM"));
                                mItem.Factor = dr.GetDecimal(dr.GetOrdinal("Factor"));
                                mItem.RoundSys = dr.GetInt16(dr.GetOrdinal("RoundSys"));
                                mItem.GroupCode = dr.GetInt16(dr.GetOrdinal("GroupCode"));
                                mItem.DataSource = dr.GetString(dr.GetOrdinal("DataSource"));
                                mItem.SPPCounter = dr.GetInt32(dr.GetOrdinal("SPPCounter"));
                                mItem.UserSign = dr.GetInt16(dr.GetOrdinal("UserSign"));
                                mItem.IsGrossPrc = dr.GetString(dr.GetOrdinal("IsGrossPrc"));
                                mItem.LogInstanc = dr.GetInt32(dr.GetOrdinal("LogInstanc"));
                                mItem.UserSign2 = dr.GetInt16(dr.GetOrdinal("UserSign2"));
                                mItem.UpdateDate = dr.GetDateTime(dr.GetOrdinal("UpdateDate"));
                                mItem.ValidFor = dr.GetString(dr.GetOrdinal("ValidFor"));
                                mItem.ValidFrom = dr.GetDateTime(dr.GetOrdinal("ValidFrom"));
                                mItem.ValidTo = dr.GetDateTime(dr.GetOrdinal("ValidTo"));
                                mItem.CreateDate = dr.GetDateTime(dr.GetOrdinal("CreateDate"));
                                mItem.PrimCurr = dr.GetString(dr.GetOrdinal("PrimCurr"));
                                mItem.AddCurr1 = dr.GetString(dr.GetOrdinal("AddCurr1"));
                                mItem.AddCurr2 = dr.GetString(dr.GetOrdinal("AddCurr2"));
                                mItem.RoundRule = dr.GetString(dr.GetOrdinal("RoundRule"));
                                mItem.ExtAmount = dr.GetDecimal(dr.GetOrdinal("ExtAmount"));
                                mItem.RndFrmtInt = dr.GetString(dr.GetOrdinal("RndFrmtInt"));
                                mItem.RndFrmtDec = dr.GetString(dr.GetOrdinal("RndFrmtDec"));
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
