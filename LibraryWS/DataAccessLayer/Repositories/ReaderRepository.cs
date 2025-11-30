using LibraryModels;
using System.Data;
using System.IO.Pipelines;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;

namespace LibraryWS
{
    public class ReaderRepository : Repository, IRepository<Reader>
    {
        public ReaderRepository(OledbContext OledbContext,
                              FactoryModels FactoryModels) :
                              base(OledbContext, FactoryModels)
        {

        }
        public bool Create(Reader item)
        {
            string sql = $@"INSERT INTO Readers
                            (
                              ReaderId, 
                              ReaderFirstName, ReaderLastName,
                              ReaderAdress,ReaderTelephone,
                              ReaderImage, CityId,
                              ReaderNickName, ReaderPassword, ReaderSalt
                            )
                           VALUES
                           (
                               @ReaderId,  @ReaderFirstName,
                               @ReaderLastName, @ReaderAdress,@ReaderTelephone,
                               @ReaderImage, @CityId, 
                               @ReaderNickName, @ReaderPassword,@ReaderSalt
                           )";
            this.dbContext.AddParameter("@ReaderId", item.ReaderId);
            this.dbContext.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.dbContext.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.dbContext.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.dbContext.AddParameter("@CityId", item.CityId);
            this.dbContext.AddParameter("@ReaderTelephone", item.ReaderTelephone);
            this.dbContext.AddParameter("@ReaderImage", item.ReaderImage);
            this.dbContext.AddParameter("@ReaderNickName", item.ReaderNickName);
            this.dbContext.AddParameter("@ReaderPassword", item.ReaderPassword);
            string salt = GenerateSalt();
            this.dbContext.AddParameter("@ReaderSalt",salt );  


            return this.dbContext.Insert(sql) > 0;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
           RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public bool Delete(string id)
        {
            string sql = $@"delete from Readers where ReaderId=@ReaderId";
            this.dbContext.AddParameter("@ReaderId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<Reader> GetAll()
        {
            List<Reader> readers = new List<Reader>();
            string sql = "SELECT * FROM Readers";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    readers.Add(this.factoryModels.ReaderCreator.CreateModel(reader));
                }
            }
            return readers;
        }

        public Reader GetById(string id)
        {
            string sql = $"SELECT * FROM Readers where Readerid=@ReaderId";
            this.dbContext.AddParameter("@ReaderId", id);
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModels.ReaderCreator.CreateModel(reader);
            }
        }

        public bool Update(Reader item)
        {
            string sql = $@"INSERT set Readers
                              ReaderId=@ReaderId, 
                              ReaderFirstName=@ReaderFirstName,
                              ReaderLastName=@ReaderLastName,
                              ReaderAdress=@ReaderAdress,
                              ReaderTelephone=@ReaderTelephone,
                              ReaderImage=@ReaderImage,
                              CityId=@CityId,
                              ReaderNickName=@ReaderNickName,
                              ReaderPassword=@ReaderPassword";
            this.dbContext.AddParameter("@ReaderId", item.ReaderId);
            this.dbContext.AddParameter("@ReaderFirstName", item.ReaderFirstName);
            this.dbContext.AddParameter("@ReaderLastName", item.ReaderLastName);
            this.dbContext.AddParameter("@ReaderAdress", item.ReaderAdress);
            this.dbContext.AddParameter("@CityId", item.CityId);
            this.dbContext.AddParameter("@ReaderTelephone", item.ReaderTelephone);
            this.dbContext.AddParameter("@ReaderImage", item.ReaderImage);
            this.dbContext.AddParameter("@ReaderNickName", item.ReaderNickName);
            this.dbContext.AddParameter("@ReaderPassword", item.ReaderPassword);

            return this.dbContext.Insert(sql) > 0;
        }

        public string Login(string nickName, string password)
        {
            string sql = @"Select ReaderId from Readers 
                           where ReaderNickName=@ReaderNickName
                           and ReaderPassword=@ReaderPassword";
            this.dbContext.AddParameter("@ReaderNickName", nickName);
            this.dbContext.AddParameter("@ReaderPassword", password);
           using(IDataReader reader = this.dbContext.Select(sql))
            {
                if (reader.Read() == true)
                    return reader["ReaderId"].ToString();
                return null;
            }

        }
    }
}
