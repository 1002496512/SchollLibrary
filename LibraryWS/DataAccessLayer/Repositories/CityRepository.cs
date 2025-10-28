using LibraryModels;
using System.Data;

namespace LibraryWS.DataAccessLayer.Repositories
{
    public class CityRepository : Repository, IRepository<City>
    {
        public CityRepository(OledbContext dbContext, FactoryModels factoryModels) : base(dbContext, factoryModels)
        {
        }

        public bool Create(City item)
        {
            string sql = $@"INSERT INTO Cities
                            (CityId) VALUES (@CityName)";
            this.dbContext.AddParameter("@CityName", item.CityName);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete from Cities where CitiId=@CityId";
            this.dbContext.AddParameter("@CityId", id);
            return this.dbContext.Insert(sql) > 0;
        }

        public List<City> GetAll()
        {
            List<City> cities = new List<City>();
            string sql = "SELECT * FROM Cities";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    cities.Add(this.factoryModels.CityCreator.CreateModel(reader));
                }
            }
            return cities;
        }

        public City GetById(string id)
        {
            string sql = "SELECT * FROM Cities where CityId=@Cityid";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModels.CityCreator.CreateModel(reader);
            }
        }

        public bool Update(City item)
        {
            string sql = $@"Update Cities set CityName=@CityName)";
            this.dbContext.AddParameter("@CityName", item.CityName);
            return this.dbContext.Insert(sql) > 0;
        }
    }
}
