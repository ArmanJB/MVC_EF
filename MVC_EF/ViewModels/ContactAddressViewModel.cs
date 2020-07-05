using Microsoft.Ajax.Utilities;
using MVC_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_EF.ViewModels
{
    public class ContactAddressViewModel
    {
        public int ContactID { get; set; }
        public int AreaID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }


        public string AddressDescription { get; set; }
        public List<AddressViewModel> Addresses { get; set; }

        public ContactAddressViewModel()
        {
            Addresses = new List<AddressViewModel>();
            BirthDate = DateTime.Now;
            Refresh();
        }

        public void Refresh()
        {
            AddressDescription = string.Empty;
        }

        public bool IsValidContact()
        {
            return !(AreaID == 0 || FirstName.IsNullOrWhiteSpace() || LastName.IsNullOrWhiteSpace() || BirthDate == null);
        }

        public bool ValidAddressAdded()
        {
            return !AddressDescription.IsNullOrWhiteSpace();
        }

        public void RemoveItemAddress()
        {
            if (Addresses.Count > 0)
            {
                var address = Addresses.Where(x => x.Remove).SingleOrDefault();
                Addresses.Remove(address);
            }
        }

        public void AddItemAddress()
        {
            Addresses.Add(new AddressViewModel
            {
                Description = AddressDescription
            });

            Refresh();
        }

        public Contact ToModel()
        {
            Contact address = new Contact();
            address.AreaID = this.AreaID;
            address.FirstName = this.FirstName;
            address.LastName = this.LastName;
            address.BirthDate = this.BirthDate;
            address.Addresses = new List<Address>();

            Addresses.ForEach(x =>
            {
                address.Addresses.Add(new Address
                {
                    Description = x.Description
                });
            });

            return address;
        }
    }
}