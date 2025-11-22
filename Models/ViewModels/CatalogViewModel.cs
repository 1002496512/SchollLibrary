
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryModels
{
    public class CatalogViewModel
    {
        public List<Book> Books { get; set; }
        public List<Ganre> Ganres { get; set; }
        public List<Author> Authors { get; set; }

        public string AuthorId { get; set; }
        public string GanreId { get; set; }
        public string Page { get; set; }
        public int PagePerPage { get; set; }

    }
}