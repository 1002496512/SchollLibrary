using LibraryModels;
using System.Data;

namespace LibraryWS
{
    public class BookRepository : Repository, IRepository<Book>
    {
        public BookRepository(OledbContext dbContext, FactoryModels factoryModels) : base(dbContext, factoryModels)
        {

        }

        public bool Create(Book item)
        {
            string sql = $@"INSERT INTO Books
                            (
                              BookName, 
                              BookDescription, 
                              BookImage,BookCopies
                            )
                           VALUES
                           (
                               @BookName,  @BookDescription,
                               @BookImage, @BookCopies
                           )";
            this.dbContext.AddParameter("@BookName", item.BookName);
            this.dbContext.AddParameter("@BookDescription", item.BookDescription);
            this.dbContext.AddParameter("@BookImage", item.BookImage);
            this.dbContext.AddParameter("@BookCopies", item.BookCopies);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Books where BookId=@BookId";
            this.dbContext.AddParameter("@BookId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Book> GetAll()
        {
           
            string sql = "SELECT * FROM Books";
            return GetBookList(sql);
        }

        private List<Book> GetBookList(string sql)
        {
            List<Book> books = new List<Book>();
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    books.Add(this.factoryModels.BookCreator.CreateModel(reader));
                }
            }
            return books;
        }


        public List<Book> GetBooksbyGanre(string ganreId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksGenres.GenreId
                          FROM Books INNER JOIN BooksGenres ON Books.BookId = BooksGenres.BookId
                          WHERE BooksGenres.GenreId=@GanreId;";
            this.dbContext.AddParameter("@GanreId", ganreId);
            return GetBookList(sql);
        }
        public List<Book> GetBooksbyAuthor(string AuthorId)
        {
            string sql = @"SELECT Books.BookId, Books.BookName, Books.BookDescription, Books.BookImage, Books.BookCopies, BooksAuthors.AuthorId
                           FROM Books INNER JOIN BooksAuthors ON Books.BookId = BooksAuthors.BookId
                           WHERE (((BooksAuthors.AuthorId)=@AuthorId))";
            this.dbContext.AddParameter("@AuthorId", AuthorId);
            return GetBookList(sql);
        }


        public Book GetById(string id)
        {
            string sql = $"SELECT * FROM Books where Bookid=@Bookid";
            this.dbContext.AddParameter("@Bookid", id);

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModels.BookCreator.CreateModel(reader);
            }
            return null;
        }

        public bool Update(Book item)
        {
            string sql = $@"Update set Books
                              BookName=@BookName, 
                              BookDescription=@BookName, 
                              BookImage=@BookName,
                              BookCopies=@BookName
                              where BookId=@BookId";
            this.dbContext.AddParameter("@BookName", item.BookName);
            this.dbContext.AddParameter("@BookDescription", item.BookDescription);
            this.dbContext.AddParameter("@BookImage", item.BookImage);
            this.dbContext.AddParameter("@BookCopies", item.BookCopies);
            this.dbContext.AddParameter("@BookId", item.BookId);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Book> GetBooksbyPage(string page)
        {
            int booksPerPage = 10;
            string sql = $@"SELECT TOP {booksPerPage}  *  
                            FROM Books WHERE BookId NOT IN
                            (
                                SELECT TOP {booksPerPage * int.Parse(page) - 1} BookId 
                                FROM Books
                                ORDER BY BookId ASC
                            )
                            ORDER BY BookId ASC";
            return GetBookList(sql);
        }
    }
}
