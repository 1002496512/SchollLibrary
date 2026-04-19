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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibrarianApp.Frames
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public bool IsLogin { get; set; } = false;  
        public LoginPage()
        {
            InitializeComponent();
        }

        private void buttonLoin_Click(object sender, RoutedEventArgs e)
        {
            this.IsLogin = true;
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.StateHyperLinks(this.IsLogin);
        }
    }
}
