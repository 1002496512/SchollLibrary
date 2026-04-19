using LibraryModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WebApiClient;

namespace LibrarianApp.Frames
{
    /// <summary>
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        string imgPath;
        NewBookViewModel newBookViewModel;  
        public NewBook()
        {
            InitializeComponent();
            GetNewBookViewModel();
        }
        private async Task GetNewBookViewModel()
        {
            WebClient<NewBookViewModel> apiClient = new WebClient<NewBookViewModel>();
            apiClient.Scheme = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5185;
            apiClient.Path = "api/Admin/GetnewBookViewModel";
            newBookViewModel = await apiClient.GetAsync();
            newBookViewModel.Book = new Book(); 
            newBookViewModel.Book.BookName ="aaaa";
            newBookViewModel.Book.BookDescription = "bbbb";
            if (newBookViewModel != null)
            {
                this.DataContext = newBookViewModel;
                listBoxGenres.ItemsSource = newBookViewModel.Genres;
                listBoxAuthors.ItemsSource = newBookViewModel.Authors;
               
            }
        }
        private void buttonSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter= "Only Imges (*.jpg;*png;*.gif)|*.jpg;*png;*gif";   
            bool? ok = ofd.ShowDialog();
            if (ok == true)
            {
                Uri uri = new Uri(ofd.FileName);
                this.imageBook.Source = new BitmapImage(uri);
                this.imgPath = ofd.FileName;
            }
        }

        private async void buttonAddBook_Click(object sender, RoutedEventArgs e)
        {
            NewBookViewModel newBookViewModel = new NewBookViewModel();
            newBookViewModel.Book = new Book();
            newBookViewModel.Book.BookName = textBoxBookName.Text;
            newBookViewModel.Book.BookDescription = textBoxBookDescription.Text;
            newBookViewModel.Book.BookImage = System.IO.Path.GetExtension(this.imgPath);
            newBookViewModel.Authors = this.listBoxAuthors.SelectedItems.Cast<Author>().ToList();
            newBookViewModel.Genres = this.listBoxGenres.SelectedItems.Cast<Ganre>().ToList(); ;
           Stream stream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            WebClient<NewBookViewModel> apiClient = new WebClient<NewBookViewModel>();
            newBookViewModel.Book.Validate();   
            bool isValid = newBookViewModel.Book.IsValid;
            bool ok= false;
            if (isValid == true)
            {
                apiClient.Scheme = "http";

                apiClient.Host = "localhost";
                apiClient.Port = 5185;
                apiClient.Path = "api/Admin/AddNewBook";
                ok = await apiClient.PostAsync(newBookViewModel, stream);
            }
            if (ok == true)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
                MessageBox.Show("Adding new book was fail. Try later!",
                                "Fail adding new book",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

        }
    }
}
