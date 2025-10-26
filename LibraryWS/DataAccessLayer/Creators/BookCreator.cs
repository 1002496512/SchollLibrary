using LibraryModels;
using System.Data;
namespace LibraryWS
{
    public class BookCreator : IModelCreator<Book>
    {
        public Book CreateModel(IDataReader reader)
        {
            return new Book()
            {
                BookDescription = Convert.ToString(reader["BookDescription"]),
                BookId = Convert.ToString(reader["BokId"]),
                BookImage = Convert.ToString(reader["BookImage"]),
                BookName = Convert.ToString(reader["BookImage"])
            };
        }
    }
}
