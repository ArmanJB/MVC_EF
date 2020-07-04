using System;
using System.Collections.Generic;

namespace MVC_EF.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        public int AreaID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual Area Area { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}