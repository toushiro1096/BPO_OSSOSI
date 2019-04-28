using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SSC.Prueba
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        #region Variables

        private static string key = "eDTL1q27/E8X3rLElDFhAsZoYaDXS8M/ox96p6n4L/Q3t19IFtV55NV2pw2Gq11u",
            company = "TEST_PE_SBO_OSSOSI_SAC";

        #endregion

        #region Metodos del objeto Auxliares

        private void MostrarResultado(List<ServiceRDCIC.CError> LstCERROR)
        {
            if (LstCERROR.Count > 0)
            {
                foreach (var item in LstCERROR)
                {
                    Response.Write(string.Format("Result:  {0}<br>", item.Result));
                    Response.Write(string.Format("Error:  {0}<br>", item.Error));
                    Response.Write(string.Format("DocEntry:  {0}<br>", item.DocEntry));
                    Response.Write(string.Format("JrnlMemo:  {0}<br>", item.JrnlMemo));
                    Response.Write(string.Format("CodigoExterno:  {0}<br>", item.CodigoExterno));
                    Response.Write(string.Format("Result2:  {0}<br>", item.Result2));
                    Response.Write(string.Format("Error2:  {0}<br>", item.Error2));
                    Response.Write(string.Format("DocEntry2:  {0}<br>", item.DocEntry2));
                    Response.Write(string.Format("JrnlMemo2:  {0}<br>", item.JrnlMemo2));
                    Response.Write(string.Format("CodigoExterno2:  {0}<br>", item.CodigoExterno2));
                    Response.Write("=======================<br>");

                }
            }
            else
                Response.Write("Sin Resultados <br>");
        }

        private void LimpiarMemory()
        {
            ServiceRDCIC.IconfigClient ObjConfig = new ServiceRDCIC.IconfigClient();
            ObjConfig.RefreshMemory();
        }

        private void obtenerxml(dynamic obj)
        {
            try
            {
                List<ServiceRDCIC.OCRD> result = new List<ServiceRDCIC.OCRD>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<ServiceRDCIC.OCRD>));
                using (FileStream fileStream = new FileStream(@"E:\DESARROLLO\SolServiceSAPCacao\SSC.Prueba\XMLFile1.xml", FileMode.Open))
                {
                    result = (List<ServiceRDCIC.OCRD>)serializer.Deserialize(fileStream);
                }
                MostrarResultado(new ServiceRDCIC.SOCRDClient().SP_INSERTAR_OCRD(result.ToArray(), company, key).ToList());
                //MostrarResultado(new ServiceRDCIC.SORDRClient().FP_Solicitud_Venta_Masiva_ORDR(result.ToArray(), company, key).ToList());
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.Message.ToString()));
            }
        }

        #endregion

        #region Insert ORDR

        private void AnularTicket() //FP_Solicitud_CancelarDocumentos_ORDR
        {
            try
            {
                //*******************************************************************************
                //ANULAR VENTA-TICKET---------------------------------- INGRESADO
                List<ServiceRDCIC.ORDR> LstORDR = new List<ServiceRDCIC.ORDR>();
                LstORDR.Add(new ServiceRDCIC.ORDR()
                {
                    TipoDoc = "12",
                    SerieDoc = "1500",
                    NumDoc = "999878",
                    DocDate = "19/09/2016 17:09:51"
                });
                MostrarResultado(new ServiceRDCIC.SORDRClient().FP_Solicitud_CancelarDocumentos_ORDR(LstORDR.ToArray(), company, key).ToList());

                //*******************************************************************************
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//INGRESADO (ORDR)

        private void IngresoVentaMasivo() //FP_Solicitud_Venta_Masiva_ORDR
        {
            try
            {
                //*******************************************************************************
                //// INGRESO DE VENTA MASIVO  ---------------------------------- VALIDADO CON SAP

                List<ServiceRDCIC.ORDR> LstORDR = new List<ServiceRDCIC.ORDR>();
                List<ServiceRDCIC.RCT3> LstRCT3 = new List<ServiceRDCIC.RCT3>();
                List<ServiceRDCIC.RDR1> LstRDR1 = new List<ServiceRDCIC.RDR1>();
                List<ServiceRDCIC.CampoUsuario> LstCU = new List<ServiceRDCIC.CampoUsuario>();

                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_GS_Turno", Valor = "2654" });//Estatico
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_GS_EsExt", Valor = "Y" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_GS_NAC", Valor = "Mexico" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_TDOC", Valor = "12" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_SDOC", Valor = "1500" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_CDOC", Valor = "00020488" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EfSo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EfDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EnSo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EnDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_VuSo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_VuDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_RDC_MonV", Valor = "S/" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_TjSo", Valor = "38" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_TjDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_TOPE", Valor = "01" });//Estatico
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_TIPO", Valor = "VEN" });

                LstRCT3.Add(new ServiceRDCIC.RCT3()
                {
                    CreditCardNumber = "9999",
                    CardValidUntil = "01/10/2017 03:10:20",
                    VoucherNum = "",
                    CreditCard = "VISA",
                    Amount = 38,
                    AmountFC = 0
                });
                LstRDR1.Add(new ServiceRDCIC.RDR1()
                {
                    ItemCode = "102962",
                    ItemDescription = "RDC LATA 'HOT CHOCOLAT' CACACO EN POLVO 300G X09",
                    Price = 32.2034,
                    Quantity = 1,
                    DiscountPercent = 0,
                    WarehouseCode = "1500"
                });
                LstORDR.Add(new ServiceRDCIC.ORDR()
                {
                    CardCode = "CE15000000000",
                    CardName = "CLIENTE GENERICO CHOCOLAT",
                    DocDate = "01/10/2016 05:10:28",
                    DocDueDate = "01/10/2016 05:10:28",
                    TaxDate = "01/10/2016 05:10:28",
                    SalesPersonCode = 50,
                    DocTotal = 38,
                    Comments = "Prueba Desarrollo",
                    JrnlMemo = "12-1500-00020488",
                    CodAlmacen = "1500",
                    LstPagoTarjeta = LstRCT3.ToArray(),
                    LstDetalle = LstRDR1.ToArray(),
                    LstCampoUsuario = LstCU.ToArray()
                });
                //---------------------------------------------------
                LstRCT3.Clear();
                LstRDR1.Clear();
                LstCU.Clear();
                //---------------------------------------------------
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_TDOC", Valor = "12" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_SDOC", Valor = "1500" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_CDOC", Valor = "00020489" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EfSo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EfDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EnSo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_EnDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_VuSo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_VuDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_RDC_MonV", Valor = "S/" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_TjSo", Valor = "64" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.numerico, Nombre = "U_RDC_TjDo", Valor = "0" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_GS_EsExt", Valor = "Y" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_GS_NAC", Valor = "Nicaragua" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_TIPO", Valor = "VEN" });
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_GS_Turno", Valor = "2654" });//Estatico
                LstCU.Add(new ServiceRDCIC.CampoUsuario() { tipo = ServiceRDCIC.CampoUsuario.m_tipos.alfanumerico, Nombre = "U_LGS_TOPE", Valor = "01" });//Estatico
                LstRCT3.Add(new ServiceRDCIC.RCT3()
                {
                    CreditCardNumber = "9999",
                    CardValidUntil = "01/10/2017 03:10:20",
                    VoucherNum = "",
                    CreditCard = "MASTER",
                    Amount = 64,
                    AmountFC = 0
                });
                LstRDR1.Add(new ServiceRDCIC.RDR1()
                {
                    ItemCode = "102264",
                    ItemDescription = "RDC BARRA DARK CHOC 62% 100G X12 X8",
                    Price = 27.1186,
                    Quantity = 1,
                    DiscountPercent = 0,
                    WarehouseCode = "1500"
                });
                LstRDR1.Add(new ServiceRDCIC.RDR1()
                {
                    ItemCode = "102268",
                    ItemDescription = "RDC BARRA MILK CHOC 35% Y AGUAYMANTO 100G X12 X8",
                    Price = 27.1186,
                    Quantity = 1,
                    DiscountPercent = 0,
                    WarehouseCode = "1500"
                });
                LstORDR.Add(new ServiceRDCIC.ORDR()
                {
                    CardCode = "CE15000000000",
                    CardName = "CLIENTE GENERICO CHOCOLAT",
                    DocDate = "01/10/2016 03:10:20",
                    DocDueDate = "01/10/2016 03:10:20",
                    TaxDate = "01/10/2016 03:10:20",
                    SalesPersonCode = 50,
                    DocTotal = 64,
                    Comments = "Prueba Desarrollo",
                    JrnlMemo = "12-1500-00020489",
                    CodAlmacen = "1500",
                    TipoMovimiento = ServiceRDCIC.ORDR.TIPOMOV.VEN,
                    LstPagoTarjeta = LstRCT3.ToArray(),
                    LstDetalle = LstRDR1.ToArray(),
                    LstCampoUsuario = LstCU.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SORDRClient().FP_Solicitud_Venta_Masiva_ORDR(LstORDR.ToArray(), company, key).ToList());

                //*******************************************************************************
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//VALIDADO CON SAP (ORDR)

        private void NoteCreditPurchase() //FP_Solicitud_NotaCredito_Compra
        {
            try
            {
                List<ServiceRDCIC.ORPC> LstORPC = new List<ServiceRDCIC.ORPC>();
                List<ServiceRDCIC.RPC1> LstRPC1 = new List<ServiceRDCIC.RPC1>();
                LstRPC1.Add(new ServiceRDCIC.RPC1()
                {
                    ItemCode = "",
                    ItemName = "ALQUILER ADICIONAL SUPERAR VENTAS MARZO 2016 ( 01-07-2016 AL 31-07-2016 ) Porcentaje de ventas",
                    LineNum = 0,
                    Quantity = 0,
                    WhsCode = "1500",
                    DiscountPercent = 0
                });
                LstORPC.Add(new ServiceRDCIC.ORPC()
                {
                    CardCode = "PL20501577252",
                    CardName = "LIMA AIRPORT PARTNERS S.R.L.",
                    DocDate = "14/12/2016 15:11:29",
                    TaxDate = "14/12/2016 15:11:29",
                    DocDueDate = "14/12/2016 15:11:29",
                    SlpCode = 1,
                    Series = ServiceRDCIC.ORPC.ISeries.S1100,
                    DocCurrency = "S/",
                    Comments = "COMENTARIO DE PRUEBA 2",
                    JournalMemo = "iNGRESO DE PRUEBA 2 ",
                    DocType = "S",
                    TipoDoc = "07",
                    SerieDoc = "F002",
                    NumDoc = "20889",
                    DocEntry = 1325,
                    ListaDetalle = LstRPC1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SORDRClient().FP_Solicitud_NotaCredito_Compra(LstORPC.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        private void NoteCreditSell() //FP_Solicitud_NotaCredito_Venta
        {
            try
            {
                List<ServiceRDCIC.ORPC> LstORPC = new List<ServiceRDCIC.ORPC>();
                List<ServiceRDCIC.RPC1> LstRPC1 = new List<ServiceRDCIC.RPC1>();
                LstRPC1.Add(new ServiceRDCIC.RPC1()
                {
                    LineNum = 0,
                    ItemCode = "108407",
                    ItemName = "RDC LATA 'ORITOS' TROZOS DE BANANA RECUBIERTA DARK CHOC 62%, 100G X12",
                    WhsCode = "1500",
                    Quantity = 2
                });
                LstRPC1.Add(new ServiceRDCIC.RPC1()
                {
                    LineNum = 1,
                    ItemCode = "108407",
                    ItemName = "RDC LATA 'ORITOS' TROZOS DE BANANA RECUBIERTA DARK CHOC 62%, 100G X12",
                    WhsCode = "1500",
                    Quantity = 1
                });
                LstORPC.Add(new ServiceRDCIC.ORPC()
                {
                    CardCode = "CE15000000000",
                    CardName = "CLIENTE GENERICO CHOCOLAT",
                    DocDate = "14/12/2016 15:11:29",
                    TaxDate = "14/12/2016 15:11:29",
                    DocDueDate = "14/12/2016 15:11:29",
                    SlpCode = 1,
                    Series = ServiceRDCIC.ORPC.ISeries.S1500,
                    DocCurrency = "S/",
                    TipoDoc = "07",
                    SerieDoc = "F002",
                    NumDoc = "12999",
                    Comments = "COMENTARIO DE PRUEBA 2",
                    JournalMemo = "iNGRESO DE PRUEBA 2 ",
                    DocType = "I",
                    DocEntry = 1330,
                    TipoDocOrigen = "12",
                    SerieDocOrigen = "1500",
                    NumDocOrigen = "00006998",
                    ListaDetalle = LstRPC1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SORDRClient().FP_Solicitud_NotaCredito_Venta(LstORPC.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        #endregion

        #region Insert OPOR

        private void SolicitudOPOR() //FP_Solicitud_OPOR
        {
            try
            {
                //*******************************************************************************
                //SOLICITUD OPOR  ---------------------------------- INGRESADO
                List<ServiceRDCIC.OPRQ> LstOPRQ = new List<ServiceRDCIC.OPRQ>();
                List<ServiceRDCIC.PRQ1> LstPRQ1 = new List<ServiceRDCIC.PRQ1>();
                const string fecha = "25/04/2017 12: 04 : 35"; //Variable de apoyo
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100273",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100511",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstOPRQ.Add(new ServiceRDCIC.OPRQ()
                {
                    DocDate = fecha,
                    DocDueDate = fecha,
                    TaxDate = fecha,
                    RequriedDate = fecha,
                    JournalMemo = "1500@80FE2536-DC25-4E93-A5E2-48FB10C4D398",
                    Comments = "PRUEBA",
                    Series = ServiceRDCIC.OPRQ.ISeries.S1100,
                    SlpCode = 0,
                    LstPRQ1 = LstPRQ1.ToArray()
                });
                // Segundo Item
                LstPRQ1.Clear();
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100273",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100511",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstOPRQ.Add(new ServiceRDCIC.OPRQ()
                {
                    DocDate = fecha,
                    DocDueDate = fecha,
                    TaxDate = fecha,
                    RequriedDate = fecha,
                    JournalMemo = "1500@2E3240B9-69FF-4E6A-9560-3E9316490103",
                    Comments = "PRUEBA",
                    Series = ServiceRDCIC.OPRQ.ISeries.S1100,
                    SlpCode = 0,
                    LstPRQ1 = LstPRQ1.ToArray()
                });

                // Tercer Item
                LstPRQ1.Clear();
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100273",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100511",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstOPRQ.Add(new ServiceRDCIC.OPRQ()
                {
                    DocDate = fecha,
                    DocDueDate = fecha,
                    TaxDate = fecha,
                    RequriedDate = fecha,
                    JournalMemo = "1500@1BC55433-1240-46FF-BC2D-4AF1D4F6F6BE",
                    Comments = "PRUEBA",
                    Series = ServiceRDCIC.OPRQ.ISeries.S1100,
                    SlpCode = 0,
                    LstPRQ1 = LstPRQ1.ToArray()
                });

                // Cuarto Item
                LstPRQ1.Clear();
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100273",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "100511",
                    Quantity = 1,
                    Price = 0,
                    LineNum = 0,
                    RequiredDate = fecha,
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstOPRQ.Add(new ServiceRDCIC.OPRQ()
                {
                    DocDate = fecha,
                    DocDueDate = fecha,
                    TaxDate = fecha,
                    RequriedDate = fecha,
                    JournalMemo = "1500@2642E123-67C2-4568-9C69-5463DAED7AAA",
                    Comments = "PRUEBA",
                    Series = ServiceRDCIC.OPRQ.ISeries.S1100,
                    SlpCode = 0,
                    LstPRQ1 = LstPRQ1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SOPORClient().FP_Solicitud_OPOR(LstOPRQ.ToArray(), company, key).ToList());

                //*******************************************************************************
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//INGRESADO (OPRQ)

        private void OrdenCompra() //FP_Orden_Compra_OPOR
        {
            try
            {
                //*******************************************************************************
                //SOLICITUD OPOR  ---------------------------------- INGRESADO
                List<ServiceRDCIC.OPRQ> LstOPRQ = new List<ServiceRDCIC.OPRQ>();
                List<ServiceRDCIC.PRQ1> LstPRQ1 = new List<ServiceRDCIC.PRQ1>();
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "102015",
                    Quantity = 10,
                    RequiredDate = "04/10/2016 20:11:29",
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "102041",
                    Quantity = 10,
                    RequiredDate = "04/10/2016 20:11:29",
                    WarehouseCode = "1100",
                    CostingCode = "1100"
                });
                LstOPRQ.Add(new ServiceRDCIC.OPRQ()
                {
                    DocDate = "04/10/2016 20:11:29",
                    DocDueDate = "04/10/2016 20:11:29",
                    TaxDate = "04/10/2016 20:11:29",
                    RequriedDate = "04/10/2016 20:11:29",
                    JournalMemo = "Ingreso de Prueba",
                    Comments = "Ingreso de Prueba",
                    LstPRQ1 = LstPRQ1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SOPORClient().FP_Orden_Compra_OPOR(LstOPRQ.ToArray(), company, key).ToList());

                //*******************************************************************************
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        private void Pedido_Proovedores() //FP_Pedido_Proovedores
        {
            try
            {
                List<ServiceRDCIC.OPCH> LstOPCH = new List<ServiceRDCIC.OPCH>();
                LstOPCH.Add(new ServiceRDCIC.OPCH()
                {
                    DocDate = "04/11/2016 18:11:29",
                    DocDueDate = "04/11/2016 18:11:29",
                    TaxDate = "04/11/2016 18:11:29",
                    U_LGS_TDOC = "01",
                    U_LGS_SDOC = "999",
                    U_LGS_CDOC = "1234567",
                    U_LGS_TIPO = ServiceRDCIC.OPCH.TIPOMOV.COM,
                    Series = ServiceRDCIC.OPCH.ISeries.S1200,
                    DocEntry = "173"
                });
                MostrarResultado(new ServiceRDCIC.SOPORClient().FP_Pedido_Proovedores(LstOPCH.ToArray(), company, key).ToList());
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        private void FP_EntradaMercancia_To_Proovedores() //FP_EntradaMercancia_To_Proovedores
        {
            try
            {
                List<ServiceRDCIC.OPCH> LstOPCH = new List<ServiceRDCIC.OPCH>();
                List<ServiceRDCIC.PCH1> LstPCH1 = new List<ServiceRDCIC.PCH1>();
                LstPCH1.Add(new ServiceRDCIC.PCH1()
                {
                    DocEntry = 42,
                    CodigoExterno = "F0015"
                }); 
                LstPCH1.Add(new ServiceRDCIC.PCH1()
                {
                    DocEntry = 43,
                    CodigoExterno = "F0016"
                });
                LstOPCH.Add(new ServiceRDCIC.OPCH()
                {
                    DocDate = "05/11/2016 18:11:29",
                    DocDueDate = "05/11/2016 18:11:29",
                    TaxDate = "05/11/2016 18:11:29",
                    U_LGS_TDOC = "01",
                    U_LGS_SDOC = "999",
                    U_LGS_CDOC = "1234992",
                    U_LGS_TIPO = ServiceRDCIC.OPCH.TIPOMOV.COM,
                    Series = ServiceRDCIC.OPCH.ISeries.S1200,
                    SlpCode = 24,
                    DocCurrency = "US$",
                    LstPCH1 = LstPCH1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SOPORClient().FP_EntradaMercancia_To_Proovedores(LstOPCH.ToArray(), company, key).ToList());
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        private void FP_OrdenCompra_To_EntradaMercancia() //FP_OrdenCompra_To_EntradaMercancia
        {
            try
            {
                List<ServiceRDCIC.OPRQ> LstOPRQ = new List<ServiceRDCIC.OPRQ>();
                List<ServiceRDCIC.PRQ1> LstPRQ1 = new List<ServiceRDCIC.PRQ1>();
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "108616",
                    Quantity = 5,
                    LineNum = 7,
                    WarehouseCode = "1100"
                });
                LstPRQ1.Add(new ServiceRDCIC.PRQ1()
                {
                    ItemCode = "102776",
                    Quantity = 30,
                    LineNum = 57,
                    WarehouseCode = "1100"
                });
                LstOPRQ.Add(new ServiceRDCIC.OPRQ()
                {
                    DocDate = "04/11/2016 18:11:29",
                    DocDueDate = "04/11/2016 18:11:29",
                    TaxDate = "04/11/2016 18:11:29",
                    Series = ServiceRDCIC.OPRQ.ISeries.S1200,
                    Comments = "Comentario INgreso Prueba",
                    JournalMemo = "JrnlMemo prueba",
                    SlpCode = 24,
                    U_LGS_TDOC = "09",
                    U_LGS_SDOC = "999",
                    U_LGS_NUCE = "1234990",
                    CodigoExterno = "mfiaewu@sdamsodasnon",
                    DocEntry = 165, // Proviene de la lista
                    LstPRQ1 = LstPRQ1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SOPORClient().FP_OrdenCompra_To_EntradaMercancia(LstOPRQ.ToArray(), company, key).ToList());
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        #endregion

        #region Insert OWTR

        private void TransferenciaStock() //FP_Transferencia_Stock_OWTR
        {

            //******************************************************************************* 
            //// TransferenciaStock   ---------------------------------- INGRESADO
            try
            {
                List<ServiceRDCIC.OWTR> LstOWTR = new List<ServiceRDCIC.OWTR>();
                List<ServiceRDCIC.WTR1> LstWTR1 = new List<ServiceRDCIC.WTR1>();
                LstWTR1.Add(new ServiceRDCIC.WTR1()
                {
                    StyleName = "P104108",
                    Quantity = 4
                });
                LstOWTR.Add(new ServiceRDCIC.OWTR()
                {
                    DocDate = "04/11/2016 11:11:29",
                    TaxDate = "04/11/2016 11:11:29",
                    CodeAlmOrigen = "1100",
                    CodeAlmDestino = "1500",
                    Comments = "Prueba Traslados -",
                    memo = "Prueba Desarrollo",
                    Series = ServiceRDCIC.OWTR.ISeries.S1100,
                    ListaArticulos = LstWTR1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.OWTRClient().FP_Transferencia_Stock_OWTR(LstOWTR.ToArray(), company, key).ToList());
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
            //******************************************************************************* 

        }//INGRESADO (OWTR)

        #endregion

        #region Insert OPDN

        private void IngresoMercaderia() //FP_Solicitud_Recibo_OPDN
        {
            try
            {
                List<ServiceRDCIC.OPDN> LstOPDN = new List<ServiceRDCIC.OPDN>();
                List<ServiceRDCIC.PDN1> LstPDN1 = new List<ServiceRDCIC.PDN1>();
                LstPDN1.Add(new ServiceRDCIC.PDN1()
                {
                    ItemCode = "200001",
                    Quantity = 150,
                    Currency = "EUR",
                    Price = 17.0700,
                    DiscountPercent = 0,
                    WarehouseCode = "1500"
                });

                LstOPDN.Add(new ServiceRDCIC.OPDN()
                {
                    DocDate = "04/10/2016 15:11:29",
                    DocDueDate = "04/10/2016 15:11:29",
                    TaxDate = "04/10/2016 15:11:29",
                    CardCode = "PE00000000004",
                    CardName = "AMPLITUDE - TIN BOXES",
                    DocCurrency = "EUR",
                    Series = ServiceRDCIC.OPDN.ISeries.S1500,
                    Comments = "COMENTARIO DE PRUEBA 2",
                    JrnlMemo = "iNGRESO DE PRUEBA 2 ",
                    SlpCode = 1,
                    TipoMovimiento = ServiceRDCIC.OPDN.TIPOMOV.COM,
                    TipoDoc = "01",
                    SerieDoc = "1507",
                    NumDoc = "0000184",
                    ListaDetalle = LstPDN1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.OPDNClient().FP_Solicitud_Recibo_OPDN(LstOPDN.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//INGRESADO (OIGN)

        private void SalidaMercaderia() //FP_Solicitud_Salida_OPDN
        {
            try
            {
                List<ServiceRDCIC.OPDN> LstOPDN = new List<ServiceRDCIC.OPDN>();
                List<ServiceRDCIC.PDN1> LstPDN1 = new List<ServiceRDCIC.PDN1>();
                LstPDN1.Add(new ServiceRDCIC.PDN1()
                {
                    ItemCode = "200001",
                    Quantity = 150,
                    Currency = "EUR",
                    Price = 17.0700,
                    DiscountPercent = 0,
                    WarehouseCode = "1500"
                });

                LstOPDN.Add(new ServiceRDCIC.OPDN()
                {
                    DocDate = "04/10/2016 15:11:29",
                    DocDueDate = "04/10/2016 15:11:29",
                    TaxDate = "04/10/2016 15:11:29",
                    CardCode = "PE00000000004",
                    CardName = "AMPLITUDE - TIN BOXES",
                    DocCurrency = "EUR",
                    Series = ServiceRDCIC.OPDN.ISeries.S1500,
                    Comments = "COMENTARIO DE PRUEBA 2",
                    JrnlMemo = "iNGRESO DE PRUEBA 2 ",
                    SlpCode = 1,
                    TipoMovimiento = ServiceRDCIC.OPDN.TIPOMOV.DEG,
                    TipoDoc = "01",
                    SerieDoc = "1507",
                    NumDoc = "0000198",
                    ListaDetalle = LstPDN1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.OPDNClient().FP_Solicitud_Salida_OPDN(LstOPDN.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//INGRESADO (OIGE)

        private void AjusteIngresoInventario() //FP_Solicitud_Ajuste_Ingreso_OPDN
        {
            try
            {
                List<ServiceRDCIC.OPDN> LstOPDN = new List<ServiceRDCIC.OPDN>();
                List<ServiceRDCIC.PDN1> LstPDN1 = new List<ServiceRDCIC.PDN1>();
                LstPDN1.Add(new ServiceRDCIC.PDN1()
                {
                    ItemCode = "200001",
                    Quantity = 150,
                    Currency = "EUR",
                    Price = 17.0700,
                    DiscountPercent = 0,
                    WarehouseCode = "1200"
                });
                LstOPDN.Add(new ServiceRDCIC.OPDN()
                {
                    DocDate = "04/10/2016 15:11:29",
                    DocDueDate = "04/10/2016 15:11:29",
                    TaxDate = "04/10/2016 15:11:29",
                    Series = ServiceRDCIC.OPDN.ISeries.S1500,
                    Comments = "COMENTARIO DE PRUEBA 2",
                    JrnlMemo = "iNGRESO DE PRUEBA 2 ",
                    ListaDetalle = LstPDN1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.OPDNClient().FP_Solicitud_Ajuste_Ingreso_OPDN(LstOPDN.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        private void AjusteSalidaInventario() //FP_Solicitud_Ajuste_Salida_OPDN
        {
            try
            {
                List<ServiceRDCIC.OPDN> LstOPDN = new List<ServiceRDCIC.OPDN>();
                List<ServiceRDCIC.PDN1> LstPDN1 = new List<ServiceRDCIC.PDN1>();
                LstPDN1.Add(new ServiceRDCIC.PDN1()
                {
                    ItemCode = "200001",
                    Quantity = 150,
                    Currency = "EUR",
                    Price = 17.0700,
                    DiscountPercent = 0,
                    WarehouseCode = "1200"
                });
                LstOPDN.Add(new ServiceRDCIC.OPDN()
                {
                    DocDate = "04/10/2016 15:11:29",
                    DocDueDate = "04/10/2016 15:11:29",
                    TaxDate = "04/10/2016 15:11:29",
                    Series = ServiceRDCIC.OPDN.ISeries.S1500,
                    Comments = "COMENTARIO DE PRUEBA 2",
                    JrnlMemo = "iNGRESO DE PRUEBA 2 ",
                    ListaDetalle = LstPDN1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.OPDNClient().FP_Solicitud_Ajuste_Salida_OPDN(LstOPDN.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        private void InsertarTransformacion() //FP_Solicitud_Transformacion_Ingreso
        {
            try
            {
                List<ServiceRDCIC.OWOR> LstOWOR = new List<ServiceRDCIC.OWOR>();
                List<ServiceRDCIC.WOR1> LstWOR1 = new List<ServiceRDCIC.WOR1>();
                LstWOR1.Add(new ServiceRDCIC.WOR1()
                {
                    ItemCode = "P104121",
                    Quantity = 1,
                    WarehouseCode = "1500"
                });
                LstOWOR.Add(new ServiceRDCIC.OWOR()
                {
                    PostingDate = "28/11/2016 21:10:27",
                    DueDate = "28/11/2016 21:10:27",
                    DocDate = "28/11/2016 21:10:27",
                    DocDueDate = "28/11/2016 21:10:27",
                    CardCode = "",
                    Series2 = ServiceRDCIC.OWOR.ISeries2.S1500,
                    ItemNo = "104121",
                    PlannedQty = 152,
                    Price = 1.2947,
                    WarehouseCode = "1500",
                    JrnlMemo = "Recibo de producción",
                    Comments = "coment prueba",
                    Series = ServiceRDCIC.OWOR.ISeries.S1500,
                    ListWOR1 = LstWOR1.ToArray()
                });
                // CERROR.DocEntry = DocEntryOrFa
                // CERROR.DocEntry2 = DocEntryRePr

                MostrarResultado(new ServiceRDCIC.OPDNClient().FP_Solicitud_Transformacion_Ingreso(LstOWOR.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }

        #endregion

        #region Insert ORPD

        private void FP_Devolucion_Recibo_ORPD() //FP_Devolucion_Recibo_ORPD
        {/*
            try
            {
                List<ServiceRDCIC.ORPD> LstORPD = new List<ServiceRDCIC.ORPD>();
                List<ServiceRDCIC.PDN1> LstPDN1 = new List<ServiceRDCIC.PDN1>();
                LstPDN1.Add(new ServiceRDCIC.PDN1()
                {
                    ItemCode = "200001",
                    Quantity = 10,
                    WarehouseCode = "1200"
                });

                LstORPD.Add(new ServiceRDCIC.ORPD()
                {
                    DocDate = "04/10/2016 15:11:29",
                    DocDueDate = "04/10/2016 15:11:29",
                    CardCode = "PE00000000004",
                    CardName = "AMPLITUDE - TIN BOXES",
                    Comments = "COMENTARIO DE PRUEBA 2",
                    Jrnlmemo = "iNGRESO DE PRUEBA 2 ",
                    SalesPersonCode = 1,
                    NumAtCard = "12-B154-0009999",
                    ListaDetalle = LstPDN1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.OPDNClient().FP_Solicitud_Recibo_OPDN(LstOPDN.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }*/
        }

        #endregion

        #region Insert OCRD

        private void InsertarCliente() //SP_INSERTAR_OCRD
        {
            try
            {
                //*******************************************************************************
                //INSERTAR CLIENTE  ---------------------------------- VALIDADO CON SAP
                List<ServiceRDCIC.OCRD> LstOCRD = new List<ServiceRDCIC.OCRD>();
                List<ServiceRDCIC.CRD1> LstCRD1 = new List<ServiceRDCIC.CRD1>();
                LstCRD1.Add(new ServiceRDCIC.CRD1()
                {
                    Address = "DIRECCION FISCAL",
                    Street = "",
                    Block = "",
                    City = "",
                    ZipCode = "",
                    County = "",
                    State = "",
                    Country = "",
                    StreetNo = "",
                    BuildingFloorRoom = "",
                    Tipo = "F"
                });
                LstCRD1.Add(new ServiceRDCIC.CRD1()
                {
                    Address = "DIRECCION ENVIO 01",
                    Street = "",
                    Block = "",
                    City = "",
                    ZipCode = "",
                    County = "",
                    State = "",
                    Country = "",
                    StreetNo = "",
                    BuildingFloorRoom = "",
                    Tipo = "E"
                });
                LstOCRD.Add(new ServiceRDCIC.OCRD()
                {
                    LicTradNum = "71475735",
                    CardCode = "CL71475735000",
                    U_GS_EsExt = "Y",
                    CardName = "PATRICIA ",
                    Nombre1 = "PATRICIA",
                    Nombre2 = "",
                    ApellidoP = "JOTES",
                    ApellidoM = "",
                    City = "",
                    Country = "PE",
                    County = "",
                    E_Mail = "",
                    MailCity = "",
                    MailCountr = "",
                    MailCounty = "",
                    Notes = "",
                    Tipo = "C",
                    TipoPersona = "N",
                    TipDoc = 1,
                    LstCRD1 = LstCRD1.ToArray()
                });
                MostrarResultado(new ServiceRDCIC.SOCRDClient().SP_INSERTAR_OCRD(LstOCRD.ToArray(), company, key).ToList());

                //*******************************************************************************
            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//VALIDADO CON SAP (OCRD)

        #endregion

        #region Insert OSLP

        private void IngresoEmpleados() //SP_INSERTAR_OSLP
        {
            try
            {
                List<ServiceRDCIC.OSLP> LstOSLP = new List<ServiceRDCIC.OSLP>();
                LstOSLP.Add(new ServiceRDCIC.OSLP()
                {
                    SlpNAme = "Vendor 1500 migule prueba",
                    CodeAlm = "1500",
                    JrnlMemo = "mherenciaprueba",
                    CodigoExterno = "154451215",
                    LstCampoUsuario = new List<ServiceRDCIC.CampoUsuario>().ToArray()
                });
                MostrarResultado(
                    new ServiceRDCIC.SOSLPClient().SP_INSERTAR_OSLP(
                    LstOSLP.ToArray(), company, key).ToList());

            }
            catch (Exception ex)
            {
                Response.Write(string.Format("Error : {0}", ex.ToString()));
            }
        }//VALIDADO CON SAP (OSLP)

        #endregion

        #region Query OCRD

        private void FP_BUSCAR_OCRD()
        {
            try
            {
                ServiceSap.OCRD objOCRD = new ServiceSap.OCRDClient().FP_BUSCAR_OCRD(new ServiceSap.OCRD() { LicTradNum = "20341841357", Tipo = "C" }, company, key);
                Label1.Text = Label1.Text + "<br>=============================";
                Label1.Text = string.Format("{0}<br>CardCode: {1}", Label1.Text, objOCRD.CardCode);
                Label1.Text = string.Format("{0}<br>CardName: {1}", Label1.Text, objOCRD.CardName);
                Label1.Text = string.Format("{0}<br>LicTradNum: {1}", Label1.Text, objOCRD.LicTradNum);
            }
            catch (Exception ex)
            {
                Label1.Text = string.Format("{0}<br>Error: {1}", Label1.Text, ex.ToString());
            }
        }

        private void FP_Listar_OCRD()
        {
            try
            {
                List<ServiceSap.OCRD> LstOCRD = new ServiceSap.OCRDClient().FP_Listar_OCRD(new ServiceSap.OCRD() { Tipo = "P" }, company, key).ToList();
                int i = 0;
                foreach (var item in LstOCRD)
                {
                    Label1.Text = Label1.Text + "<br>=============================";
                    Label1.Text = string.Format("{0}<br>Nro Item: {1}", Label1.Text, i);
                    Label1.Text = string.Format("{0}<br>CardCode: {1}", Label1.Text, item.CardCode);
                    Label1.Text = string.Format("{0}<br>CardName: {1}", Label1.Text, item.CardName);
                    Label1.Text = string.Format("{0}<br>LicTradNum: {1}", Label1.Text, item.LicTradNum);
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = string.Format("{0}<br>Error: {1}", Label1.Text, ex.ToString());
            }
        }

        #endregion




        #region Query OITM

        private void FP_LISTAR_OITM()
        {
            try
            {
                List<ServiceSap.OITM> LstOITM = new ServiceSap.OITMClient().FP_LISTAR_OITM("2016-04-27", "2016-04-27", company, key).ToList();
                int i = 0;
                foreach (var item in LstOITM)
                {
                    Label1.Text = Label1.Text + "<br>=============================";
                    Label1.Text = string.Format("{0}<br>Nro Item: {1}", Label1.Text, i);
                    Label1.Text = string.Format("{0}<br>ItemCode: {1}", Label1.Text, item.StyleName);
                    i += 1;

                }
            }
            catch (Exception ex)
            {
                Label1.Text = string.Format("{0}<br>Error: {1}", Label1.Text, ex.ToString());
            }
        }

        private void FP_LISTAR_OITM_TOT()
        {
            try
            {
                List<ServiceSap.OITM> LstOITM = new ServiceSap.OITMClient().FP_LISTAR_OITM_TOT(company, key).ToList();
                int i = 0;
                foreach (var item in LstOITM)
                {
                    Label1.Text = Label1.Text + "<br>=============================";
                    Label1.Text = string.Format("{0}<br>Nro Item: {1}", Label1.Text, i);
                    Label1.Text = string.Format("{0}<br>ItemCode: {1}", Label1.Text, item.StyleName);
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = string.Format("{0}<br>Error: {1}", Label1.Text, ex.ToString());
            }
        }

        #endregion




        #region Query ORTT

        private void FP_LISTAR_ORTT()
        {
            try
            {
                List<ServiceSap.ORTT> LstORTT = new ServiceSap.ORTTClient().FP_LISTAR_ORTT("2016-01-01", company, key).ToList();
                int i = 0;
                foreach (var item in LstORTT)
                {
                    Label1.Text = Label1.Text + "<br>=============================";
                    Label1.Text = string.Format("{0}<br>Nro Item: {1}", Label1.Text, i);
                    Label1.Text = string.Format("{0}<br>RateDate: {1}", Label1.Text, item.RateDate);
                    Label1.Text = string.Format("{0}<br>Currency: {1}", Label1.Text, item.Currency);
                    Label1.Text = string.Format("{0}<br>Rate: {1}", Label1.Text, item.Rate);
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                Label1.Text = string.Format("{0}<br>Error: {1}", Label1.Text, ex.ToString());
            }
        }

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            FP_BUSCAR_OCRD();
        }
    }
}