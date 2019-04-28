using Microsoft.Web.Administration;
using SSC.Entity;
using SSC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SAPbobsCOM;
using SSC.SBOSDK;
using System.Globalization;
using SSC.Service.MODULOS;
using System.Configuration;
using System.ComponentModel;
using Newtonsoft.Json;
using SSC.ILogic;
using SSC.Logic;

namespace SSC.Service.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceI" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceI.svc o ServiceI.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceI : Iconfig, SORDR, SOCRD, SOPOR, SOJDT
    {
        #region Variables

        private SAPbobsCOM.Company oCompany;

        #endregion

        #region Metodos del objeto Auxliares

        private bool Validar(string texto)
        {
            return ModVariables.ValidarKey(texto);
        }

        void iniciarSAPCompany(string Company)
        {
            oCompany = new Company();
            try
            {
                oCompany = AppSAPbobsCOM.ObtenerCompany(Company);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        void FinalizarSAPCómpany()
        {
            try
            {
                oCompany.Disconnect();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        void RecycleAppPools()
        {
            try
            {
                ServerManager serverManager = new ServerManager();
                ApplicationPool appPool = serverManager.ApplicationPools["ServiceSAPOSSOSI"];// serverManager.ApplicationPools["ASP.NET V4 32B Desarrollo"];
                if (appPool != null)
                {
                    if (appPool.State == ObjectState.Stopped)
                    {
                        appPool.Start();
                    }
                    else
                    {
                        appPool.Recycle();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void Iconfig.RefreshMemory()
        {
            for (int i = 0; i <= 10; i++)
            {
                RecycleAppPools();
            }
        }

        #endregion

        #region Metodos del objeto ORDR

        int validarVentaJrnlmemo(ORDR ObjORDR)
        {
            int result = 0;
            try
            {
                string Query = string.Format("select count(DocNum) as val from OINV where JrnlMemo = '{0}' and CANCELED='N'", ObjORDR.JrnlMemo);

                SAPbobsCOM.Recordset Reco = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                Reco.DoQuery(Query);
                Reco.MoveFirst();
                for (int k = 0; k == (Reco.RecordCount - 1); k++)
                {
                    result = Reco.Fields.Item(0).Value;
                    Reco.MoveNext();
                }
            }
            catch (Exception ex)
            {
                result = 2;
                throw ex;
            }
            return result;
        }

        List<CError> SORDR.FP_Solicitud_Venta_Masiva_ORDR(List<ORDR> LstORDR, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (LstORDR == null) throw new Exception("Parametro LstORDR null");
                if (!this.Validar(Key))
                {
                    LstResult.Add(new CError(-1, "Llave Incorrecta", "", "", ""));
                    return LstResult;
                }
                if (LstORDR.Count == 0)
                {
                    LstResult.Add(new CError(-1, "No hay Registros en el parametro LstORDR", "", "", ""));
                }

                //RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;


                foreach (ORDR ObjORDR in LstORDR)
                {
                    try
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORDR));
                        ObjResult = new CError(ObjORDR.JrnlMemo, ObjORDR.CodigoExterno);

                        int sr = this.validarVentaJrnlmemo(ObjORDR);
                        if (ObjORDR == null)
                        {
                            ObjResult.Result = 10;
                            ObjResult.Error = "Objeto ORDR Vacio";
                        }
                        else if (ObjORDR.DocDate == null || ObjORDR.DocDate == "")
                        {
                            ObjResult.Result = 11;
                            ObjResult.Error = "Falta Fecha del Documento";
                        }
                        else if (ObjORDR.CardCode == null || ObjORDR.CardCode == "")
                        {
                            ObjResult.Result = 12;
                            ObjResult.Error = "Falta codigo del Cliente";
                        }
                        else if (ObjORDR.LstRDR1 == null || ObjORDR.LstRDR1.Count == 0)
                        {
                            ObjResult.Result = 15;
                            ObjResult.Error = "No hay detalle del Documento (Aritculos)";
                        }
                        else if (sr == 2)
                        {
                            ObjResult.Result = 16;
                            ObjResult.Error = "No se pudo Validar si existia el documento (No se Ingreso)";
                        }
                        else if (sr == 1)
                        {
                            ObjResult.Result = 17;
                            ObjResult.Error = "Documento ya ingresado";
                        }
                        else if (ObjORDR.LstCampoUsuario == null || ObjORDR.LstCampoUsuario.Count == 0)
                        {
                            ObjResult.Result = 15;
                            ObjResult.Error = "No ha asignado los campos definidos por usuario";
                        }
                        else
                        {
                            SAPbobsCOM.Documents oorder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                            oorder.CardCode = ObjORDR.CardCode;
                            oorder.CardName = ObjORDR.CardName;
                            oorder.DocDate = Convert.ToDateTime(ObjORDR.DocDate, new CultureInfo("ru-RU"));
                            oorder.TaxDate = Convert.ToDateTime(ObjORDR.TaxDate, new CultureInfo("ru-RU"));
                            //oorder.DocObjectCode = BoObjectTypes.oInvoices;
                            oorder.DocType = BoDocumentTypes.dDocument_Service;
                            oorder.HandWritten = BoYesNoEnum.tNO;
                            oorder.Rounding = BoYesNoEnum.tYES;
                            //oorder.DocTime = Convert.ToDateTime(ObjORDR.DocDate, new CultureInfo("ru-RU"));


                            /*
                            if (ObjORDR.CodAlmacen.Substring(0, 2) == "11")
                                oorder.Series = 4;
                            else if (ObjORDR.CodAlmacen.Substring(0, 2) == "15")
                                oorder.Series = 68;
                            */
                            oorder.NumAtCard = string.Format("{0}-{1}-{2}", ObjORDR.U_LGS_TDOC, ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);

                            oorder.DocDueDate = Convert.ToDateTime(ObjORDR.DocDueDate, new CultureInfo("ru-RU"));
                            oorder.SalesPersonCode = ObjORDR.SalesPersonCode;
                            oorder.DocTotal = ObjORDR.DocTotal;
                            oorder.Comments = string.Format("{0} {1}-{2}-{3}", ObjORDR.Comments, ObjORDR.U_LGS_TDOC, ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);
                            //oorder.JournalMemo = ObjORDR.JrnlMemo;
                            foreach (var item in ObjORDR.LstCampoUsuario)
                            {
                                oorder.UserFields.Fields.Item(item.Nombre).Value = CampoUsuario.getValueOnType(item);
                            }

                            //oorder.UserFields.Fields.Item("U_GS_Turno").Value = "2654";
                            //oorder.UserFields.Fields.Item("U_GS_EsExt").Value = ObjORDR.U_GS_EsExt;
                            //oorder.UserFields.Fields.Item("U_GS_NAC").Value = ObjORDR.U_GS_NAC;
                            //oorder.UserFields.Fields.Item("U_LGS_TDOC").Value = ObjORDR.U_LGS_TDOC;
                            //oorder.UserFields.Fields.Item("U_LGS_SDOC").Value = ObjORDR.U_LGS_SDOC;
                            //oorder.UserFields.Fields.Item("U_LGS_CDOC").Value = ObjORDR.U_LGS_NUCE;
                            //oorder.UserFields.Fields.Item("U_RDC_EfSo").Value = ObjORDR.U_RDC_EfSo;
                            //oorder.UserFields.Fields.Item("U_RDC_EfDo").Value = ObjORDR.U_RDC_EfDo;
                            //oorder.UserFields.Fields.Item("U_RDC_EnSo").Value = ObjORDR.U_RDC_EnSo;
                            //oorder.UserFields.Fields.Item("U_RDC_EnDo").Value = ObjORDR.U_RDC_EnDo;
                            //oorder.UserFields.Fields.Item("U_RDC_VuSo").Value = ObjORDR.U_RDC_VuSo;
                            //oorder.UserFields.Fields.Item("U_RDC_VuDo").Value = ObjORDR.U_RDC_VuDo;
                            //oorder.UserFields.Fields.Item("U_RDC_MonV").Value = ObjORDR.U_RDC_MonV;
                            //oorder.UserFields.Fields.Item("U_RDC_TjSo").Value = ObjORDR.U_RDC_TjSo;
                            //oorder.UserFields.Fields.Item("U_RDC_TjDo").Value = ObjORDR.U_RDC_TjDo;
                            //oorder.UserFields.Fields.Item("U_LGS_TOPE").Value = "01";
                            //oorder.UserFields.Fields.Item("U_LGS_TIPO").Value = ObjORDR.U_LGS_TIPO == ORDR.TIPOMOV.VEN ? "VEN" : "DEG";


                            foreach (var item in ObjORDR.LstRDR1)
                            {
                                //oorder.Lines.ItemCode = item.ItemCode;
                                oorder.Lines.ItemDescription = item.ItemDescription;
                                oorder.Lines.LineTotal = item.Price;
                                oorder.Lines.AccountCode = item.AccountCode;
                                //oorder.Lines.DiscountPercent = item.DiscountPercent;
                                oorder.Lines.TaxCode = item.TaxCode; // IGV IGV_EXE

                                //oorder.Lines.WarehouseCode = item.WarehouseCode;
                                oorder.Lines.Quantity = item.Quantity;
                                //oorder.Lines.SalesPersonCode = 2;
                                oorder.Lines.CostingCode = item.CostingCode;
                                oorder.Lines.Add();
                            }
                            #region comment
                            /*
                            // Datos de Pago en Soles ( Tarjeta o Efectivo)
                            SAPbobsCOM.Payments oPaymentS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                            oPaymentS.CardCode = ObjORDR.CardCode;
                            oPaymentS.DocDate = Convert.ToDateTime(ObjORDR.DocDate, new CultureInfo("ru-RU"));
                            oPaymentS.DocTypte = SAPbobsCOM.BoRcptTypes.rCustomer;

                            //oPaymentS.UserFields.Fields.Item("PayNoDoc").Value = "N";
                            if (ObjORDR.U_RDC_EfSo != 0)
                            {
                                oPaymentS.DocCurrency = "S/";
                                oPaymentS.CashSum = ObjORDR.U_RDC_EfSo;
                                if (ObjORDR.CodAlmacen == "1100") // AEROPUERTO LIMA - PRINCIPAL
                                    oPaymentS.CashAccount = "1011101";
                                else if (ObjORDR.CodAlmacen == "1500") // CHOCOLAT - PRINCIPAL
                                    oPaymentS.CashAccount = "1011105";
                            }

                            if (ObjORDR.U_RDC_TjSo != 0)
                            {
                                foreach (var itemTarS in ObjORDR.LstRCT3)
                                {
                                    if (itemTarS.Amount != 0)
                                    {
                                        if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "AMEX")  // AEROPUERTO LIMA - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 1;
                                            oPaymentS.CreditCards.CreditAcct = "1032101";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "AMEX")  // CHOCOLAT - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 2;
                                            oPaymentS.CreditCards.CreditAcct = "1032102";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "DINNERS")  // AEROPUERTO LIMA - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 5;
                                            oPaymentS.CreditCards.CreditAcct = "1032101";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "DINNERS")  // CHOCOLAT - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 6;
                                            oPaymentS.CreditCards.CreditAcct = "1032102";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "MASTER")  // AEROPUERTO LIMA - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 9;
                                            oPaymentS.CreditCards.CreditAcct = "1032101";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "MASTER")  // CHOCOLAT - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 10;
                                            oPaymentS.CreditCards.CreditAcct = "1032102";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "VISA")  // AEROPUERTO LIMA - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 13;
                                            oPaymentS.CreditCards.CreditAcct = "1032101";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "VISA")  // CHOCOLAT - PRINCIPAL
                                        {
                                            oPaymentS.CreditCards.CreditCard = 14;
                                            oPaymentS.CreditCards.CreditAcct = "1032102";
                                        }

                                        oPaymentS.CreditCards.CardValidUntil = Convert.ToDateTime(itemTarS.CardValidUntil, new CultureInfo("ru-RU"));
                                        oPaymentS.CreditCards.CreditCardNumber = itemTarS.CreditCardNumber;
                                        if (itemTarS.VoucherNum != "")
                                            oPaymentS.CreditCards.VoucherNum = itemTarS.VoucherNum;
                                        else
                                            oPaymentS.CreditCards.VoucherNum = string.Format("{0}-{1}", ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);
                                        oPaymentS.CreditCards.CreditSum = itemTarS.Amount;
                                        oPaymentS.CreditCards.Add();
                                    }
                                }
                            }
                            // Datos de Pago en DOlares ( Tarjeta o Efectivo)
                            SAPbobsCOM.Payments oPaymentD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);

                            oPaymentD.CardCode = ObjORDR.CardCode;
                            oPaymentD.DocDate = Convert.ToDateTime(ObjORDR.DocDate, new CultureInfo("ru-RU"));
                            oPaymentD.DocTypte = SAPbobsCOM.BoRcptTypes.rCustomer;
                            //oPaymentD.UserFields.Fields.Item("PayNoDoc").Value = "N";

                            if (ObjORDR.U_RDC_EfDo != 0)
                            {
                                oPaymentD.DocCurrency = "US$";
                                oPaymentD.CashSum = ObjORDR.U_RDC_EfDo;

                                if (ObjORDR.CodAlmacen == "1100")  // AEROPUERTO LIMA - PRINCIPAL
                                    oPaymentD.CashAccount = "1011101";
                                else if (ObjORDR.CodAlmacen == "1500") // CHOCOLAT - PRINCIPAL
                                    oPaymentD.CashAccount = "1011105";
                            }

                            if (ObjORDR.U_RDC_TjDo != 0)
                            {
                                foreach (var itemTarD in ObjORDR.LstRCT3)
                                {
                                    if (itemTarD.AmountFC != 0)
                                    {
                                        if (ObjORDR.CodAlmacen == "1100" && itemTarD.CreditCard == "VISA")
                                        {
                                            oPaymentD.CreditCards.CreditCard = 17;
                                            oPaymentD.CreditCards.CreditAcct = "1032101";
                                        }
                                        else if (ObjORDR.CodAlmacen == "1500" && itemTarD.CreditCard == "VISA")
                                        {
                                            oPaymentD.CreditCards.CreditCard = 18;
                                            oPaymentD.CreditCards.CreditAcct = "1032202";
                                        }

                                        oPaymentD.CreditCards.CardValidUntil = Convert.ToDateTime(itemTarD.CardValidUntil, new CultureInfo("ru-RU"));
                                        oPaymentD.CreditCards.CreditCardNumber = itemTarD.CreditCardNumber;
                                        if (itemTarD.VoucherNum != "")
                                            oPaymentD.CreditCards.VoucherNum = itemTarD.VoucherNum;
                                        else
                                            oPaymentD.CreditCards.VoucherNum = string.Format("{0}-{1}", ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);
                                        oPaymentD.CreditCards.CreditSum = itemTarD.AmountFC;
                                        oPaymentD.CreditCards.Add();
                                    }
                                }
                            }
                             * 
                             */
                            #endregion

                            ObjResult.Result = oorder.Add();
                            ObjResult.DocEntry = oCompany.GetNewObjectKey().ToString();
                            if (ObjResult.Result != 0)
                            {
                                ObjResult.Error = oCompany.GetLastErrorDescription();
                            }

                            /*
                        else
                        {
                            string OrdCodeStr;
                            oCompany.GetNewObjectCode(out OrdCodeStr);
                            int err = 0;
                            if (ObjORDR.U_RDC_EfSo != 0 || ObjORDR.U_RDC_TjSo != 0)
                            {

                                oPaymentS.Invoices.DocEntry = int.Parse(OrdCodeStr);

                                oPaymentS.Invoices.SumApplied = ObjORDR.U_RDC_EfSo + ObjORDR.U_RDC_TjSo;
                                oPaymentS.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                                err = oPaymentS.Add();
                            }
                            if (err != 0)
                                ObjResult.Error = oCompany.GetLastErrorDescription();

                            if (ObjORDR.U_RDC_EfDo != 0 || ObjORDR.U_RDC_TjDo != 0)
                            {


                                oPaymentD.Invoices.DocEntry = int.Parse(OrdCodeStr);
                                oPaymentD.Invoices.AppliedFC = ObjORDR.U_RDC_EfDo + ObjORDR.U_RDC_TjDo;

                                oPaymentD.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                                err = oPaymentD.Add();

                            }

                            if (err != 0)
                                ObjResult.Error = oCompany.GetLastErrorDescription();

                        }*/
                        }
                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORDR), 1, exx.Message.ToString());
                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }
            return LstResult;
        }

        List<CError> SORDR.FP_Nota_Credito_ORDR(List<ORDR> LstORDR, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (LstORDR == null) throw new Exception("Parametro LstORDR null");
                if (!this.Validar(Key))
                {
                    LstResult.Add(new CError(-1, "Llave Incorrecta", "", "", ""));
                    return LstResult;
                }
                if (LstORDR.Count == 0)
                {
                    LstResult.Add(new CError(-1, "No hay Registros en el parametro LstORDR", "", "", ""));
                }

                RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;


                foreach (ORDR ObjORDR in LstORDR)
                {
                    try
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORDR));
                        ObjResult = new CError(ObjORDR.JrnlMemo, ObjORDR.CodigoExterno);

                        int sr = this.validarVentaJrnlmemo(ObjORDR);
                        if (ObjORDR == null)
                        {
                            ObjResult.Result = 10;
                            ObjResult.Error = "Objeto ORDR Vacio";
                        }
                        else if (ObjORDR.DocDate == null || ObjORDR.DocDate == "")
                        {
                            ObjResult.Result = 11;
                            ObjResult.Error = "Falta Fecha del Documento";
                        }
                        else if (ObjORDR.CardCode == null || ObjORDR.CardCode == "")
                        {
                            ObjResult.Result = 12;
                            ObjResult.Error = "Falta codigo del Cliente";
                        }
                        else if (ObjORDR.LstRDR1 == null || ObjORDR.LstRDR1.Count == 0)
                        {
                            ObjResult.Result = 15;
                            ObjResult.Error = "No hay detalle del Documento (Aritculos)";
                        }
                        else if (sr == 2)
                        {
                            ObjResult.Result = 16;
                            ObjResult.Error = "No se pudo Validar si existia el documento (No se Ingreso)";
                        }
                        else if (sr == 1)
                        {
                            ObjResult.Result = 17;
                            ObjResult.Error = "Documento ya ingresado";
                        }
                        else if (ObjORDR.LstCampoUsuario == null || ObjORDR.LstCampoUsuario.Count == 0)
                        {
                            ObjResult.Result = 15;
                            ObjResult.Error = "No ha asignado los campos definidos por usuario";
                        }
                        else
                        {
                            SAPbobsCOM.Documents oorder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oCreditNotes);
                            oorder.CardCode = ObjORDR.CardCode;
                            oorder.CardName = ObjORDR.CardName;
                            oorder.DocDate = Convert.ToDateTime(ObjORDR.DocDate, new CultureInfo("ru-RU"));
                            oorder.TaxDate = Convert.ToDateTime(ObjORDR.TaxDate, new CultureInfo("ru-RU"));
                            oorder.DocType = BoDocumentTypes.dDocument_Service;
                            oorder.HandWritten = BoYesNoEnum.tNO;
                            oorder.Rounding = BoYesNoEnum.tYES;
                            oorder.NumAtCard = string.Format("{0}-{1}-{2}", ObjORDR.U_LGS_TDOC, ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);

                            oorder.SalesPersonCode = ObjORDR.SalesPersonCode;
                            oorder.DocTotal = ObjORDR.DocTotal;
                            oorder.Comments = string.Format("{0} {1}-{2}-{3}", ObjORDR.Comments, ObjORDR.U_LGS_TDOC, ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);
                            foreach (var item in ObjORDR.LstCampoUsuario)
                            {
                                oorder.UserFields.Fields.Item(item.Nombre).Value = CampoUsuario.getValueOnType(item);
                            }

                            foreach (var item in ObjORDR.LstRDR1)
                            {
                                oorder.Lines.ItemDescription = item.ItemDescription;
                                oorder.Lines.LineTotal = item.Price;
                                oorder.Lines.AccountCode = item.AccountCode;
                                oorder.Lines.TaxCode = item.TaxCode; // IGV IGV_EXE

                                oorder.Lines.Quantity = item.Quantity;
                                oorder.Lines.CostingCode = item.CostingCode;
                                oorder.Lines.Add();
                            }

                            ObjResult.Result = oorder.Add();
                            ObjResult.DocEntry = oCompany.GetNewObjectKey().ToString();
                            if (ObjResult.Result != 0)
                            {
                                ObjResult.Error = oCompany.GetLastErrorDescription();
                            }

                        }
                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORDR), 1, exx.Message.ToString());
                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }
            return LstResult;
        }

        List<CError> SORDR.FP_Guia_Remision(List<ORDR> LstORDR, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (LstORDR == null) throw new Exception("Parametro LstORDR null");
                if (!this.Validar(Key))
                {
                    LstResult.Add(new CError(-1, "Llave Incorrecta", "", "", ""));
                    return LstResult;
                }
                if (LstORDR.Count == 0)
                {
                    LstResult.Add(new CError(-1, "No hay Registros en el parametro LstORDR", "", "", ""));
                }

                //RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;


                foreach (ORDR ObjORDR in LstORDR)
                {
                    try
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORDR));
                        ObjResult = new CError(ObjORDR.JrnlMemo, ObjORDR.CodigoExterno);


                        if (ObjORDR == null)
                        {
                            ObjResult.Result = 10;
                            ObjResult.Error = "Objeto ORDR Vacio";
                        }
                        else if (ObjORDR.DocDate == null || ObjORDR.DocDate == "")
                        {
                            ObjResult.Result = 11;
                            ObjResult.Error = "Falta Fecha del Documento";
                        }
                        else if (ObjORDR.CardCode == null || ObjORDR.CardCode == "")
                        {
                            ObjResult.Result = 12;
                            ObjResult.Error = "Falta codigo del Cliente";
                        }
                        else if (ObjORDR.LstRDR1 == null || ObjORDR.LstRDR1.Count == 0)
                        {
                            ObjResult.Result = 15;
                            ObjResult.Error = "No hay detalle del Documento (Aritculos)";
                        }
                        else
                        {
                            SAPbobsCOM.Documents oOPOR = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                            oOPOR.DocObjectCode = (SAPbobsCOM.BoObjectTypes.oDeliveryNotes);
                            //oOPOR.Confirmed = BoYesNoEnum.tYES;
                            oOPOR.DocDate = Convert.ToDateTime(ObjORDR.DocDate, new CultureInfo("ru-RU"));
                            oOPOR.TaxDate = Convert.ToDateTime(ObjORDR.TaxDate, new CultureInfo("ru-RU"));
                            oOPOR.JournalMemo = ObjORDR.JrnlMemo;

                            oOPOR.Comments = ObjORDR.Comments;
                            oOPOR.CardCode = ObjORDR.CardCode;
                            oOPOR.Reference2 = ObjORDR.Reference2;

                            if (ObjORDR.LstCampoUsuario.Count > 0)
                            {
                                foreach (var item in ObjORDR.LstCampoUsuario)
                                {
                                    oOPOR.UserFields.Fields.Item(item.Nombre).Value = CampoUsuario.getValueOnType(item);
                                }
                            }
                            foreach (var item in ObjORDR.LstRDR1)
                            {
                                item.ItemCode = this.getItemCode(item.ItemDescription, Compania);
                                if (string.IsNullOrEmpty(item.ItemCode))
                                    throw new Exception("No se encontro el codigo del articulo: " + item.ItemDescription + " /Error-siglo");
                                ObjResult.Error2 = item.ItemCode;
                                oOPOR.Lines.TaxCode = "IGV";//
                                oOPOR.Lines.ItemCode = item.ItemCode;
                                oOPOR.Lines.Quantity = item.Quantity;
                                oOPOR.Lines.Price = item.Price;

                                //oOPOR.Lines.AccountCode = item.AccountCode;
                                oOPOR.Lines.Add();
                            }
                            //ObjResult.Result = oOPOR.Add();// oOPOR.Add();

                            if (ObjResult.Result != 0)
                            {
                                ObjResult.Error = oCompany.GetLastErrorDescription();
                            }
                            else
                            {
                                ObjResult.DocEntry = oCompany.GetNewObjectKey();
                            }

                        }
                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORDR), 1, exx.Message.ToString());
                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }
            return LstResult;
        }

        #endregion

        #region Metodos del objeto OCRD

        List<CError> SOCRD.SP_INSERTAR_OCRD(List<OCRD> LstOCRD, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (LstOCRD == null) throw new Exception("Parametro LstOCRD null");
                if (!this.Validar(Key))
                {
                    LstResult.Add(new CError(-1, "Llave Incorrecta", "", "", ""));
                    return LstResult;
                }
                if (LstOCRD.Count == 0)
                {
                    LstResult.Add(new CError(-1, "No hay Registros en el parametro LstOCRD", "", "", ""));
                    return LstResult;
                }
                RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;

                foreach (OCRD ObjOCRD in LstOCRD)
                {
                    try
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjOCRD));

                        ObjResult = new CError(ObjOCRD.JrnlMemo, ObjOCRD.CodigoExterno);
                        if (ObjOCRD == null)
                        {
                            ObjResult.Result = 10;
                            ObjResult.Error = "objeto ";
                        }
                        else if (ObjOCRD.CardName == null || ObjOCRD.CardName == "")
                        {
                            ObjResult.Result = 13;
                            ObjResult.Error = "";
                        }
                        else if (ObjOCRD.LicTradNum == null || ObjOCRD.LicTradNum == "")
                        {
                            ObjResult.Result = 14;
                            ObjResult.Error = "";
                        }
                        else
                        {
                            SAPbobsCOM.BusinessPartners oBusinessParteners = (SAPbobsCOM.BusinessPartners)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                            ObjResult.DocEntry = this.FP_Generar_CardCode(ObjOCRD.CardType, ObjOCRD.U_GS_EsExt, ObjOCRD.TipDoc.ToString(), ObjOCRD.LicTradNum, 100);
                            oBusinessParteners.CardCode = ObjResult.DocEntry;
                            oBusinessParteners.CardName = ObjOCRD.CardName;
                            oBusinessParteners.CardType = ObjOCRD.CardType == "P" ? BoCardTypes.cSupplier : BoCardTypes.cCustomer;
                            oBusinessParteners.GroupCode = ObjOCRD.GroupCode;
                            oBusinessParteners.UserFields.Fields.Item("LicTradNum").Value = ObjOCRD.LicTradNum != "99999999999" ? ObjOCRD.LicTradNum : ObjResult.DocEntry.Substring(2, 11);

                            foreach (var item in ObjOCRD.LstCampoUsuario)
                            {
                                oBusinessParteners.UserFields.Fields.Item(item.Nombre).Value = CampoUsuario.getValueOnType(item);
                            }

                            //oBusinessParteners.UserFields.Fields.Item("U_GS_EsExt").Value = ObjOCRD.U_GS_EsExt;
                            //oBusinessParteners.UserFields.Fields.Item("U_LGS_BPTP").Value = ObjOCRD.TipoPersona == "N" ? "TPN" : ObjOCRD.TipoPersona == "J" ? "TPJ" : "SND";
                            //oBusinessParteners.UserFields.Fields.Item("U_LGS_BPTD").Value = ObjOCRD.TipDoc.ToString();
                            //if (ObjOCRD.TipoPersona == "N")
                            //{
                            //    oBusinessParteners.CardName = string.Format("{0} {1}, {2} {3}", ObjOCRD.ApellidoP, ObjOCRD.ApellidoM, ObjOCRD.Nombre1, ObjOCRD.Nombre2);
                            //    oBusinessParteners.UserFields.Fields.Item("U_LGS_BPNO").Value = ObjOCRD.Nombre1;
                            //    oBusinessParteners.UserFields.Fields.Item("U_LGS_BPN2").Value = ObjOCRD.Nombre2;
                            //    oBusinessParteners.UserFields.Fields.Item("U_LGS_BPAP").Value = ObjOCRD.ApellidoP;
                            //    oBusinessParteners.UserFields.Fields.Item("U_LGS_BPAM").Value = ObjOCRD.ApellidoM;
                            //}
                            oBusinessParteners.Currency = "##";
                            //oBusinessParteners.City = ObjOCRD.City;
                            //oBusinessParteners.Country = ObjOCRD.Country.ToUpper();
                            //oBusinessParteners.County = ObjOCRD.County;
                            //oBusinessParteners.Address = ObjOCRD.Address;
                            //oBusinessParteners.EmailAddress = ObjOCRD.E_Mail;
                            //oBusinessParteners.MailCity = ObjOCRD.MailCity;
                            //oBusinessParteners.MailCountry = ObjOCRD.MailCountr;
                            //oBusinessParteners.MailCounty = ObjOCRD.MailCounty;
                            //oBusinessParteners.Notes = ObjOCRD.Notes;
                            foreach (var item in ObjOCRD.LstCRD1)
                            {
                                oBusinessParteners.Addresses.AddressName = item.Address;
                                //oBusinessParteners.Addresses.Street = item.Street;
                                //oBusinessParteners.Addresses.Block = item.Block;
                                //oBusinessParteners.Addresses.City = item.City;
                                //oBusinessParteners.Addresses.ZipCode = item.ZipCode;
                                //oBusinessParteners.Addresses.County = item.County;
                                //oBusinessParteners.Addresses.State = item.State;
                                //oBusinessParteners.Addresses.Country = item.Country.ToUpper();
                                //oBusinessParteners.Addresses.StreetNo = item.StreetNo;
                                //oBusinessParteners.Addresses.BuildingFloorRoom = item.BuildingFloorRoom;
                                oBusinessParteners.Addresses.AddressType = item.Tipo == "F" ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;

                                if (item.Tipo != "F")
                                    oBusinessParteners.Addresses.TaxCode = "IGV";

                                oBusinessParteners.Addresses.Add();
                            }
                            ObjResult.Result = oBusinessParteners.Add();
                            if (ObjResult.Result != 0)
                            {
                                ObjResult.DocEntry = "";
                                ObjResult.Error = oCompany.GetLastErrorDescription();
                            }
                        }
                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjOCRD), 1, exx.Message.ToString());
                        ObjResult.DocEntry = "";
                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }
            return LstResult;

        }

        string FP_Generar_CardCode(string CardType, string U_GS_EsExt, string U_LGS_BPTD, string LicTradNum, int Grupo)
        {
            string objResult = "";
            try
            {
                SAPbobsCOM.Recordset Reco = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                SAPbobsCOM.Command MySP = Reco.Command;
                MySP.Name = "SBO_SP_GS_SN_GenerarCódigoSN";
                MySP.Parameters.Item(1).Value = CardType;
                MySP.Parameters.Item(2).Value = U_GS_EsExt;
                MySP.Parameters.Item(3).Value = U_LGS_BPTD;
                MySP.Parameters.Item(4).Value = LicTradNum;
                MySP.Parameters.Item(5).Value = Grupo;

                MySP.Execute();
                Reco.MoveFirst();
                for (int k = 0; k == (Reco.RecordCount - 1); k++)
                {
                    objResult = Reco.Fields.Item(0).Value.ToString();
                    Reco.MoveNext();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objResult;
        }

        #endregion

        #region Metodos del objeto OPOR

        string getItemCode(string ItemName, string Compania)
        {
            string ItemCode = "";
            try
            {
                if (ItemName.Trim() == "") throw new Exception("ItemName vacio");
                IBOITM OITMB = null;
                try
                {
                    OITMB = new BOITM(ModVariables.getCompanyConnection(Compania));
                    ItemCode = OITMB.FP_get_ItemCode(ItemName);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    OITMB = null;
                }

                //string Query = string.Format("select ItemCode from OITM where ItemName='{0}' and ItmsGrpCod='105'", ItemName.Trim());

                //SAPbobsCOM.Recordset Reco = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                //Reco.DoQuery(Query);
                //Reco.MoveFirst();
                //int sd = 0;
                //for (int k = 0; k == (Reco.RecordCount - 1); k++)
                //{
                //    sd += 1;
                //    ItemCode = Reco.Fields.Item(0).Value;
                //    Reco.MoveNext();
                //}
                //if (sd == 0)
                //    throw new Exception("No ingreso la consulta");
            }
            catch (Exception ex)
            {
                ItemCode = "";
                throw ex;
            }
            return ItemCode;
        }
        string SOPOR.lasgetItemCode(string ItemCode, string Compania)
        {
            return this.getItemCode(ItemCode, Compania);
        }

        List<CError> SOPOR.FP_Orden_Compra_OPOR(List<OPRQ> LstOPRQ, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (LstOPRQ == null) throw new Exception("Parametro LstOPRQ null");
                if (!this.Validar(Key))
                {
                    LstResult.Add(new CError(-1, "Llave Incorrecta", "", "", ""));
                    return LstResult;
                }
                if (LstOPRQ.Count == 0)
                {
                    LstResult.Add(new CError(-1, "No hay Registros en el parametro LstOPRQ", "", "", ""));
                    return LstResult;
                }
                RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;

                foreach (OPRQ ObjOPRQ in LstOPRQ)
                {
                    try
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjOPRQ));

                        ObjResult = new CError(ObjOPRQ.JournalMemo, ObjOPRQ.CodigoExterno);

                        if (ObjOPRQ == null)
                        {
                            ObjResult.Result = 10;
                            ObjResult.Error = "Objeto OPRQ vacio";
                        }
                        else
                        {
                            SAPbobsCOM.Documents oOPOR = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                            oOPOR.DocObjectCode = (SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);
                            oOPOR.Confirmed = BoYesNoEnum.tYES;
                            oOPOR.DocDate = Convert.ToDateTime(ObjOPRQ.DocDate, new CultureInfo("ru-RU"));
                            oOPOR.DocDueDate = Convert.ToDateTime(ObjOPRQ.DocDueDate, new CultureInfo("ru-RU"));
                            oOPOR.TaxDate = Convert.ToDateTime(ObjOPRQ.TaxDate, new CultureInfo("ru-RU"));
                            oOPOR.RequriedDate = Convert.ToDateTime(ObjOPRQ.RequriedDate, new CultureInfo("ru-RU"));
                            oOPOR.Comments = ObjOPRQ.Comments;
                            oOPOR.SalesPersonCode = ObjOPRQ.SlpCode;
                            oOPOR.CardCode = ObjOPRQ.CardCode;
                            oOPOR.NumAtCard = ObjOPRQ.NumAtCard;
                            if (ObjOPRQ.LstCampoUsuario.Count > 0)
                            {
                                foreach (var item in ObjOPRQ.LstCampoUsuario)
                                {
                                    oOPOR.UserFields.Fields.Item(item.Nombre).Value = CampoUsuario.getValueOnType(item);
                                }
                            }
                            foreach (var item in ObjOPRQ.LstPRQ1)
                            {
                                item.ItemCode = this.getItemCode(item.ItemName, Compania);
                                if (string.IsNullOrEmpty(item.ItemCode))
                                    throw new Exception("No se encontro el codigo del articulo: " + item.ItemName + " /Error-siglo");
                                ObjResult.Error2 = item.ItemCode;
                                oOPOR.Lines.TaxCode = "IGV";//
                                oOPOR.Lines.ItemCode = item.ItemCode;
                                oOPOR.Lines.Quantity = item.Quantity;
                                oOPOR.Lines.Price = item.Price;

                                //oOPOR.Lines.AccountCode = item.AccountCode;
                                oOPOR.Lines.Add();
                            }
                            ObjResult.Result = oOPOR.Add();// oOPOR.Add();

                            if (ObjResult.Result != 0)
                            {
                                ObjResult.Error = oCompany.GetLastErrorDescription();
                            }
                            else
                            {
                                ObjResult.DocEntry = oCompany.GetNewObjectKey();
                            }
                        }
                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjOPRQ), 1, exx.Message.ToString());

                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }

            return LstResult;
        }

        #endregion

        #region Metodos del objeto OJDT
        List<CError> SOJDT.SP_INSERTAR_OJDT(List<OJDT> LstOJDT, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (!this.Validar(Key))
                {
                    ObjResult = new CError(-1, "Llave Incorrecta", "", "", "");
                }
                RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;
                foreach (OJDT objOJDT in LstOJDT)
                {
                    try
                    {

                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(objOJDT));
                        ObjResult = new CError();
                        SAPbobsCOM.JournalVouchers jv = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalVouchers);

                        jv.JournalEntries.ReferenceDate = Convert.ToDateTime(objOJDT.ReferenceDate, new CultureInfo("ru-RU"));
                        jv.JournalEntries.TaxDate = Convert.ToDateTime(objOJDT.TaxDate, new CultureInfo("ru-RU")); ;
                        jv.JournalEntries.DueDate = Convert.ToDateTime(objOJDT.DueDate, new CultureInfo("ru-RU")); ;
                        jv.JournalEntries.Memo = objOJDT.Memo;
                        jv.JournalEntries.Reference = objOJDT.Reference;
                        jv.JournalEntries.Reference2 = objOJDT.Reference2;
                        jv.JournalEntries.Reference3 = objOJDT.Reference3;
                        foreach (JDT1 objJDT1 in objOJDT.LstJDT1)
                        {
                            jv.JournalEntries.Lines.ShortName = objJDT1.ShortName;
                            jv.JournalEntries.Lines.AccountCode = objJDT1.AccountCode;
                            if (objJDT1.Credit > 0)
                                jv.JournalEntries.Lines.Credit = objJDT1.Credit;
                            else if (objJDT1.Debit > 0)
                                jv.JournalEntries.Lines.Debit = objJDT1.Debit;
                            jv.JournalEntries.Lines.Add();
                        }
                        ObjResult.Result = jv.Add();

                        if (ObjResult.Result != 0)
                        {
                            ObjResult.Error = oCompany.GetLastErrorDescription();
                        }
                        else
                        {
                            string code = "";
                            oCompany.GetNewObjectCode(out code);
                            ObjResult.DocEntry = code;
                        }

                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, "", 1, exx.Message.ToString());

                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }
            return LstResult;
        }
        #endregion

        #region Metodos del objeto Payment
        List<CError> SORDR.FP_Cobranzas_Masiva_ORDR(List<ORCT> LstORCT, string Compania, string Key)
        {
            List<CError> LstResult = new List<CError>();
            CError ObjResult = new CError();
            bool sdk = false;
            try
            {
                if (Compania == null) throw new Exception("Parametro Compania null");
                if (Key == null) throw new Exception("Parametro Key null");
                if (LstORCT == null) throw new Exception("Parametro LstORCT null");
                if (!this.Validar(Key))
                {
                    LstResult.Add(new CError(-1, "Llave Incorrecta", "", "", ""));
                    return LstResult;
                }
                if (LstORCT.Count == 0)
                {
                    LstResult.Add(new CError(-1, "No hay Registros en el parametro LstORCT", "", "", ""));
                    return LstResult;
                }
                RecycleAppPools();
                iniciarSAPCompany(Compania);
                sdk = true;

                IBOPOR bOPOR = new BOPOR(ModVariables.getCompanyConnection(Compania));
                foreach (ORCT ObjORCT in LstORCT)
                {
                    try
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORCT));
                        ObjResult = new CError(ObjORCT.JrnlMemo, ObjORCT.CodigoExterno);

                        if (ObjORCT == null)
                        {
                            ObjResult.Result = 10;
                            ObjResult.Error = "Objeto ObjORDR vacio";
                        }
                        else
                        {
                            ORDR p = bOPOR.FP_GET_OINV_TICKET(ObjORCT.JrnlMemo);
                            SAPbobsCOM.Documents oInvoice = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);
                            SAPbobsCOM.Payments oPaymentS = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                            if (p == null)
                                throw new Exception("No se encontro el documento");
                            oInvoice.GetByKey(int.Parse(p.DocEntry));
                            oPaymentS.CardCode = ObjORCT.CardCode.Trim() == "" ? oInvoice.CardCode : ObjORCT.CardCode.Trim();
                            oPaymentS.DocDate = oInvoice.DocDate;// ObjORCT.DocDate == null ? Convert.ToDateTime(oInvoice.DocDate, new CultureInfo("ru-RU")) : Convert.ToDateTime(ObjORCT.DocDate, new CultureInfo("ru-RU")) ;
                            oPaymentS.DocTypte = SAPbobsCOM.BoRcptTypes.rCustomer;
                            foreach (var item in ObjORCT.LstCampoUsuario)
                            {
                                oPaymentS.UserFields.Fields.Item(item.Nombre).Value = CampoUsuario.getValueOnType(item);
                            }
                            ObjResult.Result2 = -22;
                            //oPaymentS.UserFields.Fields.Item("U_LGS_TPCE").Value = "PRE";
                            //oPaymentS.UserFields.Fields.Item("U_LGS_MEDPAG").Value = "003";
                            //
                            oPaymentS.DocCurrency = "S/";//oInvoice.DocCurrency;// 
                            ObjResult.Result2 = -23;
                            oPaymentS.CashSum =  ObjORCT.CashSum;//oInvoice.DocTotal;//
                            ObjResult.Result2 = -24;
                            oPaymentS.CashAccount = ObjORCT.CashAccount;// "1011101";
                            ObjResult.Result2 = -25;
                            

                            oPaymentS.Invoices.DocEntry = int.Parse(p.DocEntry);
                            ObjResult.Result2 = -25;
                            oPaymentS.Invoices.SumApplied =  ObjORCT.CashSum; //oInvoice.DocTotal;//
                            ObjResult.Result2 = -26;
                            oPaymentS.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                            ObjResult.CodigoExterno = $"Pago:{ObjORCT.CashSum.ToString()} | Doc:{oInvoice.DocTotal.ToString()}";
                            
                            ObjResult.Result = oPaymentS.Add();
                            if (ObjResult.Result != 0)
                            {
                                ObjResult.Error = oCompany.GetLastErrorDescription();
                                throw new Exception(oCompany.GetLastErrorDescription());
                            }
                            else
                            {
                                ObjResult.DocEntry = oCompany.GetNewObjectKey();
                            }
                            #region Normal

                            ////
                            //// Datos de Pago en Dolares ( Tarjeta o Efectivo)
                            //SAPbobsCOM.Payments oPaymentD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oIncomingPayments);
                            //oPaymentD.CardCode = oInvoice.CardCode;
                            //oPaymentD.DocDate = Convert.ToDateTime(oInvoice.DocDate, new CultureInfo("ru-RU"));
                            //oPaymentD.DocTypte = SAPbobsCOM.BoRcptTypes.rCustomer;

                            //if (ObjORDR.TypeSell == ORDR.EnuTypeSell.Employees)
                            //{
                            //    oPaymentS.DocCurrency = "S/";
                            //    oPaymentS.CashSum = oInvoice.DocTotal;
                            //    oPaymentS.CashAccount = "1419101";
                            //}
                            //else
                            //{
                            //    if (ObjORDR.U_RDC_EfSo != 0)
                            //    {
                            //        oPaymentS.DocCurrency = "S/";
                            //        oPaymentS.CashSum = ObjORDR.U_RDC_EfSo;
                            //        if (ObjORDR.CodAlmacen == "1100") // AEROPUERTO LIMA - PRINCIPAL
                            //            oPaymentS.CashAccount = "1011101";
                            //        else if (ObjORDR.CodAlmacen == "1500") // CHOCOLAT - PRINCIPAL
                            //            oPaymentS.CashAccount = "1011105";
                            //    }

                            //    if (ObjORDR.U_RDC_TjSo != 0)
                            //    {
                            //        foreach (var itemTarS in ObjORDR.LstRCT3)
                            //        {
                            //            if (itemTarS.Amount != 0)
                            //            {
                            //                if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "AMEX")  // AEROPUERTO LIMA - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 1;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032101";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "AMEX")  // CHOCOLAT - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 2;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032102";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "DINNERS")  // AEROPUERTO LIMA - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 5;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032101";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "DINNERS")  // CHOCOLAT - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 6;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032102";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "MASTER")  // AEROPUERTO LIMA - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 9;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032101";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "MASTER")  // CHOCOLAT - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 10;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032102";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1100" && itemTarS.CreditCard == "VISA")  // AEROPUERTO LIMA - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 13;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032101";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1500" && itemTarS.CreditCard == "VISA")  // CHOCOLAT - PRINCIPAL
                            //                {
                            //                    oPaymentS.CreditCards.CreditCard = 14;
                            //                    oPaymentS.CreditCards.CreditAcct = "1032102";
                            //                }

                            //                oPaymentS.CreditCards.CardValidUntil = Convert.ToDateTime(itemTarS.CardValidUntil, new CultureInfo("ru-RU"));
                            //                oPaymentS.CreditCards.CreditCardNumber = itemTarS.CreditCardNumber;
                            //                if (itemTarS.VoucherNum != "")
                            //                    oPaymentS.CreditCards.VoucherNum = itemTarS.VoucherNum;
                            //                else
                            //                    oPaymentS.CreditCards.VoucherNum = string.Format("{0}-{1}", ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);
                            //                oPaymentS.CreditCards.CreditSum = itemTarS.Amount;
                            //                oPaymentS.CreditCards.Add();
                            //            }
                            //        }
                            //    }

                            //    if (ObjORDR.U_RDC_EfDo != 0)
                            //    {
                            //        oPaymentD.DocCurrency = "US$";
                            //        oPaymentD.CashSum = ObjORDR.U_RDC_EfDo;
                            //        if (ObjORDR.CodAlmacen == "1100")  // AEROPUERTO LIMA - PRINCIPAL
                            //            oPaymentD.CashAccount = "1011101";
                            //        else if (ObjORDR.CodAlmacen == "1500") // CHOCOLAT - PRINCIPAL
                            //            oPaymentD.CashAccount = "1011105";
                            //    }

                            //    if (ObjORDR.U_RDC_TjDo != 0)
                            //    {
                            //        foreach (var itemTarD in ObjORDR.LstRCT3)
                            //        {
                            //            if (itemTarD.AmountFC != 0)
                            //            {
                            //                if (ObjORDR.CodAlmacen == "1100" && itemTarD.CreditCard == "VISA")
                            //                {
                            //                    oPaymentD.CreditCards.CreditCard = 17;
                            //                    oPaymentD.CreditCards.CreditAcct = "1032101";
                            //                }
                            //                else if (ObjORDR.CodAlmacen == "1500" && itemTarD.CreditCard == "VISA")
                            //                {
                            //                    oPaymentD.CreditCards.CreditCard = 18;
                            //                    oPaymentD.CreditCards.CreditAcct = "1032202";
                            //                }

                            //                oPaymentD.CreditCards.CardValidUntil = Convert.ToDateTime(itemTarD.CardValidUntil, new CultureInfo("ru-RU"));
                            //                oPaymentD.CreditCards.CreditCardNumber = itemTarD.CreditCardNumber;
                            //                if (itemTarD.VoucherNum != "")
                            //                    oPaymentD.CreditCards.VoucherNum = itemTarD.VoucherNum;
                            //                else
                            //                    oPaymentD.CreditCards.VoucherNum = string.Format("{0}-{1}", ObjORDR.U_LGS_SDOC, ObjORDR.U_LGS_NUCE);
                            //                oPaymentD.CreditCards.CreditSum = itemTarD.AmountFC;
                            //                oPaymentD.CreditCards.Add();
                            //            }
                            //        }
                            //    }
                            //}
                            //int err = 0;
                            //if (ObjORDR.U_RDC_EfSo != 0 || ObjORDR.U_RDC_TjSo != 0)
                            //{
                            //    oPaymentS.Invoices.DocEntry = int.Parse(ObjORDR.DocEntry);
                            //    oPaymentS.Invoices.SumApplied = ObjORDR.U_RDC_EfSo + ObjORDR.U_RDC_TjSo;
                            //    oPaymentS.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                            //    err = oPaymentS.Add();
                            //}
                            //if (err != 0)
                            //    throw new Exception(oCompany.GetLastErrorDescription());

                            //if ((ObjORDR.U_RDC_EfDo != 0 || ObjORDR.U_RDC_TjDo != 0) && ObjORDR.TypeSell != ORDR.EnuTypeSell.Employees)
                            //{
                            //    oPaymentD.Invoices.DocEntry = int.Parse(ObjORDR.DocEntry);
                            //    oPaymentD.Invoices.AppliedFC = ObjORDR.U_RDC_EfDo + ObjORDR.U_RDC_TjDo;
                            //    oPaymentD.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                            //    err = oPaymentD.Add();
                            //}
                            //if (err != 0)
                            //    throw new Exception(oCompany.GetLastErrorDescription());

                            //if (ObjResult.Result != 0)
                            //{
                            //    ObjResult.Error = oCompany.GetLastErrorDescription();
                            //}
                            //else
                            //{
                            //    ObjResult.DocEntry = oCompany.GetNewObjectKey();
                            //}
                            #endregion 
                        }
                    }
                    catch (Exception exx)
                    {
                        MODULOS.ErrorLog.RegistrarLog(System.Reflection.MethodBase.GetCurrentMethod().Name, Compania, Key, JsonConvert.SerializeObject(ObjORCT), 1, exx.Message.ToString());

                        ObjResult.Error = exx.ToString();
                        ObjResult.Result = -1;
                    }
                    LstResult.Add(ObjResult);
                }
            }
            catch (Exception ex)
            {
                LstResult.Add(new CError(-1, ex.Message.ToString(), "", "", ""));
            }
            finally
            {
                if (sdk == true) FinalizarSAPCómpany();
            }

            return LstResult;
        }
        #endregion
    }
}
