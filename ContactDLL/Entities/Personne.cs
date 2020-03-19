using System;
using System.Collections.Generic;
using System.Text;

namespace ContactDLL
{
    public abstract class Personne
    {
        private int id;
        private string nom;
        private string prenom;
        private string email;
        private string adresse;
        private string telephone;

        public int Id { get => id; set => id = value; }

        public string Nom { get => nom; set => nom = value; }

        public string Prenom { get => prenom; set => prenom = value; }

        public string Email { get => email; set => email = value; }

        public string Adresse { get => adresse; set => adresse = value; }

        public string Telephone { get => telephone; set => telephone = value; }
     
    }
}
