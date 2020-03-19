using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactDLL.Services
{
    class PersonneSeeder
    {
        public Faker faker { get; set; }
        
        public PersonneSeeder()
        {
            faker = new Faker("fr");
        }
        public List<Personne> seed()
        {
            ContactService cs = new ContactService();
            List<Personne> listPersonnes = new List<Personne>();
            for (int i = 0; i < 25; i++)
            {
                Ami ami = new Ami(faker.Name.LastName(), faker.Name.FirstName(), faker.Internet.Email(), faker.Address.FullAddress(), faker.Phone.PhoneNumberFormat(), faker.Date.Past(), faker.Phone.PhoneNumberFormat());
                Client client = new Client(faker.Name.LastName(), faker.Name.FirstName(), faker.Internet.Email(), faker.Address.FullAddress(), faker.Phone.PhoneNumberFormat(), Int32.Parse(faker.Finance.Account()), faker.Random.Guid().ToString(), faker.Date.Past());
                
                listPersonnes.Add(ami);
                listPersonnes.Add(client);

                cs.AjouterContact(ami);
                cs.AjouterContact(client);
            }
            return listPersonnes;
        }
    }
}
