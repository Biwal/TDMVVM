using System;
using System.Collections.Generic;
using System.Text;

namespace ContactDLL
{
    public class Client : Personne
    {
        private int numClient;
        private string guid;
        private DateTime anciennete;

        public Client(string nom, string prenom, string email, string adresse, string telephone, int numClient, string guid, DateTime anciennete)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Adresse = adresse;
            Telephone = telephone;
            NumClient = numClient;
            Guid = guid;
            Anciennete = anciennete;
        }

        public int NumClient { get => numClient; set => numClient = value; }

        public string Guid { get => guid; set => guid = value; }

        public DateTime Anciennete { get => anciennete; set => anciennete = value; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Client numéro : ").AppendLine(NumClient.ToString()).AppendLine("Informations personnel : ");
            sb.Append("Nom : ").Append(Nom).Append("   Prénom: ").AppendLine(Prenom);
            sb.Append("Email : ").Append(Email).Append("   Adresse: ").AppendLine(Adresse);
            sb.Append("Téléphone: ").AppendLine(Telephone);
            sb.AppendLine("Informations Client : ").Append("GUID : ").Append(Guid).Append("   Ancienneté : ").AppendLine(Anciennete.ToString());
            return sb.ToString();
        }
    }
}
