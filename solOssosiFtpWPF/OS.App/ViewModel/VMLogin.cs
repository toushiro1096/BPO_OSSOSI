using Microsoft.Win32;
using OS.App.Model;
using OS.App.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace OS.App.ViewModel
{
    public class VMLogin : NotifyPropertyChanged
    {
        string usr;

        public string Usr
        {
            get { return usr; }
            set { SetProperty(() => usr = value, () => usr == value); }
        }

        Command cmdOk = null;
        Command cmdCancel = null;

        public ICommand CmdOk => cmdOk;
        public ICommand CmdCancel => cmdCancel;

        public void Ok(object obj)
        {
            try
            {
                var w = obj as System.Windows.Window;
                w.Cursor = Cursors.Wait;

                bool log = true;

                var _pwdBx = w.FindName("txtPwd") as PasswordBox;
                string _pwd = _pwdBx.Password;

                if (Usr == "admin" && _pwd == "m1s3r4bl3s")
                {
                    var vieI = new vieUsrInterface("usrInterfaz", "1s3aX5jf", true);
                    vieI.Show();

                    //var vieO = new vieUsrInterface("usrOssosi", "mcdSssZp","jj");
                    //vieO.Show();
                }
                else if (Usr == "Ossosi" && _pwd == "$abc1234")
                {
                    var vieO = new vieUsrInterface("usrOssosi", "mcdSssZp");
                    vieO.Show();
                }
                else if (Usr == "Siglo" && _pwd == "$$$abc1234")
                {
                    var vieI = new vieUsrInterface("usrInterfaz", "1s3aX5jf", true);
                    vieI.Show();
                }
                else log = false;

                if (log) w.Close();

                w.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cancel(object obj)
        {
            try
            {
                var w = obj as System.Windows.Window;
                w.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VMLogin()
        {
            cmdOk = new Command(Ok);
            cmdCancel = new Command(Cancel);
        }
    }
}
