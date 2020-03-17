
using Bogus;
using ContactDLL.Services;
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
            ListPersonnes = new List<Personne>();
            ListPersonnes = new PersonneSeeder().seed();
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
