namespace LibraryWS
{
    public class FactoryModels
    {
        AuthorCreator authorCreator;
        BookCreator bookCreator;
        ReaderCreator readerCreator;
        BorrowCreator borrowCreator;
        CountryCreator countryCreator;
        GanreCreator ganreCreator;
        CityCreator cityCreator;    

        public AuthorCreator AuthorCreator
        {
            get
            {
                if (this.authorCreator == null)
                {
                    this.authorCreator = new AuthorCreator();
                }
                return authorCreator;
            }
        }

        public BookCreator BookCreator
        {
            get
            {
                if (this.bookCreator == null)
                {
                    this.bookCreator = new BookCreator();
                }
                return bookCreator;
            }
        }


        public ReaderCreator ReaderCreator
        {
            get
            {
                if (this.readerCreator == null)
                {
                    this.readerCreator = new ReaderCreator();
                }
                return readerCreator;
            }
        }

        public BorrowCreator BorrowCreator
        {
            get
            {
                if (this.borrowCreator == null)
                {
                    this.borrowCreator = new BorrowCreator();
                }
                return borrowCreator;
            }
        }
        public CountryCreator CountryCreator
        {
            get
            {
                if (this.countryCreator == null)
                {
                    this.countryCreator = new CountryCreator();
                }
                return countryCreator;
            }
        }

        public GanreCreator GanreCreator
        {
            get
            {
                if (this.ganreCreator == null)
                {
                    this.ganreCreator = new GanreCreator();
                }
                return ganreCreator;
            }
        }
        public CityCreator CityCreator
        {
            get
            {
                if (this.cityCreator == null)
                {
                    this.cityCreator = new CityCreator();
                }
                return cityCreator;
            }
        }


    }
}
