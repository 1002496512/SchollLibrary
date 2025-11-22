using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        RepositoryFactory repositoryFactory;
        public AdminController()
        {
            this.repositoryFactory = new RepositoryFactory();
        }
        [HttpPost]
        public bool AddNewBook(NewBookViewModel newBookViewModel)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                this.repositoryFactory.BeginTransaction();
                bool ok = this.repositoryFactory.BookRepository.Create(newBookViewModel.Book);
                string bookId = this.repositoryFactory.GetLastInsertedId().ToString();
                foreach (string authorid in newBookViewModel.Authors)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AddBookAuthor(bookId, authorid);
                }
                foreach (string ganreId in newBookViewModel.Genres)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AdBookGanre(bookId, ganreId);
                }
                this.repositoryFactory.Commit();
                return true;
            }
            catch (Exception ex)
            {
                this.repositoryFactory.RollBack();
                return false;
            }
            finally
            {
                this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpPost]
        public bool UpdateBook(UpdateBookViewModel  updateBookViewModel)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                this.repositoryFactory.BeginTransaction();
                bool ok = this.repositoryFactory.BookRepository.Update(updateBookViewModel.Book);
                foreach(string authorid in updateBookViewModel.AuthorsToDelete)
                {
                    ok = ok && this.repositoryFactory.BookRepository.DeleteBookAuthor(updateBookViewModel.Book.BookId, authorid);
                }
                foreach (string ganreId in updateBookViewModel.GanresToDelete)
                {
                    ok = ok && this.repositoryFactory.BookRepository.DeleteBookGanre(updateBookViewModel.Book.BookId, ganreId);
                }   
                foreach (string authorid in updateBookViewModel.AuthorsToAdd)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AddBookAuthor(updateBookViewModel.Book.BookId, authorid);
                }
                foreach (string ganreId in updateBookViewModel.GanresToAdd)
                {
                    ok = ok && this.repositoryFactory.BookRepository.AdBookGanre(updateBookViewModel.Book.BookId, ganreId);
                }
                this.repositoryFactory.Commit();
                return true;
            }
            catch (Exception ex)
            {
                this.repositoryFactory.RollBack();
                return false;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }
        }

        [HttpGet]
        public bool DeleteBook(string bookId)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                this.repositoryFactory.BeginTransaction();
                bool ok = this.repositoryFactory.BookRepository.GeleteBookAuthors(bookId);
                ok = ok && this.repositoryFactory.BookRepository.DeleteBookGanres(bookId);
                ok = ok && this.repositoryFactory.BookRepository.Delete(bookId);
                this.repositoryFactory.Commit();
                return ok;
            }
            catch (Exception ex)
            {
                this.repositoryFactory.RollBack();
                return false;
            }
            finally
            {
                 this.repositoryFactory.DisconnectDb();
            }
        }

    }
}
