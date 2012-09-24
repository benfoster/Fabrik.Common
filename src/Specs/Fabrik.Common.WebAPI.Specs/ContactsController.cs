using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;

namespace Fabrik.Common.WebAPI.Specs
{
    public class ContactsController : ApiController
    {
        private static Collection<Contact> contacts = new Collection<Contact>
        {
            new Contact { Name = "Ben Foster", Number = "555-1234" },
            new Contact { Name = "John Smith", Number = "123-6787" },
            new Contact { Name = "Pete Jones", Number = "980-8736" }
        };

        public IEnumerable<Contact> Get()
        {
            return contacts;
        }
    }

    public class Contact
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
