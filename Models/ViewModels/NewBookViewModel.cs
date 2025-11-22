using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  LibraryModels

{
    public class NewBookViewModel
    {
        public Book Book { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Genres { get; set; }


    }
}
