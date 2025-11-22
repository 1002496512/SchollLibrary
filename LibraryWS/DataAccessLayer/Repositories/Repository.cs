namespace LibraryWS
{
    public class Repository
    {
       protected OledbContext dbContext;
       protected FactoryModels factoryModels;

        public Repository(OledbContext dbContext,
                          FactoryModels FactoryModels)
        {
            this.dbContext =  dbContext;
            this.factoryModels = factoryModels;
        }

        

    }
}
