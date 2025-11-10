namespace LibraryWS
{
    public class RepositoryUOW
    {
        AuthorRepository authorRepository;
        BookRepository bookRepository;
        BorrowRepository borrowRepository;
        CityRepository cityRepository;
        CountryRepository countryRepository;
        GanreRepository ganreRepository;
        ReaderRepository readerRepository;
        OledbContext oledbContext;
        FactoryModels factoryModels;

        public RepositoryUOW()
        {
            this.oledbContext = new OledbContext(); ;
            this.factoryModels = new FactoryModels(); ;
        }

        public AuthorRepository AuthorRepository
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new AuthorRepository(oledbContext, factoryModels);
                }
                return authorRepository;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new BookRepository(oledbContext, factoryModels);
                }
                return bookRepository;
            }
        }
        public BorrowRepository BorrowRepository
        {
            get
            {
                if (borrowRepository == null)
                {
                    borrowRepository = new BorrowRepository(oledbContext, factoryModels);
                }
                return borrowRepository;
            }
        }
        public CityRepository CityRepository
        {
            get {

                if (cityRepository == null)
                {
                    cityRepository = new CityRepository(oledbContext, factoryModels);
                }
           
            return cityRepository;
            }
        }
        public CountryRepository CountryRepository
        {
            get
            {
                if (countryRepository == null)
                {
                    countryRepository = new CountryRepository(oledbContext, factoryModels);
                }
                return countryRepository;
            }
        }
        public GanreRepository GanreRepository
        {
            get
            {
                if (ganreRepository == null)
                {
                    ganreRepository = new GanreRepository(oledbContext, factoryModels);
                }
                return ganreRepository;
            }
        }
        public ReaderRepository ReaderRepository
        {
            get
            {
                if (readerRepository == null)
                {
                    readerRepository = new ReaderRepository(oledbContext, factoryModels);
                }
                return readerRepository;
            }
        }

        public void ConnectDb()
        {
            this.oledbContext.OpenConnection();
        }   
        public void DisconnectDb()
        {
            this.oledbContext.CloseConnection();
        }

    }
}
