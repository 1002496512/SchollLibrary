using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibraryModels;
using static System.Reflection.Metadata.BlobBuilder;

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
        public CatalogViewModel GetBookCatalog(string authorId = null, string ganreId = null, int page = 0)
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
                if (authorId == null && ganreId == null && page == 0)
                {
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetAll();
                }
                else if (authorId != null && ganreId == null && page == 0)
                {
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);
                }
                else if (authorId == null && ganreId != null && page == 0)
                {
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);
                }
                else if (authorId == null && ganreId == null && page != 0)
                {
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksByPage(page);
                }
                else if (authorId != null && ganreId == null && page != 0)
                {
                    int booksperPage = 10;
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyAuthor(authorId);
                    catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();

                }
                else if (authorId == null && ganreId != null && page != 0)
                {
                    int booksperPage = 10;
                    catalogViewModel.Books = this.repositoryFactory.BookRepository.GetBooksbyGanre(ganreId);
                    catalogViewModel.Books.Skip(catalogViewModel.PagePerPage * (page - 1)).Take(catalogViewModel.PagePerPage).ToList();
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

        [HttpGet]
        public Book GetBook(string bookId)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                return this.repositoryFactory.BookRepository.GetById(bookId);
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
        public bool RegisterReader([FromBody] Reader reader)
        {
            try
            {
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

    }
}
