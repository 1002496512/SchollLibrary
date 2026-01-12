using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryModels;
using static System.Reflection.Metadata.BlobBuilder;
using LibraryModels;
using System.Text.Json;

namespace LibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        RepositoryFactory repositoryFactory;

        public GuestController()
        {
            this.repositoryFactory = new RepositoryFactory();
        }

        [HttpGet]
        public CatalogViewModel GetBookCatalog(string authorId = null,
            string ganreId = null, int page = 1)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            catalogViewModel.GanreId = ganreId;
            catalogViewModel.AuthorId = authorId;
            catalogViewModel.Page = page.ToString();
            catalogViewModel.PagePerPage = 10;
            try
            {
                this.repositoryFactory.ConnectDb();
                catalogViewModel.Ganres = this.repositoryFactory.GanreRepository.GetAll();
                catalogViewModel.Authors = this.repositoryFactory.AuthorRepository.GetAll();
                
                //if (authorId != null && ganreId == null && page == 0)
                //{
                //    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);
                //    catalogViewModel.PageCount = BookCount(catalogViewModel.Books.Count);
                //}
                //else if (authorId == null && ganreId != null && page == 0)
                //{
                //    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);
                //    catalogViewModel.PageCount = BookCount(catalogViewModel.Books.Count);
                //}
                if (authorId == null && ganreId == null && page != 0)
                {
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksByPage(page);
                    catalogViewModel.PageCount = BookCount(this.repositoryFactory.BookRepository.AllBookCount());
                   // catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();
                }
                else if (authorId != null && ganreId == null && page != 0)
                {
                    
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);
                    catalogViewModel.PageCount = BookCount(catalogViewModel.Books.Count); 
                    catalogViewModel.Books = catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();

                }
                else if (authorId == null && ganreId != null && page != 0)
                {
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);
                    catalogViewModel.PageCount =BookCount(catalogViewModel.Books.Count);
                    catalogViewModel.Books= catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();
                }
                return catalogViewModel;
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }

        }
        private int BookCount(int allBooks)
        {
            int pageCount = allBooks / 10;
            if (allBooks % 10 > 0)
            {
                pageCount += 1;
            }
            return pageCount;
        }
        [HttpGet]
        public BookViewModel ViewBook(string bookId)
        {
            BookViewModel bookViewModel = new BookViewModel();
            try
            {
                this.repositoryFactory.ConnectDb();
                bookViewModel.Book = this.repositoryFactory.BookRepository.GetById(bookId);
                bookViewModel.Authors = this.repositoryFactory.AuthorRepository.GetAuthorsByBook(bookId);
                bookViewModel.Ganres = this.repositoryFactory.GanreRepository.GetGanresByBook(bookId);
                return bookViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpPost]   
        public bool RegisterReader([FromForm] string data, IFormFile file)
        {
            try
            {
                Reader reader = JsonSerializer.Deserialize<Reader>(data);
                this.repositoryFactory.ConnectDb();
                return this.repositoryFactory.ReaderRepository.Create(reader);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public RegistrationViewModel GetRegistrationViewModel()
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            try
            {
                this.repositoryFactory.ConnectDb();
                registrationViewModel.Cities = this.repositoryFactory.CityRepository.GetAll();
                registrationViewModel.Reader = null;
                return registrationViewModel;
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }

            finally
            {
                this.repositoryFactory.DisconnectDb() ; 
            }

        }

    }
}
