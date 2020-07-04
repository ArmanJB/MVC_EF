using System.Collections.Generic;

namespace MVC_EF.Models
{
    public class Area
    {
        public int AreaID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}