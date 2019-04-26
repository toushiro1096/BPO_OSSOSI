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
    /// Lógica de interacción para vieUsrOssosi.xaml
    /// </summary>
    public partial class vieUsrOssosi : Window
    {
        public vieUsrOssosi()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.VMOssosi();
        }
        
    }
}
