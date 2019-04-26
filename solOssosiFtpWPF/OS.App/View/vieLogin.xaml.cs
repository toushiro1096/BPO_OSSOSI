using OS.App.ViewModel;
using System.Windows;

namespace OS.App.View
{
    /// <summary>w
    /// Lógica de interacción para vieLogin.xaml
    /// </summary>
    public partial class vieLogin : Window
    {
        public vieLogin()
        {
            InitializeComponent();
            VMLogin vm = new VMLogin();
            //vm.Usr = "admin";
            //txtPwd.Password = "m1s3r4bl3s";
            this.DataContext = vm;

            this.txtUsr.TextChanged += (s, a) => 
                txtPwd.PasswordChar = 
                    Equals(txtUsr.Text.ToLower(), "abimael") ? '☭' : 
                    Equals(txtUsr.Text.ToLower(), "ella") ? '☠' : 
                    Equals(txtUsr.Text.ToLower(), "manuela") ? '✍' : '*';
            
        }
    }
}
