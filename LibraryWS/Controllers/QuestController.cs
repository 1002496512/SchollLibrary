using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestController : ControllerBase
    {

        RepositoryUOW repositoryUOW;
        public QuestController()
        {
            this.repositoryUOW = new RepositoryUOW();

        }


        [HttpGet]
        public CatalogViewModel GetCatalog(string authorId=null, string ganreId=null, string page=null)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            //open connection
            try
            {
                this.repositoryUOW.ConnectDb();
               
                if(authorId == null && ganreId == null && page == null)
                   catalogViewModel.Authors = this.repositoryUOW.AuthorRepository.GetAll();
                else if(authorId != null && ganreId == null && page == null)
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyAuthor(authorId);
                else if (authorId == null && ganreId != null && page == null)
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyGanre(ganreId);
                else if (authorId == null && ganreId == null && page != null)
                    catalogViewModel.Books = this.repositoryUOW.BookRepository.GetBooksbyPage(page);


                catalogViewModel.Authors = this.repositoryUOW.AuthorRepository.GetAll();    
                catalogViewModel.Ganres = this.repositoryUOW.GanreRepository.GetAll();
                return catalogViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Console.WriteLine(error);
                return null;
            }
            finally
            {
                this.repositoryUOW.DisconnectDb();
            }


            //close connection
           
        }
    }
}
        