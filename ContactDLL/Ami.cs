using System;
using System.Collections.Generic;
using System.Text;

namespace ContactDLL
{
    public class Ami : Personne
    {
        private DateTime anniversaire;
        private string telPerso;

        public Ami(string nom, string prenom, string email, string adresse, string telephone, DateTime anniversaire, string telPerso)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Adresse = adresse;
            Telephone = telephone;
            Anniversaire = anniversaire;
            TelPerso = telPerso;
        }
        
        public DateTime Anniversaire { get => anniversaire; set => anniversaire = value; }

        public string TelPerso { get => telPerso; set => telPerso = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Informations personnel : ");
            sb.Append("Nom : ").Append(Nom).Append("   Prénom: ").AppendLine(Prenom);
            sb.Append("Email : ").Append(Email).Append("   Adresse: ").AppendLine(Adresse);
            sb.Append("Téléphone: ").AppendLine(Telephone);
            sb.AppendLine("Informations de l'ami : ").Append("Téléphone Perso : ").Append(TelPerso).Append("   Anniversaire : ").AppendLine(Anniversaire.ToString());
            return sb.ToString();
        }
    }
}
