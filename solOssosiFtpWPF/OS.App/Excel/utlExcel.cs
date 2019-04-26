using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using interopExcel = Microsoft.Office.Interop.Excel;

namespace Excel
{
    public static class utlExcel
    {

        static OleDbConnection conn = null;
        static OleDbDataAdapter myDataAdapter = null;

        /// <summary>
        /// Instancia una nueva conexión hacia un archivo excel 
        /// </summary>
        /// <param name="pFilePath"></param>
        private static void setConnection(string pFilePath)
        {
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; " +
                "data source=" + pFilePath + ";Extended Properties='Excel 12.0; HDR=YES;'"); //Xml;HDR=Yes'
        }
        /// <summary>
        /// Instancia un nuevo adaptador para obtener la inforamción de un excel y hoja específicos
        /// </summary>
        /// <param name="pHoja"></param>
        /// <param name="pCols"></param>
        /// <param name="pRngo"></param>
        private static void setDataAdapter(string pHoja, string pCols = "*", string pRngo = "")
        {
            if (conn != null)
                myDataAdapter = new OleDbDataAdapter("SELECT " + pCols + " FROM [" + pHoja + "$" + pRngo + "]", conn);
        }
        /// <summary>
        /// Ventana para buscar un archivo Xlsx
        /// Devuelve la ruta
        /// </summary>
        /// <returns></returns>
        public static string getPath()
        {
            string sRslt = string.Empty;
            try
            {
                System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
                openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFile.Filter = "Excel Files (*.xlsx)|*.xlsx|Excel Files (*.xls)|*.xls";
                openFile.Title = "Seleccione el archivo de Excel";
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    if (!openFile.FileName.Equals("")) sRslt = openFile.FileName;

            }
            catch (Exception)
            {
                sRslt = string.Empty;
                throw;
            }
            return sRslt;
        }

        /// <summary>
        /// Devuelve un List<String> que contiene las Hojas de un Archivo Xls
        /// </summary>
        /// <param name="excelFile"></param>
        /// <returns></returns>
        public static IList<string> getSheets(string pFilePath)
        {
            System.Data.DataTable dt = null;
            try
            {
                setConnection(pFilePath);
                // Open connection with the database.
                conn.Open();
                // Get the data table containg the schema guid.
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null) { return null; }

                IList<string> excelSheets = new List<string>();
                int i = 0;

                // Add the sheet name to the string array.
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string vName = row["TABLE_NAME"].ToString();
                    if (!vName.Equals("Objetos$"))
                    {
                        excelSheets.Add(vName.Replace("$", string.Empty));
                        i++;
                    }
                    else { continue; }

                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                throw ex;
                // return null;

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
                if (dt != null) dt.Dispose();
            }
        }
        /// <summary>
        /// Devuelve un DataTable con la información del Archivo Xlsx y Hoja deseadas
        /// </summary>
        /// <param name="pRuta"></param>
        /// <param name="pHoja"></param>
        /// <returns></returns>
        public static System.Data.DataTable getDataXlsToDt(string pRuta, string pHoja, string pCols = "*", string pRngo = "")
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                if (!string.IsNullOrEmpty(pRuta) && !string.IsNullOrEmpty(pHoja))
                {
                    setConnection(pRuta);
                    setDataAdapter(pHoja, pCols, pRngo);
                    myDataAdapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                dt = new System.Data.DataTable();
                return dt;
                throw ex;
            }
            finally
            {
                if (conn != null) { conn.Close(); conn.Dispose(); }
                if (dt != null) dt.Dispose();
            }
        }
        /// <summary>
        /// Devuelve un valor String, que se encuentra en una Celda, segun el rango
        /// Se hace uso de la librería Interop
        /// </summary>
        /// <param name="pRuta"></param>
        /// <param name="pHoja"></param>
        /// <param name="pRngo1"></param>
        /// <param name="pRngo2"></param>
        /// <returns></returns>
        public static string getCellValueFromRange(string pRuta, string pHoja, string pRngo1, string pRngo2 = "")
        {
            string vRslt = string.Empty;

            interopExcel.Application xlApp;
            interopExcel.Workbook xlWorkBook;
            interopExcel.Worksheet xlWorkSheet;
            xlApp = new interopExcel.Application();

            try
            {
                xlWorkBook = xlApp.Workbooks.Open(pRuta, 0, true, 5, "", "", true,
                                                  Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t",
                                                  false, false, 0, true, 1, 0);
                xlWorkSheet = (interopExcel.Worksheet)xlWorkBook.Worksheets[pHoja];
                vRslt = xlWorkSheet.get_Range(pRngo1, pRngo2 == string.Empty ? pRngo1 : pRngo2).Value2;

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                releaseObject(xlApp);
            }
            return vRslt;
        }
        /// <summary>
        /// Limpiamos 
        /// </summary>
        /// <param name="obj"></param>
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw new Exception("No se pudo liberar el objeto excel." + System.Environment.NewLine + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }



        // validacion de cadenas
        public static string RemoveSpecialCharacters(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^a-zA-z0-9 ]+", "");
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }
        public static string removeDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            string strJoin = new string(chars).Normalize(NormalizationForm.FormC);
            strJoin = strJoin.Replace('Y', 'I');
            strJoin = strJoin.ToLower();
            strJoin = strJoin.Trim();
            return strJoin.Replace(" ", string.Empty);
        }

        public static void ExportarExcelDataTable(DataTable dt, string RutaExcel)
        {
            const string FIELDSEPARATOR = "\t";
            const string ROWSEPARATOR = "\n";
            System.Text.StringBuilder output = new System.Text.StringBuilder();
            // Escribir encabezados    
            foreach (DataColumn dc in dt.Columns)
            {
                output.Append(dc.ColumnName);
                output.Append(FIELDSEPARATOR);
            }
            output.Append(ROWSEPARATOR);
            foreach (DataRow item in dt.Rows)
            {
                foreach (object value in item.ItemArray)
                {
                    output.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace('.', ','));
                    output.Append(FIELDSEPARATOR);
                }
                // Escribir una línea de registro        
                output.Append(ROWSEPARATOR);
            }
            // Valor de retorno    
            // output.ToString();
            System.IO.StreamWriter sw = new System.IO.StreamWriter(RutaExcel);
            sw.Write(output.ToString());
            sw.Close();
        }

    }
}
