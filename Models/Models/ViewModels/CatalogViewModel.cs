
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryModels
{
    public class CatalogViewModel
    {
        public List<Book> Books { get; set; }
        public List<Ganre> TypesBook { get; set; }
    }
}