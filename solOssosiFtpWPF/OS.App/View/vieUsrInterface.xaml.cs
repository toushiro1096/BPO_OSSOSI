using OS.App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OS.App.View
{
    /// <summary>
    /// Lógica de interacción para vieUsrInterface.xaml
    /// </summary>
    public partial class vieUsrInterface : Window
    {
        public vieUsrInterface(string ftpU, string ftpP, bool isSiglo = false)
        {
            InitializeComponent();
            DataContext = new VMInterface(ftpU, ftpP, isSiglo);
        }
    }
}
