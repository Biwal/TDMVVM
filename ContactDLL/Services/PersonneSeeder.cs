using Bogus;
using System;
using System.Collections.Generic;
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
            List<Personne> listPersonnes = new List<Personne>();
            for (int i = 0; i < 25; i++)
            {
                listPersonnes.Add(new Ami(faker.Name.FirstName(), faker.Name.LastName(), faker.Internet.Email(), faker.Address.FullAddress(), faker.Phone.PhoneNumberFormat(), faker.Date.Past(), faker.Phone.PhoneNumberFormat()));
                listPersonnes.Add(new Client(faker.Name.FirstName(), faker.Name.LastName(), faker.Internet.Email(), faker.Address.FullAddress(), faker.Phone.PhoneNumberFormat(), Int32.Parse(faker.Finance.Account()), faker.Random.Guid().ToString(), faker.Date.Past()));
            }
            return listPersonnes;
        }
    }
}
