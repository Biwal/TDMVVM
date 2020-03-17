
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactDLL
{
    public sealed class PersonneSingleton
    {
        private static volatile PersonneSingleton instance;
        private static object syncRoot = new Object();
        private static volatile List<Personne> listPersonnes;

        public List<Personne> ListPersonnes { get => listPersonnes; set => listPersonnes = value; }

        private PersonneSingleton() {
            var faker = new Faker("fr");
            ListPersonnes = new List<Personne>();
            for (int i = 0; i < 25; i++)
            {
                ListPersonnes.Add(new Ami(faker.Name.FirstName(), faker.Name.LastName(), faker.Internet.Email(), faker.Address.FullAddress(), faker.Phone.PhoneNumberFormat(), faker.Date.Past(), faker.Phone.PhoneNumberFormat()));
                ListPersonnes.Add(new Client(faker.Name.FirstName(), faker.Name.LastName(), faker.Internet.Email(), faker.Address.FullAddress(), faker.Phone.PhoneNumberFormat(),Int32.Parse(faker.Finance.Account()) , faker.Random.Guid().ToString() ,faker.Date.Past()));
            }
       }

        public static PersonneSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new PersonneSingleton();
                    }
                }

                return instance;
            }
        }
    }
}
