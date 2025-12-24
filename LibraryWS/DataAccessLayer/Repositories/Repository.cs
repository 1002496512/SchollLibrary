namespace LibraryWS
{
    public class Repository
    {
       protected OledbContext dbContext;
       protected FactoryModels factoryModels;

        public Repository(OledbContext dbContext,
                          FactoryModels factoryModels)
        {
            this.dbContext =  dbContext;
            this.factoryModels = factoryModels;
        }

        

    }
}
