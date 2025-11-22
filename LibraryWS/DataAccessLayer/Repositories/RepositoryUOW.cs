
namespace LibraryWS
{
    public class RepositoryFactory
    {
        AuthoRepository authoRepository;
        BookRepository bookRepository;
        BorrowRepository borrowRepository;
        CityRepository cityRepository;
        CountryRepository countryRepository;
        GanreRepository ganreRepository;
        ReaderRepository readerRepository;

        OledbContext helperOledb;
        FactoryModels FactoryModels;

        public RepositoryFactory()
        {
            this.helperOledb = new OledbContext();
            this.FactoryModels = new FactoryModels();
        }

        public OledbContext OledbContext
        {
            get
            {
                return this.helperOledb;
            }
        }

        public AuthoRepository AuthoRepository
        {
            get
            {
                if (this.authoRepository == null)
                    return new AuthoRepository(this.helperOledb, this.FactoryModels);
                return this.authoRepository;
            }
        }
        public BookRepository BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                    return new BookRepository(this.helperOledb, this.FactoryModels);
                return this.bookRepository;
            }
        }

        public CountryRepository CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    return new CountryRepository(this.helperOledb, this.FactoryModels);
                return this.countryRepository;
            }
        }

        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                    return new CityRepository(this.helperOledb, this.FactoryModels);
                return this.cityRepository;
            }
        }

        public BorrowRepository BorrowRepository
        {
            get
            {
                if (this.borrowRepository == null)
                    return new BorrowRepository(this.helperOledb, this.FactoryModels);
                return this.borrowRepository;
            }
        }
        public GanreRepository GanreRepository
        {
            get
            {
                if (this.ganreRepository == null)
                    return new GanreRepository(this.helperOledb, this.FactoryModels);
                return this.ganreRepository;
            }
        }

        public ReaderRepository ReaderRepository
        {
            get
            {
                if (this.readerRepository == null)
                    return new ReaderRepository(this.helperOledb, this.FactoryModels);
                return this.readerRepository;
            }
        }

        public void BeginTransaction()
        {
            this.OledbContext.OpenTransaction();
        }
        public void Commit()
        {
            this.OledbContext.Commit();
        }

        public void Rollback()
        {
            this.OledbContext.RollBack();
        }

        public string GetLastInsertedId()
        {
            
            return this.OledbContext.GetLastInsertedId();
        }
    }
}
