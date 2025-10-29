using LibraryModels;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWS
{
    public class ReaderCreator : IModelCreator<Reader>
    {
        public Reader CreateModel(IDataReader reader)
        {
            return new Reader
            {
                CityId = Convert.ToString(reader["AuthorFirstName"]),
            };
        }
    }
}
