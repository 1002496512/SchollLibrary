using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibrarianApp.Frames;  

namespace LibrarianApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartPage startPage;
        LoginPage loginPage;
        BooksPage booksPage;    
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
        }

        private void ViewStartPage()
        {
            if(this.startPage == null)   
                this.startPage = new StartPage();
            this.frameContent.Content = this.startPage;


        }
        private void ViewLoginPage()
        {
            if (this.loginPage == null)
                this.loginPage = new LoginPage();
            this.frameContent.Content = this.loginPage;


        }

         private void ViewBooksPage()
        {
            if (this.booksPage == null)
                this.booksPage = new BooksPage();
            this.frameContent.Content = this.booksPage;


        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


      

      
        private void StartPage_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }

        private void BookPage_Click(object sender, RoutedEventArgs e)
        {
            ViewBooksPage();
        }
    }
}