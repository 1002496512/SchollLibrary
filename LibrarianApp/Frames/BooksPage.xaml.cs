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
using LibraryModels;
using WebApiClient;

namespace LibrarianApp.Frames
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : UserControl
    {
        List<Book> books;
        public BooksPage()
        {
            InitializeComponent();
            GetBooks();
        }

        private async Task GetBooks()
        {
            WebClient<List<Book>> client = new WebClient<List<Book>>();
            client.Scheme = "http"; 
            client.Host = "localhost";
            client.Port = 5185;
            client.Path = "api/Admin/GetBooks";
            this.books = await client.GetAsync(); 
            this.listViewBooks.ItemsSource = this.books;    
            //this.DataContext = this.books;
        }
    }


}
