using LibraryModels;
using System.Data;

namespace LibraryWS
{
    public class AuthorRepository : Repository, IRepository<Author>
    {
        public AuthorRepository(OledbContext dbContext, FactoryModels factoryModels) :
            base(dbContext, factoryModels)
        {
        }

        public bool Create(Author item)
        {
            //string sql = $@"INSERT INTO Authors
            //             (AuthorFirstName, AuthorLastName,
            //              AuthorLastName,AuthorYear,
            //              CountryId,AuthorPicture)
            //              VALUES ('{item.AuthorFirstName}', 
            //             '{item.AuthorLastName}',
            //              {item.AuthorYear},
            //              {item.CountryId},
            //             '{item.AuthorPicture}')";    
            string sql = $@"INSERT INTO Authors
                            (
                              AuthorFirstName, 
                              AuthorLastName, AuthorYear,
                              CountryId,AuthorPicture
                            )
                           VALUES
                           (
                               @AuthorFirstName,  @AuthorLastName,
                               @AuthorYear, @CountryId,@AuthorPicture
                           )";
            this.dbContext.AddParameter("@AuthorFirstName", item.AuthorFirstName);
            this.dbContext.AddParameter("@AuthorLastName", item.AuthorLastName);
            this.dbContext.AddParameter("@AuthorYear", item.AuthorYear);
            this.dbContext.AddParameter("@CountryId", item.CountryId);
            this.dbContext.AddParameter("@AuthorPicture", item.AuthorPicture);


            return this.dbContext.Insert(sql) > 0;  
        }

        public bool Delete(string id)
        {
            string sql = $@"DELETE FROM Authors
                           WHERE AuthorId =@authorId";
            this.dbContext.AddParameter("@authorId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<Author> GetAll()
        {
            List<Author> authors = new List<Author>();  
            string sql = "SELECT * FROM Authors";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while(reader.Read())
                {
                   
                    authors.Add(this.factoryModels.AuthorCreator.CreateModel(reader));
                }
               
            }
            return authors;
        }

        public Author GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Author item)
        {
            throw new NotImplementedException();
        }
    }
}
