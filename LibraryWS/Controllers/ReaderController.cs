using LibraryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryWS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        RepositoryFactory repositoryFactory;
        public ReaderController()
        {
            this.repositoryFactory = new RepositoryFactory();
        }
        [HttpGet]
        public string Login(string nickName, string password)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                return this.repositoryFactory.ReaderRepository.Login(nickName, password);
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

        [HttpGet]
        public Reader GetReaderById(string readerId)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                return this.repositoryFactory.ReaderRepository.GetById(readerId);
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

        [HttpGet]
        public List<ReaderBorrow> GetReaderBorrows(string readerId)
        {
            List<ReaderBorrow> readerBorrows = new List<ReaderBorrow>();
            try
            {
                this.repositoryFactory.ConnectDb();
                List<Borrow> borrows = this.repositoryFactory.BorrowRepository.GetReaderBorrows(readerId);
                foreach (var borrow in borrows)
                {
                    Book book = this.repositoryFactory.BookRepository.GetById(borrow.BookId);
                    ReaderBorrow readerBorrow = new ReaderBorrow()
                    {
                        Borrow = borrow,
                        Book = book
                    };
                    readerBorrows.Add(readerBorrow);
                }
                return readerBorrows;
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


        [HttpGet]
        public bool AddBorrow(string readerId, string bookId)
        {
            try
            {
                this.repositoryFactory.ConnectDb();
                Borrow borrow = new Borrow()
                {
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    BorrowStatus = "1"
                };
                return this.repositoryFactory.BorrowRepository.Create(borrow);
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
