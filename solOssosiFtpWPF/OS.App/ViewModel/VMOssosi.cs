using Microsoft.Win32;
using OS.App.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace OS.App.ViewModel
{
    public class VMOssosi : NotifyPropertyChanged
    {
        #region Constantes
        const string ftpH = "ftp://169.47.196.208/";
        //const string ftpU = "usrOssosi";
        //const string ftpP = "mcdSssZp";
        #endregion

        #region Variables

        private string ftpU = string.Empty;
        private string ftpP = string.Empty;

        FtpSiglo oFtp = null;
        string pRemoteFile = string.Empty;
        string pLocalFile = string.Empty;
        IEnumerable<modFtpFileDet> lstFile = null;

        Command cmdValidar = null;
        Command cmdEnviar = null;
        Command cmdSeleccionar = null;
        #endregion

        #region Comandos
        public ICommand CmdValidar => cmdValidar;
        public ICommand CmdEnviar => cmdEnviar;
        public ICommand CmdSeleccionar => cmdSeleccionar;
        #endregion

        #region Propiedades
        public string PLocalFile
        {
            get { return pLocalFile; }
            set { SetProperty(() => pLocalFile = value, () => pLocalFile == value); }
        }
        public IEnumerable<modFtpFileDet> LstFile
        {
            get { return lstFile; }
            private set { SetProperty(ref lstFile, value); }
        }
        #endregion

        #region Metodos
        public void Validar(object obj)
        {
            try
            {
                var w = obj as System.Windows.Window;
                w.Cursor = Cursors.Wait;

                LstFile = oFtp.directoryListDetailed(string.Empty);

                w.Cursor = Cursors.Arrow;
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
                var w = obj as System.Windows.Window;
                w.Cursor = Cursors.Wait;

                oFtp.upload(new System.IO.FileInfo(pLocalFile).Name, pLocalFile);

                w.Cursor = Cursors.Arrow;
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
                OpenFileDialog pFd = new OpenFileDialog();
                if (pFd.ShowDialog() == true)
                    PLocalFile = pFd.FileName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Constructor
        public VMOssosi()
        {
        //    this.ftpU = ftpU;
        //    this.ftpP = ftpP;

            cmdValidar = new Command(Validar);
            cmdEnviar = new Command(Enviar);
            cmdSeleccionar = new Command(Seleccionar);

            pLocalFile = string.Empty;
            lstFile = new List<modFtpFileDet>();
        }
        #endregion
    }
}
