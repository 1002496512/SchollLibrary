using LibraryModels;
using System.Threading.Channels;
namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ModelValidation();
        }

        static void ModelValidation()
        {
            Author author = new Author();   
            author.AuthorFirstName = "john";
            author.AuthorLastName = "dw";
            author.AuthorYear = 12;
            author.CountryId = 1;
            author.AuthorPicture = "picture.pdf";

            Dictionary<string, List<string>> errors = author.AllErrors();   
            if(author.IsValid == false)
            {
                foreach(var error in errors)
                {
                    foreach(var errorMsg in error.Value)
                    {
                        Console.WriteLine($"{errorMsg}");
                    }
                }
            }

        }
    }

    
}
