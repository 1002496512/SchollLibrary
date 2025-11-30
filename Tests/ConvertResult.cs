using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class ConvertResult
    {
       public bool success{get; set; }
       public List<string>  validationMessage { get; set; }
       public result result { get; set; }  
    }
}
