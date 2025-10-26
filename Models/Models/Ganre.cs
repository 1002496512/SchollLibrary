using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ganre
    {
        string ganreId;
        string ganreName;

        public Ganre(string ganreId, string ganreName)
        {
            this.ganreId = ganreId;
            this.ganreName = ganreName;
        }
        public string GanreId
        {
            get { return ganreId; }
            set
            {
                ganreId = value;
            }
        }

        [Required(ErrorMessage = "Ganre Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ganre Name cannot be less than 3 characters.")]
        public string GanreName
        {
            get { return ganreName; }
            set
            {
                ganreName = value;
            }
        }   
    }
}
