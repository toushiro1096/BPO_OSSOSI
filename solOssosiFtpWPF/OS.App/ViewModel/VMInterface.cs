using Microsoft.Win32;
using OS.App.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Linq;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace OS.App.ViewModel
{

    public static class ResourceSap
    {
        public const string Company = "PE_SBO_OSSOSI_SAC";// "PE_SBO_OSSOSI_SAC"; // PE_SBO_OSSOSI_PRUEBAS
        public const string Key = "eDTL1q27/E8X3rLElDFhAsZoYaDXS8M/ox96p6n4L/Q3t19IFtV55NV2pw2Gq11u";
    }

    public class beCprAgp
    {
        public string Ruc { get; set; }
        public string Supplier { get; set; }
        public string Location { get; set; }
        public string ReceiptName { get; set; }
        public string ReceiptDate { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceDate { get; set; }
        public string FamilyGroup { get; set; }
        public double NetVal { get; set; }
        public double GrossVal { get; set; }
    }

    public class VMInterface : NotifyPropertyChanged
    {
        #region Constantes
        const string ftpH = "ftp://200.14.248.247/"; //  "ftp://169.47.196.208/";
        const string ftpF1 = "recibidos";
        const string ftpF2 = "procesados";
        const bool flagDes = true;
        #endregion

        #region Variables
        List<string> LstDes = new List<string>();
        private string ftpU = string.Empty;
        private string ftpP = string.Empty;
        private string ftpD1 = string.Empty;
        private string ftpD2 = string.Empty;
        private bool isSiglo = false;
        private bool isLoading = false;
        private bool flagFtp = true;
        private Visibility visible;
        private int h = 0;
        private int cntRsltPrc;
        private int cntRsltPrcOk;
        string flagPrc = string.Empty;

        modFtpFileDet M = null;

        private readonly BackgroundWorker worker;
        private string modo = string.Empty;

        string pRemoteFile = string.Empty;
        string pLocalFile = string.Empty;
        string pLocalPath = string.Empty;
        int prog = 0;
        IEnumerable<modFtpFileDet> lstFile1 = null;
        ObservableCollection<ModFileProcess> lstFile2 = null;

        DataSet dsPrcChk = new DataSet();

        Command cmdValidar = null;
        Command cmdEnviar = null;
        Command cmdSeleccionar = null;
        Command cmdDescargar = null;
        Command cmdProcesar = null;

        #endregion

        #region VariablesSAP
        #region Ventas
        string pVtaNoCorr;              //  0
        string pVtaFecEmi;              //  1
        string pVtaFecVen;              //  2
        string pVtaTipo;                //  3
        string pVtaNoSer;               //  4
        string pVtaNoDoc;               //  5
        string pVtaCliTipDoc;           //  6
        string pVtaCliNoDoc;            //  7
        string pVtaCliRazSoc;           //  8
        string pVtaValFacExpo;          //  9
        string pVtaBaseImpoOpeGrav;     // 10
        string pVtaImpoOpeExo;          // 11
        string pVtaImpoOpeIna;          // 12
        string pVtaISC;                 // 13
        string pVtaIGV;                 // 14
        string pVtaOtrTrib;             // 15
        string pVtaProp;                // 16
        string pVtaImpTotal;            // 17
        string pVtaTipCbo;              // 18
        string pVtaRefFec;              // 19
        string pVtaRefTipo;             // 20
        string pVtaRefNoSer;            // 21
        string ptaRefNoDoc;             // 22
        private void cleanSAPVta()
        {
            pVtaNoCorr = string.Empty;
            pVtaFecEmi = string.Empty;
            pVtaFecVen = string.Empty;
            pVtaTipo = string.Empty;
            pVtaNoSer = string.Empty;
            pVtaNoDoc = string.Empty;
            pVtaCliTipDoc = string.Empty;
            pVtaCliNoDoc = string.Empty;
            pVtaCliRazSoc = string.Empty;
            pVtaValFacExpo = string.Empty;
            pVtaBaseImpoOpeGrav = string.Empty;
            pVtaImpoOpeExo = string.Empty;
            pVtaImpoOpeIna = string.Empty;
            pVtaISC = string.Empty;
            pVtaIGV = string.Empty;
            pVtaOtrTrib = string.Empty;
            pVtaProp = string.Empty;
            pVtaImpTotal = string.Empty;
            pVtaTipCbo = string.Empty;
            pVtaRefFec = string.Empty;
            pVtaRefTipo = string.Empty;
            pVtaRefNoSer = string.Empty;
            ptaRefNoDoc = string.Empty;
        }
        #endregion
        #region Compras
        string pCprRuc;                 //  0
        string pCprSupplier;            //  1 -> Cabecera
        string pCprArticle;             //  1 -> Detalle
        string pCprLocation;            //  2
        string pCprReceiptName;         //  3
        string pCprReceiptDate;         //  4
        string pCprInvoiceName;         //  5
        string pCprInvoiceDate;         //  6
        string pCprNetValue;            //  7
        string pCprGrossValue;          //  8
        string pCprState;               //  9
        string pCprMajorGroup;          // 10
        string pCprFamilyGroup;         // 11
        private void cleanSAPCpr()
        {
            pCprRuc = string.Empty;
            pCprSupplier = string.Empty;
            pCprArticle = string.Empty;
            pCprLocation = string.Empty;
            pCprReceiptName = string.Empty;
            pCprReceiptDate = string.Empty;
            pCprInvoiceName = string.Empty;
            pCprInvoiceDate = string.Empty;
            pCprNetValue = string.Empty;
            pCprGrossValue = string.Empty;
            pCprState = string.Empty;
            pCprMajorGroup = string.Empty;
            pCprFamilyGroup = string.Empty;
        }
        #endregion
        #endregion

        #region Comandos
        public ICommand CmdValidar => cmdValidar;
        public ICommand CmdEnviar => cmdEnviar;
        public ICommand CmdSeleccionar => cmdSeleccionar;
        public ICommand CmdDescargar => cmdDescargar;
        public ICommand CmdProcesar => cmdProcesar;
        #endregion

        #region Propiedades
        public int Prog
        {
            get { return prog; }
            set { SetProperty(() => prog = value, () => prog == value); }
        }
        public string PLocalFile
        {
            get { return pLocalFile; }
            set { SetProperty(() => pLocalFile = value, () => pLocalFile == value); }
        }
        public string PLocalPath
        {
            get { return pLocalPath; }
            set { SetProperty(() => pLocalPath = value, () => pLocalPath == value); }
        }
        public IEnumerable<modFtpFileDet> LstFile1
        {
            get { return lstFile1; }
            private set { SetProperty(ref lstFile1, value); }
        }
        public ObservableCollection<ModFileProcess> LstFile2
        {
            get { return lstFile2; }
            private set { SetProperty(ref lstFile2, value); }
        }
        public bool IsSiglo
        {
            get { return isSiglo; }
            set { SetProperty(() => isSiglo = value, () => isSiglo == value); }
        }
        public bool FlagFtp
        {
            get { return flagFtp; }
            set { SetProperty(() => flagFtp = value, () => flagFtp == value); }
        }
        public Visibility Visible
        {
            get { return visible; }
            set { SetProperty(() => visible = value, () => visible == value); }
        }
        public int H
        {
            get { return h; }
            set { SetProperty(() => h = value, () => h == value); }
        }
        #endregion

        #region Metodos

        #region Sistema
        public void Validar(object obj)
        {
            try
            {
                if (FlagFtp)
                {
                    // FTP
                    LstFile1 = directoryListDetailedFtp(ftpD1);
                    // if (IsSiglo) LstFile2 = directoryListDetailedFtp(ftpD2);
                }
                else
                {
                    // LOCAL
                    FolderBrowserDialog pFd = new FolderBrowserDialog();
                    DialogResult dr = pFd.ShowDialog();
                    PLocalPath = pFd.SelectedPath;

                    LstFile1 = directoryListDetailedLocal(PLocalPath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Enviar(object obj)
        {
            try
            {
                if (!worker.IsBusy) { worker.RunWorkerAsync(); modo = "E"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Validar(obj);
            }
        }
        public void Seleccionar(object obj)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog pFd = new Microsoft.Win32.OpenFileDialog();
                if (pFd.ShowDialog() == true) PLocalFile = pFd.FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Descargar(object obj)
        {
            try
            {
                FolderBrowserDialog pFd = new FolderBrowserDialog();
                DialogResult dr = pFd.ShowDialog();
                PLocalPath = pFd.SelectedPath;
                M = obj as modFtpFileDet;
                if (M != null) if (!worker.IsBusy) { worker.RunWorkerAsync(); modo = "D"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Validar(obj);
            }
        }
        public void Procesar(object obj)
        {
            try
            {
                if (!worker.IsBusy) { worker.RunWorkerAsync(); modo = FlagFtp ? "PF" : "PL"; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Validar(obj);
            }
        }
        #endregion

        #region SAP
        private bool SaveVta(ModFileProcess pModFP, DataTable pDt)
        {
            //return true;
            cntRsltPrc = 0;
            cntRsltPrcOk = 0;
            int cnt = 0, _Corr = 0, cntError = 0;
            List<svcSAPI.CError> cErrors = new List<svcSAPI.CError>();
            try
            {
                if (!pDt.HasErrors && pDt.Rows.Count > 0)
                {
                    cnt = pDt.Rows.Count;
                    #region RECORRER / PROCESAR
                    for (int i = 0; i < cnt; i++)
                    {
                        cleanSAPVta();
                        cntRsltPrc++;
                        try
                        {
                            #region MAPEO
                            try
                            {
                                pVtaNoCorr = pDt.Rows[i][0].ToString();                //  0
                                pVtaFecEmi = pDt.Rows[i][1].ToString();                //  1
                                pVtaFecVen = pDt.Rows[i][2].ToString();                //  2
                                pVtaTipo = pDt.Rows[i][3].ToString();                  //  3
                                pVtaNoSer = pDt.Rows[i][4].ToString();                 //  4
                                pVtaNoDoc = pDt.Rows[i][5].ToString();                 //  5

                                //if (pVtaNoDoc.Equals("0000013044")) pVtaNoDoc = pVtaNoDoc;  // para seleccion
                                //else continue;

                                pVtaCliTipDoc = pDt.Rows[i][6].ToString();             //  6
                                pVtaCliNoDoc = pDt.Rows[i][7].ToString();              //  7
                                pVtaCliRazSoc = pDt.Rows[i][8].ToString();             //  8
                                pVtaValFacExpo = pDt.Rows[i][9].ToString();            //  9
                                pVtaBaseImpoOpeGrav = pDt.Rows[i][10].ToString();      // 10
                                pVtaImpoOpeExo = pDt.Rows[i][11].ToString();           // 11
                                pVtaImpoOpeIna = pDt.Rows[i][12].ToString();           // 12
                                pVtaISC = pDt.Rows[i][13].ToString();                  // 13
                                pVtaIGV = pDt.Rows[i][14].ToString();                  // 14
                                pVtaOtrTrib = pDt.Rows[i][15].ToString();              // 15
                                pVtaProp = pDt.Rows[i][16].ToString();                 // 16
                                pVtaImpTotal = pDt.Rows[i][17].ToString();             // 17
                                pVtaTipCbo = pDt.Rows[i][18].ToString();               // 18

                                try
                                {
                                    pVtaRefFec = pDt.Rows[i][19].ToString();               // 19
                                    pVtaRefTipo = pDt.Rows[i][20].ToString();              // 20
                                    pVtaRefNoSer = pDt.Rows[i][21].ToString();             // 21
                                    ptaRefNoDoc = pDt.Rows[i][22].ToString();              // 22
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error en estructura de información. Campos Incorrectos. " + Environment.NewLine + ex.Message);
                            }
                            #endregion

                            #region CALCULO.VARIABLES / VALIDACIONES
                            // validamos serie
                            if (string.IsNullOrEmpty(pVtaNoSer)) throw new Exception("Error en estructura de información. Campo NroSerie. Valor(4)[" + pVtaNoSer + "]");

                            // si no tiene fecha valida
                            DateTime dtmFlagFecEmi = DateTime.Now;
                            try
                            {
                                dtmFlagFecEmi = Convert.ToDateTime(pVtaFecEmi, System.Globalization.CultureInfo.GetCultureInfo("en-Us").DateTimeFormat);
                            }
                            catch (FormatException fex)
                            {
                                throw new Exception("Error en estructura de información. Campo FechaEmisión. Valor(1)[" + pVtaFecEmi + "]");
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error en estructura de información. Campo FechaEmisión. Valor(1)[" + pVtaFecEmi + "]");
                            }
                            pVtaFecEmi = dtmFlagFecEmi.ToString("dd/MM/yyyy");

                            // operacion grabada
                            double dblOpeGrab = 0;
                            if (!Double.TryParse(pVtaBaseImpoOpeGrav, out dblOpeGrab)) throw new Exception("Error en estructura de información. Campo Operación Grabada. Valor(10)[" + pVtaBaseImpoOpeGrav + "]");

                            // validar igualdad del igv excel con el calculado
                            double dblIgv = 0, _igvCalc = 0;
                            if (!Double.TryParse(pVtaIGV, out dblIgv)) throw new Exception("Error en estructura de información. Campo IGV. Valor(14)[" + pVtaIGV + "]");
                            _igvCalc = Math.Round(dblOpeGrab * 0.18, 3);
                            double _igvDif = 0;
                            if (!double.Equals(dblIgv, _igvCalc)) _igvDif = dblIgv - _igvCalc;

                            // otros tributos
                            double dblOtrTrib = 0;
                            if (!Double.TryParse(pVtaOtrTrib, out dblOtrTrib)) throw new Exception("Error en estructura de información. Campo Otros Tributos. Valor(15)[" + pVtaOtrTrib + "]");

                            // propinas
                            double dblProp = 0;
                            if (!Double.TryParse(pVtaProp, out dblProp)) throw new Exception("Error en estructura de información. Campo Propinas. Valor(16)[" + pVtaProp + "]");

                            // total
                            double dblTotal = 0;
                            dblTotal = dblOpeGrab + _igvCalc + dblOtrTrib + dblProp + Math.Round(_igvDif, 3);

                            bool flagNotaCredito = dblOpeGrab < 0;

                            string centroCosto = string.Empty;
                            string serie = flagNotaCredito ? pVtaRefNoSer.Trim() : pVtaNoSer.Trim();
                            centroCosto = serie.Equals("FFBF269908") ? "00001" : serie.Equals("FFBF269898") ? "00002" : string.Empty;
                            if (string.IsNullOrEmpty(centroCosto))
                                centroCosto = (serie.Equals("B005") || serie.Equals("F005")) ? "00001" : (serie.Equals("B006") || serie.Equals("F006")) ? "00002" : string.Empty;
                            if (string.IsNullOrEmpty(centroCosto)) throw new Exception("Error en estructura de información. No se pudo identificar Centro de Costo. Campos de Serie: Serie_Valor(4)[" + pVtaNoSer + "], RefSerie_Valor(21)[" + pVtaRefNoSer + "], NC[" + (flagNotaCredito ? "SI" : "NO") + "]");
                            #endregion

                            #region CLIENTE
                            string CardCode = string.Empty;
                            if (!(pVtaCliTipDoc.Equals("1") || pVtaCliTipDoc.Equals("6") || pVtaCliTipDoc.Equals("4")))
                            {
                                // CLIENTE VARIOS
                                CardCode = "CL99999999999"; pVtaCliRazSoc = "CLIENTES VARIOS";
                            }
                            else
                                // BUCAR CLIENTE
                                CardCode = new svcSAPC.OCRDClient().FP_BUSCAR_OCRD(new svcSAPC.OCRD()
                                {
                                    LicTradNum = pVtaCliNoDoc, // "20548062480",
                                    Tipo = "C"
                                }, ResourceSap.Company, ResourceSap.Key).CardCode;

                            if (string.IsNullOrEmpty(CardCode))
                                if (dblTotal <= 700 && (pVtaCliTipDoc.Equals("1")))
                                {
                                    // CLIENTE VARIOS
                                    CardCode = "CL99999999999"; pVtaCliRazSoc = "CLIENTES VARIOS";
                                }
                                else
                                    // INSERTAR CLIENTE
                                    try
                                    {
                                        CardCode = InsertarClieProv("C", pVtaCliTipDoc, pVtaCliRazSoc, pVtaCliNoDoc);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message.Replace("...", ", Total>700[SI]."));
                                    }
                            #endregion

                            #region CAMPOS.USUARIO
                            List<svcSAPI.CampoUsuario> lstCU = new List<svcSAPI.CampoUsuario>();
                            #region U_LGS_TDOC
                            if (!string.IsNullOrEmpty(pVtaTipo)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_TDOC",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = pVtaTipo // "12"
                            });
                            #endregion
                            #region U_LGS_SDOC
                            if (!string.IsNullOrEmpty(pVtaNoSer)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_SDOC",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = pVtaNoSer.Substring(pVtaNoSer.Length - 4, 4) // pVtaNoSer.Trim() // .Substring(0, 4) // "F555"
                            });
                            #endregion
                            #region U_LGS_CDOC
                            if (!string.IsNullOrEmpty(pVtaNoDoc)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_CDOC",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = pVtaNoDoc // "000184"
                            });
                            #endregion
                            #region U_LGS_TOPE
                            if (!string.IsNullOrEmpty(pVtaNoDoc)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_TOPE",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = "01"
                            });
                            #endregion
                            #region U_LGS_TIPO
                            if (!string.IsNullOrEmpty(pVtaNoDoc)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_TIPO",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = "VEN"
                            });
                            #endregion
                            #region U_GS_SerTi 
                            if (!string.IsNullOrEmpty(serie)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_GS_SerTi",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = serie
                            });
                            #endregion

                            // SOLO CUANDO ES NOTA DE CREDITO
                            if (flagNotaCredito)
                            {
                                #region U_LGS_TDOO
                                //U_LGS_TDOO(Tipo documento origen), 
                                if (!string.IsNullOrEmpty(pVtaRefTipo)) lstCU.Add(new svcSAPI.CampoUsuario()
                                {
                                    Nombre = "U_LGS_TDOO",
                                    tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                    Valor = pVtaRefTipo
                                });
                                #endregion
                                #region U_LGS_SDOO
                                //U_LGS_SDOO(Serie documento origen)
                                if (!string.IsNullOrEmpty(pVtaRefNoSer)) lstCU.Add(new svcSAPI.CampoUsuario()
                                {
                                    Nombre = "U_LGS_SDOO",
                                    tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                    Valor = pVtaRefNoSer.Substring(pVtaRefNoSer.Length - 4, 4) // pVtaRefNoSer.Trim() // .Substring(0, 4)
                                });
                                #endregion
                                #region U_LGS_CDOO
                                //U_LGS_CDOO(Correlativo documento origen)
                                if (!string.IsNullOrEmpty(ptaRefNoDoc)) lstCU.Add(new svcSAPI.CampoUsuario()
                                {
                                    Nombre = "U_LGS_CDOO",
                                    tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                    Valor = ptaRefNoDoc
                                });
                                #endregion
                            }
                            #endregion

                            List<svcSAPI.RDR1> lstDEt = new List<svcSAPI.RDR1>();
                            #region VTA.BASE.IMPONIBLE
                            lstDEt.Add(new svcSAPI.RDR1()
                            {
                                ItemDescription = "VENTA BASE IMPONIBLE",
                                AccountCode = "7011014",
                                CostingCode = centroCosto, // "00001",
                                Price = Math.Round(Math.Abs(dblOpeGrab), 2), // 36.26,
                                Quantity = 1,
                                TaxCode = "IGV"
                            });

                            #endregion
                            #region RECARGO.POR.CONSUMO
                            lstDEt.Add(new svcSAPI.RDR1()
                            {
                                ItemDescription = "RECARGO POR CONSUMO",
                                AccountCode = "4699104",
                                CostingCode = centroCosto,
                                Price = Math.Round(Math.Abs(dblOtrTrib), 2),// 0,
                                Quantity = 1,
                                TaxCode = "IGV_EXE"
                            });
                            #endregion
                            #region PROPINAS
                            var vOtrTri = 0D;
                            vOtrTri = dblProp + _igvDif; // dblOtrTrib +
                            lstDEt.Add(new svcSAPI.RDR1()
                            {
                                ItemDescription = "PROPINAS",
                                AccountCode = "4699105",
                                CostingCode = centroCosto, // "00001",
                                Price = Math.Round(flagNotaCredito ? Math.Abs(vOtrTri) : vOtrTri, 2), // 4.71,
                                Quantity = 1,
                                TaxCode = "IGV_D"
                            });
                            #endregion

                            #region ORDR
                            List<svcSAPI.ORDR> LstORDR = new List<svcSAPI.ORDR>();
                            LstORDR.Add(new svcSAPI.ORDR()
                            {
                                CardCode = CardCode,
                                CardName = pVtaCliRazSoc,   // "GCZ ENERGIA S.A.C.",
                                DocDate = pVtaFecEmi,       // "14/08/2017",
                                DocDueDate = pVtaFecEmi,    // "14/08/2017",
                                DocTotal = Math.Round(Math.Abs(dblTotal), 2),        // 47.50,
                                TipoDoc = pVtaTipo,         // "12",
                                SerieDoc = pVtaNoSer,       // "F555",
                                NumDoc = pVtaNoDoc,         // "000184",
                                SalesPersonCode = 2,
                                TipoMovimiento = svcSAPI.ORDR.TIPOMOV.VEN,
                                CodigoExterno = pVtaNoCorr,
                                TaxDate = pVtaFecEmi,
                                Comments = "Ingresado por FTP",
                                LstCampoUsuario = lstCU.ToArray(),
                                LstDetalle = lstDEt.ToArray()
                            });
                            #endregion

                            flagPrc = "SAP";
                            #region RegistroSAP
                            List<svcSAPI.CError> lstResul = null;
                            if (flagNotaCredito)
                                lstResul = new svcSAPI.SORDRClient().FP_Nota_Credito_ORDR(LstORDR.ToArray(), ResourceSap.Company, ResourceSap.Key).ToList();
                            else
                                lstResul = new svcSAPI.SORDRClient().FP_Solicitud_Venta_Masiva_ORDR(LstORDR.ToArray(), ResourceSap.Company, ResourceSap.Key).ToList();
                            #endregion
                            if (lstResul == null) throw new Exception("Error al comunicarse con serivio SAPI");
                            cErrors.Add(lstResul.First());
                            if (lstResul.First().Error == "(13) [TN] LGS - La numeración legal ya ha sido registrada")
                                cntError++;
                            flagPrc = "FtpOssosi";
                            #region RegistroProcesoFTP
                            modLogProcess logPrc = new modLogProcess();
                            logPrc.FPrcId = pModFP.Id;
                            _Corr = 0;
                            int.TryParse(pVtaNoCorr, out _Corr);
                            logPrc.DocItem = _Corr;
                            logPrc.DocDate = pVtaFecEmi;
                            logPrc.DocType = pVtaTipo;
                            logPrc.DocSeries = pVtaNoSer;
                            logPrc.DocNumber = pVtaNoDoc;
                            logPrc.PrcResult = lstResul[0].Result;
                            logPrc.PrcDocEntry = lstResul[0].DocEntry;
                            logPrc.PrcError = lstResul[0].Error;
                            logPrc.Ins();
                            #endregion
                            LstDes.Add($"Fecha: {pVtaFecEmi}, Correlativo: {_Corr}, Documento: {pVtaTipo}-{pVtaNoSer}-{pVtaNoDoc} , DocEntry: {lstResul[0].DocEntry}, Error: {lstResul[0].Error}");

                            if (!string.IsNullOrEmpty(lstResul[0].DocEntry)) cntRsltPrcOk++;
                        }
                        catch (Exception ex)
                        {
                            #region RegistroExcepcionProcesoFTP
                            modLogProcess logPrc = new modLogProcess();
                            logPrc.FPrcId = pModFP.Id;
                            _Corr = 0;
                            int.TryParse(pVtaNoCorr, out _Corr);
                            logPrc.DocItem = _Corr;
                            logPrc.DocDate = pVtaFecEmi;
                            logPrc.DocType = pVtaTipo;
                            logPrc.DocSeries = pVtaNoSer;
                            logPrc.DocNumber = pVtaNoDoc;
                            logPrc.PrcResult = i;
                            logPrc.PrcDocEntry = string.Empty;
                            logPrc.PrcError = ex.Message;
                            logPrc.Ins();
                            LstDes.Add($"Fecha: {pVtaFecEmi},Correlativo: {_Corr}, Documento: {pVtaTipo}-{pVtaNoSer}-{pVtaNoDoc} , Error:{ex.Message}");
                            #endregion
                        }

                        var progress = (i + 1) * 100.0 / (cnt - 1);
                        if (worker.WorkerReportsProgress) worker.ReportProgress((int)progress);
                    }
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : Prc (" + flagPrc + "), FileProcessId (" + pModFP.Id.ToString() + "), Indice (" + cnt.ToString() + "), Correlativo (" + _Corr.ToString() + ")." + Environment.NewLine + ex.Message);
            }
        }
        private bool SaveCpr(ModFileProcess pModFP, DataTable pDt)
        {
            int cnt = 0;
            int j = 0;
            try
            {
                if (!pDt.HasErrors && pDt.Rows.Count > 0)
                {
                    cnt = pDt.Rows.Count;

                    bool isCab = false;
                    //string[] arrIvc = new string[3];
                    string[] arrRcp = new string[3];
                    //bool booIvc = false, 
                    bool booRcp = false;
                    List<modCprOsso> lstCpr = new List<modCprOsso>();

                    #region Recorrer / Procesar
                    for (int i = 0; i < cnt - 1; i++)
                    {
                        //cleanSAPCpr();
                        #region Mapeo y Asignación
                        try
                        {
                            var flagErr = pDt.Rows[i][1].ToString();
                            if (string.IsNullOrEmpty(flagErr) || flagErr.Trim().Equals("0")) continue;

                            // Identificar Cabecera
                            var flagRuc = pDt.Rows[i][0].ToString();                        //  0 
                            isCab = !string.IsNullOrEmpty(flagRuc.Trim());
                            if (isCab)
                            {
                                cleanSAPCpr();
                                // Campos de la Cabecera
                                pCprRuc = pDt.Rows[i][0].ToString();                        //  0 
                                pCprSupplier = pDt.Rows[i][1].ToString();                   //  1
                                pCprLocation = pDt.Rows[i][2].ToString();                   //  2
                                pCprReceiptName = pDt.Rows[i][3].ToString();                //  3
                                pCprReceiptDate = pDt.Rows[i][4].ToString();                //  4
                                pCprInvoiceName = pDt.Rows[i][5].ToString();                //  5
                                pCprInvoiceDate = pDt.Rows[i][6].ToString();                //  6
                                pCprState = pDt.Rows[i][9].ToString();                      //  9

                                throw new Exception("Registro Cabecera. Idx(" + i.ToString() + ")");
                                // continue;
                            }
                            else
                            {

                                // validamos que los datos de la cabecera esten completos
                                bool RcpDate = DateTime.TryParse(pCprReceiptDate, out DateTime dtmReceipDate);
                                if (
                                    !string.IsNullOrEmpty(pCprRuc.Trim()) ||
                                    !string.IsNullOrEmpty(pCprLocation.Trim()) ||
                                    !string.IsNullOrEmpty(pCprReceiptName.Trim()) ||
                                    RcpDate
                                    )
                                {
                                    // Campos del Detalle
                                    #region Detalle
                                    pCprArticle = pDt.Rows[i][1].ToString();                //  1
                                    pCprNetValue = pDt.Rows[i][7].ToString();               //  7
                                    pCprGrossValue = pDt.Rows[i][8].ToString();             //  8
                                    pCprMajorGroup = pDt.Rows[i][11].ToString();           // 11 
                                    pCprFamilyGroup = pDt.Rows[i][10].ToString();            // 10

                                    // Validamos que el registro de detalle tenga montos validos
                                    double dblNetValue, dblGrossValue = 0;
                                    if (
                                        (double.TryParse(pCprNetValue, out dblNetValue) && double.TryParse(pCprGrossValue, out dblGrossValue)) &&
                                        (dblNetValue != 0 || dblGrossValue != 0)
                                        )
                                    {
                                        //DateTime dtmInvoiceDate = DateTime.Now;
                                        bool ivcDate = DateTime.TryParse(pCprInvoiceDate, out DateTime dtmInvoiceDate);

                                        if (string.IsNullOrEmpty(pCprFamilyGroup)) throw new Exception("Error en estructura de información. Campo Familia [" + pCprFamilyGroup + "]");

                                        // Agregamos a la lista para luego agruparla
                                        lstCpr.Add(new modCprOsso()
                                        {
                                            Ruc = pCprRuc.Trim(),
                                            Supplier = pCprSupplier.Trim(),
                                            Location = pCprLocation.Trim(),
                                            ReceiptName = pCprReceiptName.Trim(),
                                            ReceiptDate = dtmReceipDate.ToShortDateString(),
                                            InvoiceName = pCprInvoiceName.Trim(),
                                            InvoiceDate = ivcDate ? dtmInvoiceDate.ToShortDateString() : string.Empty,
                                            State = pCprState,
                                            // Article = pCprArticle,
                                            NetValue = dblNetValue,
                                            GrossValue = dblGrossValue,
                                            // MajorGroup = pCprMajorGroup,
                                            FamilyGroup = pCprFamilyGroup.Trim()
                                        });
                                    }
                                    else
                                    {
                                        throw new Exception("Error en estructura de información. Datos del detalle. Montos inválidos Idx(" + i.ToString() + "), MontoNeto[" + pCprNetValue + "], MontoBruto[" + pCprGrossValue + "]");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    throw new Exception("Error en estructura de información. Datos de la cabecera incompletos. Idx(" + i.ToString() + ")");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            arrRcp = pCprReceiptName.Replace("#", "").Split('-');
                            booRcp = arrRcp.Length == 2; // SERE-NRODOC

                            #region RegistroProcesoFTP
                            modLogProcess logPrc = new modLogProcess();
                            logPrc.FPrcId = pModFP.Id;
                            // logPrc.FileId = pFileId;
                            logPrc.DocItem = i;
                            logPrc.DocDate = pCprReceiptDate;
                            logPrc.DocType = "01"; // booRcp ? arrRcp[0].Trim() : string.Empty;
                            logPrc.DocSeries = booRcp ? arrRcp[0].Trim() : string.Empty; ;
                            logPrc.DocNumber = booRcp ? arrRcp[1].Trim() : string.Empty; ;
                            logPrc.PrcResult = i;
                            logPrc.PrcDocEntry = "MAP";
                            logPrc.PrcError = ex.Message;
                            logPrc.Ins();
                            #endregion
                        }
                        #endregion
                    }
                    #endregion

                    // agrupar la información detallada
                    List<beCprAgp> grpLst = new List<beCprAgp>();
                    #region AgruparInformaciónDetallada
                    try
                    {
                        grpLst = (from t in lstCpr
                                  group t by new { t.Ruc, t.Location, t.ReceiptName, t.FamilyGroup } into g
                                  select new beCprAgp()
                                  {
                                      Ruc = g.First().Ruc,
                                      Supplier = g.First().Supplier,
                                      Location = g.First().Location,
                                      ReceiptName = g.First().ReceiptName,
                                      ReceiptDate = g.First().ReceiptDate,
                                      InvoiceName = g.First().InvoiceName,
                                      InvoiceDate = g.First().InvoiceDate,
                                      FamilyGroup = g.First().FamilyGroup,
                                      NetVal = g.Sum(x => x.NetValue),
                                      GrossVal = g.Sum(x => x.GrossValue)
                                  }).ToList();
                    }
                    catch (Exception ex)
                    {
                        #region RegistroProcesoFTP
                        modLogProcess logPrc = new modLogProcess();
                        logPrc.FPrcId = pModFP.Id;
                        // logPrc.FileId = pFileId;
                        logPrc.DocItem = -1;
                        logPrc.DocDate = string.Empty;
                        logPrc.DocType = string.Empty;
                        logPrc.DocSeries = string.Empty; ;
                        logPrc.DocNumber = string.Empty; ;
                        logPrc.PrcResult = 0;
                        logPrc.PrcDocEntry = "AGP";
                        logPrc.PrcError = "Error al agrupar información detallada." + Environment.NewLine + ex.Message;
                        logPrc.Ins();
                        #endregion
                    }
                    #endregion

                    // recorrer la lista agrupada y afectar a SAP
                    cnt = grpLst.Count;
                    cntRsltPrc = 0;
                    cntRsltPrcOk = 0;
                    #region RecorresListaAgrupada
                    foreach (var o in grpLst)
                    {
                        j++;
                        cntRsltPrc++;

                        try
                        {
                            arrRcp = o.ReceiptName.Replace("#", "").Split('-');
                            // arrIvc = o.InvoiceName.Replace("#", "").Split('-');

                            booRcp = arrRcp.Length == 3; // TIPO-SERE-NRODOC
                            // booIvc = arrIvc.Length == 3; // TIPO-SERE-NRODOC

                            // VALIDAMOS QUE AL MENOS UN CAMPO TENGA COMPLETO TIPO-SERE-NRODOC, SINO CONTINUAMOS EL BUCLE
                            if (!booRcp) throw new Exception("Campo incompleto para extraer Tipo-Serie-NroDoc. InvoiceName[" + o.InvoiceName + "]"); // continue; // || !booRcp
                            //if (!booIvc) throw new Exception("Campo incompleto para extraer Tipo-Serie-NroDoc. InvoiceName[" + o.InvoiceName + "]"); // continue; // || !booRcp

                            var _CprTipo = booRcp ? arrRcp[0].Trim() : string.Empty; // : booIvc ? arrIvc[0].Trim()

                            _CprTipo = _CprTipo.Equals("FC") ? "01" : string.Empty; // SI NO ES FACTURA DE COMPRAS, PONEMOS VACÍO, PARA FACILITAR LA VALIDACIÓN SGUIENTE

                            // VALIDAMOS QUE EL REGISTRO SEA FACTURA DE COMPRA, SINO CONTINUAMOS EL BUCLE
                            if (string.IsNullOrEmpty(_CprTipo)) throw new Exception("El registro no es una Factura. InvoiceName[" + o.InvoiceName + "]"); // continue;

                            #region PROVEEDOR
                            string CardCode = string.Empty;

                            if (string.IsNullOrEmpty(o.Ruc))
                            {
                                CardCode = "PL99999999999";
                                pVtaCliRazSoc = "PROVEEDOR VARIOS";
                            }
                            else
                                CardCode = new svcSAPC.OCRDClient().FP_BUSCAR_OCRD(new svcSAPC.OCRD()
                                {
                                    LicTradNum = o.Ruc, // "20548062480",
                                    Tipo = "P"
                                }, ResourceSap.Company, ResourceSap.Key).CardCode;

                            if (string.IsNullOrEmpty(CardCode)) CardCode = InsertarClieProv("P", "1", o.Supplier, o.Ruc);
                            #endregion

                            List<svcSAPI.CampoUsuario> lstCU = new List<svcSAPI.CampoUsuario>();
                            #region U_LGS_TDOC
                            if (!string.IsNullOrEmpty(_CprTipo)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_TDOC",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = _CprTipo
                            });
                            #endregion
                            #region U_LGS_SDOC
                            var _CprSerie = booRcp ? arrRcp[1].Trim() : string.Empty; // : booIvc ? arrIvc[1].Trim()
                            if (!string.IsNullOrEmpty(_CprSerie)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_SDOC",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = _CprSerie.PadLeft(4, '0').Substring(0, 4)
                            });
                            #endregion
                            #region U_LGS_CDOC
                            var _CprDoc = booRcp ? arrRcp[2].Trim() : string.Empty; // : booIvc ? arrIvc[2].Trim()
                            if (!string.IsNullOrEmpty(_CprDoc)) lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_CDOC",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = _CprDoc
                            });
                            #endregion
                            #region U_LGS_TIPO
                            lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_TIPO",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = "COM"
                            });
                            #endregion
                            #region U_LGS_TOPE
                            lstCU.Add(new svcSAPI.CampoUsuario()
                            {
                                Nombre = "U_LGS_TOPE",
                                tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                                Valor = "02"
                            });
                            #endregion

                            List<svcSAPI.PRQ1> lstPRQ1 = new List<svcSAPI.PRQ1>();
                            #region DETALLE
                            lstPRQ1.Add(new svcSAPI.PRQ1()
                            {
                                ItemName = o.FamilyGroup.Trim(),
                                Price = o.NetVal,
                                Quantity = 1,
                                // AccountCode = "4221103"
                            });
                            #endregion

                            var _rcpDate = o.ReceiptDate.Trim();
                            var _ivcDate = o.InvoiceDate.Trim();
                            // VALIDAMOS QUE AL MENOS TRAIGA UN CAMPO DE FECHA, RECEIPT / INVOICE
                            // string.IsNullOrEmpty(_ivcDate) || 
                            if (string.IsNullOrEmpty(_rcpDate)) throw new Exception("Campos de fecha vacíos. ReceiptDate[" + o.ReceiptDate + "]"); // continue; , InvoiceDate[" + o.InvoiceDate + "]

                            #region CABECERA
                            svcSAPI.OPRQ oOPRQ = new svcSAPI.OPRQ();
                            // oOPRQ.DocDate = DateTime.Now.ToString();
                            oOPRQ.DocDueDate = string.IsNullOrEmpty(_ivcDate) ? string.IsNullOrEmpty(_rcpDate) ? string.Empty : _rcpDate : _ivcDate;
                            oOPRQ.TaxDate = string.IsNullOrEmpty(_rcpDate) ? string.Empty : _rcpDate; // string.IsNullOrEmpty(_ivcDate) ? - > : _ivcDate
                            oOPRQ.RequriedDate = string.IsNullOrEmpty(_rcpDate) ? string.Empty : _rcpDate;
                            //oOPRQ.DocDueDate = string.IsNullOrEmpty(_rcpDate) ? string.Empty : _rcpDate;
                            oOPRQ.DocDate = string.IsNullOrEmpty(_rcpDate) ? string.Empty : _rcpDate;
                            oOPRQ.Comments = "Enviado por FTP";
                            oOPRQ.CardCode = CardCode;
                            oOPRQ.LstCampoUsuario = lstCU.ToArray();
                            oOPRQ.LstPRQ1 = lstPRQ1.ToArray();
                            _CprDoc = _CprDoc.PadLeft(7, '0');
                            oOPRQ.NumAtCard = $"{_CprTipo}-{_CprSerie}-{_CprDoc}";


                            List<svcSAPI.OPRQ> LstOPRQ = new List<svcSAPI.OPRQ>();
                            LstOPRQ.Add(oOPRQ);
                            #endregion

                            flagPrc = "SAP";
                            #region RegistroSAP
                            List<svcSAPI.CError> lstResul = new svcSAPI.SOPORClient().FP_Orden_Compra_OPOR(LstOPRQ.ToArray(), ResourceSap.Company, ResourceSap.Key).ToList();
                            #endregion

                            flagPrc = "FtpOssosi";
                            #region RegistroProcesoFTP
                            modLogProcess logPrc = new modLogProcess();
                            logPrc.FPrcId = pModFP.Id;
                            // logPrc.FileId = pFileId;
                            logPrc.DocItem = j;
                            logPrc.DocDate = string.IsNullOrEmpty(_rcpDate) ? string.Empty : _rcpDate;// string.IsNullOrEmpty(_ivcDate) ? -> : _ivcDate
                            logPrc.DocType = _CprTipo;
                            logPrc.DocSeries = _CprSerie;
                            logPrc.DocNumber = _CprDoc;
                            logPrc.PrcResult = lstResul[0].Result;
                            logPrc.PrcDocEntry = lstResul[0].DocEntry;
                            logPrc.PrcError = lstResul[0].Error;
                            logPrc.Ins();

                            LstDes.Add($"Fecha: {_rcpDate}, Correlativo: {j}, Documento: {_CprTipo}-{_CprSerie}-{_CprDoc} , DocEntry: {lstResul[0].DocEntry}, Error: {lstResul[0].Error}");
                            #endregion

                            if (!string.IsNullOrEmpty(lstResul[0].DocEntry)) cntRsltPrcOk++;
                        }
                        catch (Exception ex)
                        {

                            #region RegistroProcesoFTP
                            modLogProcess logPrc = new modLogProcess();
                            logPrc.FPrcId = pModFP.Id;
                            // logPrc.FileId = pFileId;
                            logPrc.DocItem = j;
                            logPrc.DocDate = string.IsNullOrEmpty(pCprInvoiceDate) ? pCprReceiptDate : pCprInvoiceDate;
                            logPrc.DocType = booRcp ? arrRcp[0].Trim() : string.Empty; // booIvc ? arrIvc[0].Trim() :
                            logPrc.DocSeries = booRcp ? arrRcp[1].Trim() : string.Empty; ; // booIvc ? arrIvc[1].Trim() :
                            logPrc.DocNumber = booRcp ? arrRcp[2].Trim() : string.Empty; ; // booIvc ? arrIvc[2].Trim() :
                            logPrc.PrcResult = j;
                            logPrc.PrcDocEntry = "ASG";
                            logPrc.PrcError = ex.Message;
                            logPrc.Ins();
                            LstDes.Add($"Fecha: {logPrc.DocDate},Correlativo: {j}, Documento: {logPrc.DocType}-{logPrc.DocSeries}-{logPrc.DocNumber} , Error:{ex.Message}");

                            #endregion
                        }

                        var progress = (j + 1) * 100.0 / cnt;
                        if (worker.WorkerReportsProgress) worker.ReportProgress((int)progress);

                    }
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : Prc (" + flagPrc + "), FileProcessId (" + pModFP.Id.ToString() + "), Indice (" + cnt.ToString() + "), Correlativo (" + j.ToString() + ")." + Environment.NewLine + ex.Message);
            }
        }
        private string InsertarClieProv(string pTipo, string pTipDoc, string pRazSoc, string pNoDoc)
        {
            try
            {
                List<svcSAPI.OCRD> LstOCRD = new List<svcSAPI.OCRD>();
                List<svcSAPI.CRD1> LstCRD1 = new List<svcSAPI.CRD1>();
                LstCRD1.Add(new svcSAPI.CRD1()
                {
                    Address = "DIRECCION FISCAL",
                    Street = "",
                    Block = "",
                    City = "",
                    ZipCode = "",
                    County = "",
                    State = "",
                    Country = "PE",
                    StreetNo = "",
                    BuildingFloorRoom = "",
                    Tipo = "F"
                });
                LstCRD1.Add(new svcSAPI.CRD1()
                {
                    Address = "DIRECCION ENVIO 01",
                    Street = "",
                    Block = "",
                    City = "",
                    ZipCode = "",
                    County = "PE",
                    State = "",
                    Country = "",
                    StreetNo = "",
                    BuildingFloorRoom = "",
                    Tipo = "E"
                });


                List<svcSAPI.CampoUsuario> lstCU = new List<svcSAPI.CampoUsuario>();
                // 4 - CARNET DE EXTRANJERIA, 7 - PASAPORTE
                bool _esExt = (pTipDoc.Equals("4") || pTipDoc.Equals("7"));
                lstCU.Add(new svcSAPI.CampoUsuario()
                {
                    Nombre = "U_GS_EsExt",
                    tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                    Valor = _esExt ? "Y" : "N"
                });
                // 1 - DNI, 6 - REG. UNICO DE CONTRIBUYENTES
                string _tpoPsn = pTipDoc == "1" ? "TPN" : pTipDoc == "6" ? "TPJ" : "SND";
                lstCU.Add(new svcSAPI.CampoUsuario()
                {
                    Nombre = "U_LGS_BPTP",
                    tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                    Valor = _tpoPsn
                });
                lstCU.Add(new svcSAPI.CampoUsuario()
                {
                    Nombre = "U_LGS_BPTD",
                    tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                    Valor = pTipDoc
                });

                if (_tpoPsn == "TPN")
                {
                    lstCU.Add(new svcSAPI.CampoUsuario()
                    {
                        Nombre = "U_LGS_BPNO",
                        tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                        Valor = string.IsNullOrEmpty(pRazSoc) ? "-" : pRazSoc
                    });
                    lstCU.Add(new svcSAPI.CampoUsuario()
                    {
                        Nombre = "U_LGS_BPN2",
                        tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                        Valor = "-" // string.IsNullOrEmpty(pCliRazSoc) ? "-" : pCliRazSoc
                    });
                    lstCU.Add(new svcSAPI.CampoUsuario()
                    {
                        Nombre = "U_LGS_BPAP",
                        tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                        Valor = string.IsNullOrEmpty(pRazSoc) ? "-" : pRazSoc
                    });
                    lstCU.Add(new svcSAPI.CampoUsuario()
                    {
                        Nombre = "U_LGS_BPAM",
                        tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico,
                        Valor = "-" // string.IsNullOrEmpty(pCliRazSoc) ? "-" : pCliRazSoc
                    });
                }

                int iTDoc = 0;
                int.TryParse(pTipDoc, out iTDoc);
                LstOCRD.Add(new svcSAPI.OCRD()
                {
                    //LicTradNum = pVtaCliNoDoc,
                    LicTradNum = pNoDoc,
                    CardCode = "",
                    U_GS_EsExt = _esExt ? "Y" : "N", // N , NO EXTRANJERO .. SINO Y
                    CardName = string.IsNullOrEmpty(pRazSoc) ? "-" : pRazSoc, //_tpoPsn == "TPJ" || _tpoPsn == "SND" ? string.IsNullOrEmpty(pRazSoc) ? "-" : pRazSoc : string.Empty, // 
                    Nombre1 = _tpoPsn == "TPN" ? string.IsNullOrEmpty(pRazSoc) ? "-" : pRazSoc : string.Empty,
                    Nombre2 = "",
                    ApellidoP = _tpoPsn == "TPN" ? string.IsNullOrEmpty(pRazSoc) ? "-" : pRazSoc : string.Empty,
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
                    TipoPersona = _tpoPsn, // J
                    TipDoc = iTDoc,
                    CardType = pTipo, // "C/P", // modificar
                    GroupCode = pTipo.Equals("C") ? 100 : pTipo.Equals("P") ? 101 : 0, // 100 / 101, // modificar
                    LstCRD1 = LstCRD1.ToArray(),
                    LstCampoUsuario = lstCU.ToArray()
                });

                svcSAPI.CError rslt = new svcSAPI.SOCRDClient().SP_INSERTAR_OCRD(LstOCRD.ToArray(), ResourceSap.Company, ResourceSap.Key).ToList()[0];
                // validar , SI error 
                if (string.IsNullOrEmpty(rslt.Error) && !string.IsNullOrEmpty(rslt.DocEntry))
                {
                    return rslt.DocEntry;
                }
                else
                {
                    throw new Exception("Error al Insertar[" + pTipo + "], Tipo[" + pVtaCliTipDoc + "], RazSoc[" + pVtaCliRazSoc + "], NroDoc[" + pVtaCliNoDoc + "]... SAP: DocEntry[" + rslt.DocEntry + "]Result[" + rslt.Result + "], Error[" + rslt.Error + "]");
                }
            }
            catch (Exception ex)
            {
                throw ex; //  new Exception("Error al settear los valores para Insertar[" + pTipo + "], Excepción[" + ex.Message + "]");
            }
        }
        #endregion

        #region Varios
        private DataTable GetDataExcel(string fullPath, string vName)
        {
            DataTable _dt = new DataTable();
            try
            {
                var _sheet = Excel.utlExcel.getSheets(fullPath);
                string _iSheet = string.Empty;
                if (_sheet != null && _sheet.Count > 0)
                {
                    // seteamos el valor de la hoja segun el archivo a procesar
                    _iSheet = vName.StartsWith("rvcr") ? "RegistroVentasDrll_MFP-Rest" :
                                vName.StartsWith("rvcc") ? "RegistroVentasDrll_MFP" :
                                vName.StartsWith("c") ? "AllChecksTenderDtl-Visa" :
                                vName.StartsWith("rv") ? "Sheet1" :
                                vName.StartsWith("rc") ? "Report" :
                                "-";

                    // obtenemos la data
                    _dt = Excel.utlExcel.getDataXlsToDt(fullPath, _iSheet); // _sheet[_iSheet]
                    return _dt;
                }
                else
                {
                    throw new Exception("Error en Excel. No se puede leer el archivo. Hoja[" + _iSheet + "]");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Excel. Archivo(" + vName + ") en la Ruta(" + fullPath + ")." + Environment.NewLine + ex.Message);
            }
        }
        #endregion

        #region VentasChek/Cobranzas
        private bool ProcVtaChk(ModFileProcess pModFP, DataTable pDt)
        {
            int cnt = 0;
            try
            {
                flagPrc = "VentasCheck";
                cnt = pDt.Rows.Count;

                pDt.TableName = string.Concat("v", pModFP.FileId);

                // eliminar las filas q no sirven
                DataTable vDtVta = pDt.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<string>(3))).CopyToDataTable();
                vDtVta.TableName = "dtV"; //  string.Concat("dtC", pModFP.FileId);

                if (vDtVta.Rows.Count > 0 && dsPrcChk.Tables["dtV"] != null)
                {
                    dsPrcChk.Tables["dtV"].Merge(vDtVta);
                }
                else
                {
                    dsPrcChk.Tables.Add(vDtVta);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : Prc (" + flagPrc + "), FileProcessId (" + pModFP.Id.ToString() + "), Indice (" + cnt.ToString() + ") )." + Environment.NewLine + ex.Message);
            }
        }
        private bool ProcRegComp(ModFileProcess pModFP, DataTable pDt)
        {
            int cnt = 0;
            try
            {
                flagPrc = "RegistroCompras";
                cnt = pDt.Rows.Count;

                pDt.TableName = string.Concat("rc", pModFP.FileId);

                // eliminar las filas q no sirven
                DataTable vDtCmp = pDt.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<string>(3))).CopyToDataTable();
                vDtCmp.TableName = "dtRc"; //  string.Concat("dtC", pModFP.FileId);

                if (vDtCmp.Rows.Count > 0 && dsPrcChk.Tables["dtRc"] != null)
                {
                    dsPrcChk.Tables["dtRc"].Merge(vDtCmp);
                }
                else
                {
                    dsPrcChk.Tables.Add(vDtCmp);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : Prc (" + flagPrc + "), FileProcessId (" + pModFP.Id.ToString() + "), Indice (" + cnt.ToString() + ") )." + Environment.NewLine + ex.Message);
            }
        }
        private bool ProcCbrz(ModFileProcess pModFP, DataTable pDt)
        {
            int cnt = 0;
            try
            {
                flagPrc = "Cobranzas";
                cnt = pDt.Rows.Count;

                // eliminar las filas q no sirven
                DataTable vDtCobr = pDt.AsEnumerable().Where(r => !String.IsNullOrEmpty(r.Field<string>(2)) && Information.IsNumeric(r.Field<string>(2))).CopyToDataTable();
                vDtCobr.TableName = "dtC"; //  string.Concat("dtC", pModFP.FileId);

                if (vDtCobr.Rows.Count > 0 && dsPrcChk.Tables["dtC"] != null)
                {
                    dsPrcChk.Tables["dtC"].Merge(vDtCobr);
                }
                else
                {
                    dsPrcChk.Tables.Add(vDtCobr);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : Prc (" + flagPrc + "), FileProcessId (" + pModFP.Id.ToString() + "), Indice (" + cnt.ToString() + ") )." + Environment.NewLine + ex.Message);
            }
        }
        private bool ProcMergeCheck()
        {
            try
            {
                // cobranzas    :> col: Check
                // ventas       :> col: Tkt | Chk

                var rslt = from tC in dsPrcChk.Tables["dtC"].AsEnumerable()
                           join tV in dsPrcChk.Tables["dtV"].AsEnumerable()
                            on tC[2] equals (tV[3].ToString().Split('|'))[1]
                           select new
                           {
                               tienda = tV[0],
                               tk_chk = tV[3],
                               chk = tC[2],
                               tk_Amount = tC[6]
                           };
                List<svcSAPI.ORCT> Lst = new List<svcSAPI.ORCT>();
                foreach (var item in rslt.ToList())
                {
                    string ModoPago = "VISA";
                    svcSAPI.ORCT obj = new svcSAPI.ORCT();
                    obj.JrnlMemo = item.tk_chk.ToString().Split('|')[0];
                    obj.CardCode = "";
                    obj.DocDate = null;
                    obj.CashSum = double.Parse(item.tk_Amount.ToString());
                    if (ModoPago.ToUpper() == "VISA")
                    {
                        obj.CashAccount = "1031101";
                    }
                    else if (ModoPago.ToUpper() == "EFECTIVO")
                    {
                        obj.CashAccount = "1031109";
                    }
                    List<svcSAPI.CampoUsuario> LstCU = new List<svcSAPI.CampoUsuario>();
                    LstCU.Add(new svcSAPI.CampoUsuario()
                    {
                        Nombre = "U_LGS_TPCE",
                        Valor = "PRE",
                        tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico
                    });
                    LstCU.Add(new svcSAPI.CampoUsuario()
                    {
                        Nombre = "U_LGS_MEDPAG",
                        Valor = "003",
                        tipo = svcSAPI.CampoUsuario.m_tipos.alfanumerico
                    });
                    obj.LstCampoUsuario = LstCU.ToArray();
                    Lst.Add(obj);
                }
                List<svcSAPI.CError> cErrors = new svcSAPI.SORDRClient().FP_Cobranzas_Masiva_ORDR(Lst.ToArray(), ResourceSap.Company, ResourceSap.Key).ToList();

                //var table = dsPrcChk.Tables[0].AsEnumerable()
                //   .Where(r => r.Field<string>("col1") != "ali")
                //   .CopyToDataTable();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region FTP
        public IList<modFtpFileDet> directoryListDetailedFtp(string pD)
        {
            List<modFtpFileDet> list = new List<modFtpFileDet>();
            try
            {
                var ftpRequest = (FtpWebRequest)WebRequest.Create(pD);
                ftpRequest.Credentials = new NetworkCredential(ftpU, ftpP);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                using (var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    using (var ftpStream = ftpResponse.GetResponseStream())
                    {
                        using (var ftpReader = new StreamReader(ftpStream))
                        {
                            try
                            {
                                while (ftpReader.Peek() != -1)
                                {
                                    string[] arrTo = new string[10];
                                    int c = 0;
                                    string[] arrFrom = ftpReader.ReadLine().Split(' ');
                                    for (int i = 0; i < arrFrom.Length; i++)
                                    {
                                        if (arrFrom[i] != string.Empty)
                                        {
                                            arrTo[c] = arrFrom[i];
                                            c++;
                                        }
                                    }
                                    decimal dc = 0;
                                    Decimal.TryParse(arrTo[4], out dc);

                                    // lista para el grid
                                    list.Add(new modFtpFileDet()
                                    {
                                        Access = arrTo[0],
                                        Type = arrTo[1],
                                        Ftp1 = arrTo[2],
                                        Ftp2 = arrTo[3],
                                        Size = dc,
                                        Date = arrTo[5] + " " + arrTo[6],
                                        Hour = arrTo[7],
                                        Name = arrTo[8],
                                        Path = pD
                                    });

                                    // SOLO SE GUARDARAN EN LA BD AQUELLOS ARCHIVOS QUE CUMPLAN CON 
                                    // 1 Nombre diferente de vacío
                                    // 2 RegistroCompras  RC / RegistroVentas RV
                                    // 3 Con extensión XLS / XLSX
                                    // 4 Que el archivo sea de la sucursal OSSO
                                    string vName = arrTo[8];
                                    if (!string.IsNullOrEmpty(vName)
                                        && (vName.StartsWith("rvcr", StringComparison.CurrentCultureIgnoreCase)
                                            || vName.StartsWith("rvcc", StringComparison.CurrentCultureIgnoreCase)
                                            || vName.StartsWith("c", StringComparison.CurrentCultureIgnoreCase)
                                            || vName.StartsWith("rv", StringComparison.CurrentCultureIgnoreCase)
                                            || vName.StartsWith("rc", StringComparison.CurrentCultureIgnoreCase)

                                            )
                                        && (
                                            vName.EndsWith(".xls", StringComparison.CurrentCultureIgnoreCase)
                                            || vName.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase)
                                            )
                                        && (
                                            vName.ToLower().Contains("osso")
                                            ))
                                    {
                                        // GUARDAMOS EN LA BD
                                        modFile mFile = new modFile();
                                        mFile.Name = vName;
                                        mFile.Path = pD;
                                        mFile.UplFtpUsr = ftpU;
                                        mFile.UplDatetime = arrTo[5] + " " + arrTo[6];
                                        mFile.Upload();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show("Error al listar." + Environment.NewLine + "Excepción :" + ex.ToString());
                            }
                        }
                    }
                }

                return list;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return new List<modFtpFileDet>();
        }

        public void moveFile(string fileName)
        {
            try
            {
                string fecha = DateTime.Now.ToString("yyyyMMdd_hHH");
                if (!createDirectory(ftpD2 + @"\" + fecha)) return;

                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpD1 + @"\" + fileName);
                ftpRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                ftpRequest.Credentials = new NetworkCredential(ftpU, ftpP);
                ftpRequest.RenameTo = (ftpD2 + @"\" + fecha + fileName);
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            }
            catch (WebException exc)
            {
                Console.WriteLine(exc.Message.ToString());
                String status = ((FtpWebResponse)exc.Response).StatusDescription;
                Console.WriteLine(status);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public bool createDirectory(string pDirName)
        {
            try
            {
                WebRequest ftpRequest = WebRequest.Create(pDirName);
                ftpRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Credentials = new NetworkCredential(ftpU, ftpP);
                using (var resp = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    Console.WriteLine(resp.StatusCode + Environment.NewLine + resp.StatusDescription);
                }
                return true;
            }
            catch (WebException exc)
            {
                Console.WriteLine(exc.Message.ToString());
                String status = ((FtpWebResponse)exc.Response).StatusDescription;
                Console.WriteLine(status);
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
        public void upload(string destination, string fullPathFile)
        {
            try
            {
                string fileName = new FileInfo(fullPathFile).Name;
                // siempre en el directorio de RECIBIDOS
                var ftpRequest = (FtpWebRequest)WebRequest.Create(destination + @"\" +
                            Excel.utlExcel.removeDiacritics(fileName.Replace(' ', '_')));
                ftpRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpRequest.Credentials = new NetworkCredential(ftpU, ftpP);

                using (var inputStream = File.OpenRead(PLocalFile))
                {
                    using (var outputStream = ftpRequest.GetRequestStream())
                    {
                        var buffer = new byte[1024];
                        int totalReadBytesCount = 0;
                        int readBytesCount;
                        while ((readBytesCount = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            outputStream.Write(buffer, 0, readBytesCount);
                            totalReadBytesCount += readBytesCount;
                            var progress = totalReadBytesCount * 100.0 / inputStream.Length;
                            worker.ReportProgress((int)progress);
                        }
                    }
                }

                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                System.Windows.MessageBox.Show("Se cargó satisfactoriamente" + Environment.NewLine + ftpResponse.StatusDescription);

                Prog = 0;

                Validar(null);
            }
            catch (WebException exc)
            {
                Console.WriteLine(exc.Message.ToString());
                String status = ((FtpWebResponse)exc.Response).StatusDescription;
                Console.WriteLine(status);
            }
            catch (Exception ex)
            {
                // NotificationBox.Show(ex.Message, NotificationBox.NotificationType.Error);

                System.Windows.MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public void download(string remoteFile, long siz, string fileLocal)
        {
            try
            {
                // siempre de descargará desde RECIBIDOS
                var ftpRequest = (FtpWebRequest)FtpWebRequest.Create(ftpD1 + "//" + remoteFile);
                ftpRequest.Credentials = new NetworkCredential(ftpU, ftpP);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                using (var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    using (var ftpStream = ftpResponse.GetResponseStream())
                    {
                        using (var localFileStream = new FileStream(fileLocal, FileMode.Create))
                        {
                            byte[] buffer = new byte[1024];
                            int totalBytesRead = 0;
                            int bytesRead;
                            try
                            {
                                while ((bytesRead = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    localFileStream.Write(buffer, 0, bytesRead);
                                    totalBytesRead += bytesRead;
                                    var progress = totalBytesRead * 100.0 / siz;
                                    if (worker.WorkerReportsProgress) worker.ReportProgress((int)progress);
                                }
                            }
                            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                        }
                    }
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message.ToString());
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                Console.WriteLine(status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
        public void processFtp()
        {
            try
            {
                dsPrcChk = new DataSet();
                modFile mFile = new modFile();
                List<modFile> fileList = mFile.List();

                // solo los archivos que no han sido procesados en su totalidad
                var _list = fileList; // todo, sin distinguir lo procesado
                                      // var _list = fileList.Where(x => string.IsNullOrEmpty(x.PrcFtpUsr)).ToList(); // solo aquellos que no estan procesados
                                      //var _list = fileList.Where(x=>x.Name.Contains("rc")).ToList(); // para seleccion

                // var _LstFile1 = new List<modFtpFileDet>() { m }; // para seleccion

                foreach (modFile x in _list)
                {
                    LstDes = new List<string>();
                    try
                    {
                        #region Descargar
                        M = LstFile1.FirstOrDefault(y => y.Name.Equals(x.Name) && y.Path.Equals(x.Path));
                        // M = _LstFile1.FirstOrDefault(y => y.Name == x.Name);  // para seleccion

                        if (M == null) continue; // throw new Exception("No se encuentra archivo con el mismo nombre (" + x.Name + ") en la ruta FTP.");  

                        PLocalPath = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Files"; //  @Environment.CurrentDirectory
                        string fullPath = PLocalPath + @"\" + M.Name;
                        string vName = M.Name.ToLower();
                        //string flagDoc = M.Name.ToLower().Substring(0, 2);
                        //string flagSuc = M.Name.ToLower().Substring(2, 4);

                        //if (vName.StartsWith("rc")) continue; //  throw new Exception("Se bloqueó procesar archivos de compras. Archivo(" + x.Name + ") en la Ruta(" + x.Path + ")."); //

                        // PROCESAMOS por AHORA SOLO  sucursal : OSSO
                        if (M != null && vName.Contains("osso")) download(M.Name, (long)M.Size, fullPath);
                        else continue; // throw new Exception("El archivo no contiene el formato de nombre correcto ('osso')");
                        #endregion

                        DataTable _dt = new DataTable();
                        #region LeerXls
                        try
                        {
                            var _sheet = Excel.utlExcel.getSheets(fullPath);
                            string _iSheet = string.Empty;
                            if (_sheet != null && _sheet.Count > 0)
                            {
                                // seteamos el valor de la hoja segun el archivo a procesar
                                _iSheet = vName.StartsWith("rvcr") ? "RegistroVentasDrll_MFP-Rest" :
                                            vName.StartsWith("rvcc") ? "RegistroVentasDrll_MFP" :
                                            vName.StartsWith("c") ? "AllChecksTenderDtl-Visa" :
                                            vName.StartsWith("rv") ? "Sheet1" :
                                            vName.StartsWith("rc") ? "Report" :
                                            "-";

                                // obtenemos la data
                                _dt = Excel.utlExcel.getDataXlsToDt(fullPath, _iSheet); // _sheet[_iSheet]
                            }
                            else
                            {
                                throw new Exception("Error en Excel. No se puede leer el archivo. Hoja[" + _iSheet + "]");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error en Excel. Archivo(" + x.Name + ") en la Ruta(" + x.Path + ")." + Environment.NewLine + ex.Message);
                        }
                        #endregion

                        if (_dt.Rows.Count > 0)
                        {
                            #region ProcesarSAP
                            bool rslt = false;

                            #region FileProcess_Start
                            var modFP = new ModFileProcess();
                            modFP.FileId = x.Id;
                            modFP.RegCount = _dt.Rows.Count;
                            modFP.CompanyDB = ResourceSap.Company;
                            if (!modFP.Start()) throw new Exception("Error SQL_FTP al registrar Inicio de Proceso (FileProcessStart)");
                            #endregion

                            // procesamos segun archivo
                            rslt = vName.StartsWith("rvcr") ? ProcVtaChk(modFP, _dt) :
                                    vName.StartsWith("rvcc") ? ProcVtaChk(modFP, _dt) :
                                    vName.StartsWith("c") ? ProcCbrz(modFP, _dt) :
                                    vName.StartsWith("rv") ? SaveVta(modFP, _dt) :
                                    vName.StartsWith("rc") ? SaveCpr(modFP, _dt) :
                                    false;

                            #region FileProcess_End
                            modFP.PrcCount = cntRsltPrc;
                            modFP.PrcOk = cntRsltPrcOk;
                            modFP.End();
                            #endregion

                            // MARCAMOS EL  ULTIMO PROCESO DEL ARCHIVO x:=File
                            x.PrcFtpUsr = ftpU;
                            x.Process();


                            if (flagDes)
                            {
                                string pathDoc = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $@"\FtpLog";
                                if (!Directory.Exists(pathDoc))
                                    Directory.CreateDirectory(pathDoc);
                                string archi = $@"{pathDoc}\{Path.GetFileNameWithoutExtension(M.Name)}_{DateTime.Now.ToString("ddMMyyyy_hhmmss")}.txt";
                                using (TextWriter tw = new StreamWriter(archi))
                                {
                                    foreach (String s in LstDes)
                                        tw.WriteLine(s);
                                }
                            }
                            // movemos el archivo a la carpeta procesados
                            moveFile(M.Name);
                            #endregion

                        }
                    }
                    catch (Exception ex)
                    {
                        #region FileProcess_Observación
                        var modFP = new ModFileProcess();
                        modFP.FileId = x.Id;
                        modFP.CompanyDB = ResourceSap.Company;
                        modFP.Observation = ex.Message;
                        modFP.Obs();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                ProcMergeCheck();
            }
        }
        #endregion

        #region LOCAL
        public IList<modFtpFileDet> directoryListDetailedLocal(string pLocalPath)
        {
            List<modFtpFileDet> list = new List<modFtpFileDet>();
            try
            {
                if (Directory.Exists(pLocalPath))
                    list = new DirectoryInfo(pLocalPath).GetFiles().Select(z => new modFtpFileDet() { Name = z.Name, Date = z.CreationTime.ToString(), Size = z.Length, Path = z.DirectoryName }).ToList();

                if (list.Count <= 0) return list;

                try
                {
                    foreach (modFtpFileDet item in list)
                    {
                        // SOLO SE GUARDARAN EN LA BD AQUELLOS ARCHIVOS QUE CUMPLAN CON 
                        // 1 Nombre diferente de vacío
                        // 2 RegistroCompras  RC / RegistroVentas RV
                        // 3 Con extensión XLS / XLSX
                        // 4 Que el archivo sea de la sucursal OSSO
                        string vName = item.Name;
                        if (!string.IsNullOrEmpty(vName)
                            && (
                                vName.StartsWith("rv", StringComparison.CurrentCultureIgnoreCase)
                                || vName.StartsWith("rc", StringComparison.CurrentCultureIgnoreCase)
                                || vName.StartsWith("rvcr", StringComparison.CurrentCultureIgnoreCase)
                                || vName.StartsWith("rvcc", StringComparison.CurrentCultureIgnoreCase)
                                || vName.StartsWith("c", StringComparison.CurrentCultureIgnoreCase)
                                )
                            && (
                                vName.EndsWith(".xls", StringComparison.CurrentCultureIgnoreCase)
                                || vName.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase)
                                )
                            && (
                                vName.ToLower().Contains("osso")
                                ))
                        {
                            // GUARDAMOS EN LA BD
                            modFile mFile = new modFile();
                            mFile.Name = vName;
                            mFile.Path = item.Path;
                            mFile.UplFtpUsr = ftpU;
                            mFile.UplDatetime = item.Date;
                            mFile.Upload();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error al listar." + Environment.NewLine + "Excepción :" + ex.ToString());
                }

                return list;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return new List<modFtpFileDet>();
        }
        public void processLocal()
        {
            try
            {
                //List<string> Lst = new List<string>
                //{ "H655630920957|NICOLAS HERMAN|4"
                //};
                //List<string> Rs = new List<string>();
                //foreach (var item in Lst)
                //{
                //    string LicTradNum = item.Split('|')[0];
                //    string RazSon = item.Split('|')[1];
                //    string Tipo = item.Split('|')[2];
                //    Rs.Add(InsertarClieProv("C", Tipo, RazSon, LicTradNum));
                //}
                //List<string> err = Rs.Where(x => x == "").ToList();
                //return;
                dsPrcChk = new DataSet();
                modFile mFile = new modFile();
                List<modFile> fileList = mFile.List();

                // solo los archivos que no han sido procesados en su totalidad
                var _list = fileList; // todo, sin distinguir lo procesado
                                      //var _list = fileList.Where(x => string.IsNullOrEmpty(x.PrcFtpUsr)).ToList(); // solo aquellos que no estan procesados
                                      // var _list = fileList.ToList(); // para seleccion

                // var _LstFile1 = new List<modFtpFileDet>() { m }; // para seleccion
                foreach (modFile x in _list)
                {
                    LstDes = new List<string>();
                    try
                    {
                        #region LeerLocal
                        M = LstFile1.FirstOrDefault(y => y.Name == x.Name && y.Path == x.Path);
                        // M = _LstFile1.FirstOrDefault(y => y.Name == x.Name);  // para seleccion

                        if (M == null) continue; // throw new Exception("No se encuentra archivo con el mismo nombre (" + x.Name + ") en la ruta (" + x.Path + ").");  // continue;

                        string fullPath = PLocalPath + @"\" + M.Name;
                        string vName = M.Name.ToLower();
                        //string flagDoc = M.Name.ToLower().Substring(0, 2);
                        //string flagSuc = M.Name.ToLower().Substring(2, 4);

                        //if (vName.StartsWith("rc")) continue; //  throw new Exception("Se bloqueó procesar archivos de compras. Archivo(" + x.Name + ") en la Ruta(" + x.Path + ")."); //
                        #endregion

                        DataTable _dt = new DataTable();
                        #region LeerXls
                        try
                        {
                            var _sheet = Excel.utlExcel.getSheets(fullPath);
                            string _iSheet = string.Empty;
                            if (_sheet != null && _sheet.Count > 0)
                            {
                                // si es ventas se lee la hoja 0, si es compras la hoja 2
                                // _iSheet = flagDoc.Equals("rv") ? "Sheet1" : flagDoc.Equals("rc") ? "Report" : "-";
                                _iSheet = vName.StartsWith("rvcr") ? "RegistroVentasDrll_MFP - Rest" :
                                            vName.StartsWith("rvcc") ? "RegistroVentasDrll_MFP" :
                                            vName.StartsWith("c") ? "AllChecksTenderDtl-Visa" :
                                            vName.StartsWith("rv") ? "Sheet1" :
                                            vName.StartsWith("rc") ? "Report" :
                                            "-";

                                _dt = Excel.utlExcel.getDataXlsToDt(fullPath, _iSheet); // _sheet[_iSheet]
                            }

                            else
                            {
                                throw new Exception("Error en Excel. No se puede leer el archivo. Hoja[" + _iSheet + "]");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error en Excel. Archivo(" + x.Name + ") en la Ruta(" + x.Path + ")." + Environment.NewLine + ex.Message);
                        }
                        #endregion

                        if (_dt.Rows.Count > 0)
                        {
                            #region ProcesarSAP
                            bool rslt = false;

                            #region FileProcess_Start
                            var modFP = new ModFileProcess();
                            modFP.FileId = x.Id;
                            modFP.RegCount = _dt.Rows.Count;
                            modFP.CompanyDB = ResourceSap.Company;
                            if (!modFP.Start()) throw new Exception("Error dbSQL_FTP al registrar Inicio de Proceso (FileProcessStart)");
                            #endregion

                            //lstFile2.Add(modFP);

                            // procesamos segun archivo
                            rslt = vName.StartsWith("rvcr") ? ProcVtaChk(modFP, _dt) :
                                    vName.StartsWith("rvcc") ? ProcVtaChk(modFP, _dt) :
                                    vName.StartsWith("c") ? ProcCbrz(modFP, _dt) :
                                    vName.StartsWith("rv") ? SaveVta(modFP, _dt) :
                                    vName.StartsWith("rc") ? SaveCpr(modFP, _dt) :
                                    false;

                            #region FileProcess_End
                            modFP.PrcCount = cntRsltPrc;
                            modFP.PrcOk = cntRsltPrcOk;
                            modFP.End();
                            #endregion

                            // MARCAMOS EL  ULTIMO PROCESO DEL ARCHIVO x:=File
                            x.PrcFtpUsr = ftpU;
                            x.Process();


                            #endregion
                        }

                        if (flagDes)
                        {
                            string archi = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                $@"\FtpLog\{Path.GetFileNameWithoutExtension(M.Name)}_{DateTime.Now.ToString("ddMMyyyy_hhmmss")}.txt";
                            using (TextWriter tw = new StreamWriter(archi))
                            {
                                foreach (String s in LstDes)
                                    tw.WriteLine(s);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        #region FileProcess_Observación
                        var modFP = new ModFileProcess();
                        modFP.FileId = x.Id;
                        modFP.CompanyDB = ResourceSap.Company;
                        modFP.Observation = ex.Message;
                        modFP.Obs();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                ProcMergeCheck();
            }
        }
        #endregion

        #region Eventos
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            worker.WorkerReportsProgress = true;
            switch (modo)
            {
                // new FileInfo(pLocalFile).Name
                case "E": upload(ftpD1, pLocalFile); break;
                case "D": download(M.Name, (long)M.Size, PLocalPath + @"\" + M.Name); break;
                case "PF": processFtp(); break;
                case "PL": processLocal(); break;
            }
        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Prog = e.ProgressPercentage;
        }
        #endregion

        #endregion

        #region Constructor
        public VMInterface(string ftpU, string ftpP, bool isSiglo = false)
        {
            this.ftpU = ftpU;
            this.ftpP = ftpP;
            this.isSiglo = isSiglo;
            this.visible = isSiglo ? Visibility.Visible : Visibility.Collapsed;
            this.h = isSiglo ? 300 : 800;

            this.flagFtp = true;

            // this.ftpF1 = isSiglo ? ftpF1: ftpF2;
            ftpD1 = ftpH + (IsSiglo ? ftpF1 : string.Empty);
            ftpD2 = ftpH + ftpF2;

            worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;

            cmdValidar = new Command(Validar);
            cmdEnviar = new Command(Enviar);
            cmdSeleccionar = new Command(Seleccionar);
            cmdDescargar = new Command(Descargar);
            cmdProcesar = new Command(Procesar);

            pLocalFile = string.Empty;
            lstFile1 = new List<modFtpFileDet>();
            lstFile2 = new ObservableCollection<ModFileProcess>();

            Validar(null);
        }
        #endregion
    }
}
