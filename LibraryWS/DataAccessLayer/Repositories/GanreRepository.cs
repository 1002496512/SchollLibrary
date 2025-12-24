using LibraryModels;
using System.Data;

namespace LibraryWS
{
    public class GanreRepository : Repository, IRepository<Ganre>
    {
        public GanreRepository(OledbContext OledbContext,
                             FactoryModels FactoryModels) :
                             base(OledbContext, FactoryModels)
        {

        }
        public bool Create(Ganre item)
        {
            string sql = $@"INSERT INTO Ganres
                            (GanreId) VALUES (@GanreName)";
            this.dbContext.AddParameter("@GanreName", item.GanreName);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Ganres where GanreId=@GanreId";
            this.dbContext.AddParameter("@GanreId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Ganre> GetAll()
        {
            List<Ganre> ganres = new List<Ganre>();
            string sql = "SELECT * FROM Genres";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    ganres.Add(this.factoryModels.GanreCreator.CreateModel(reader));
                }
            }
            return ganres;
        }

        public Ganre GetById(string id)
        {
            string sql = "SELECT * FROM Ganres where GanreId=@GanreId";
            this.dbContext.AddParameter("@GanreId", id);

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModels.GanreCreator.CreateModel(reader);
            }
        }

        public bool Update(Ganre item)
        {
            string sql = $@"Update Ganres set GanreName=@GanreName)";
            this.dbContext.AddParameter("@GanreName", item.GanreName);
            return this.dbContext.Insert(sql) > 0;
        }

        internal List<Ganre> GetGanresByBook(string bookId)
        {
            List<Ganre> ganres = new List<Ganre>();
            string sql = @"SELECT Genres.GenreName, Genres.GenreId, BooksGenres.BookId
                           FROM Genres INNER JOIN BooksGenres ON Genres.GenreId = BooksGenres.GenreId
                           WHERE BooksGenres.BookId=@BookId";
            this.dbContext.AddParameter("@BookId", bookId);

            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    ganres.Add(this.factoryModels.GanreCreator.CreateModel(reader));
                }
            }
            return ganres;
        }
    }
}
