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
        public CatalogViewModel GetCatalog()
        {
           
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            //open connection
            try
            {
                this.repositoryUOW.ConnectDb();
                catalogViewModel.Books = this.repositoryUOW.BookRepository.GetAll();
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
        