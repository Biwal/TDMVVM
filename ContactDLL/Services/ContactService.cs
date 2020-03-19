using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactDLL.Services
{
    public class ContactService
    {
        private MySqlConnectionStringBuilder csb;

        public MySqlConnectionStringBuilder CSB
        {
            get { return csb; }
            set { csb = value; }
        }
        
        public ContactService()
        {
            CSB = new MySqlConnectionStringBuilder();
            CSB.Server = "localhost";
            CSB.Database = "tdmvvm";
            CSB.UserID = "root";
            CSB.Password = "";
        }

        public List<Personne> ChargerListeContacts()
        {
            List<Personne> liste = new List<Personne>();

            using (MySqlConnection conn = new MySqlConnection(csb.ConnectionString))
            {
                conn.Open(); //ouverture de la connexion

                string sql = "select type, id, nom, prenom, email, telephone, adresse, " +
                "guid, noclient, telperso, date " +
                "from personnes";

                MySqlCommand command = new MySqlCommand(sql, conn);

                

                //exécution de la commande
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //on commence par récupérer le type afin de créer le client ou l'ami :
                    string stype = reader.GetString(0);
                    Personne personne;
                    if (stype == "C") personne = new Client();
                    else personne = new Ami();

                    // Chargement des colonnes NON NULL
                    personne.Id = reader.GetInt32(1);
                    personne.Nom = reader.GetString(2);
                    personne.Prenom = reader.GetString(3);

                    // Chargement des colonnes NULL avec test pour éviter le plantage
                    if (reader.IsDBNull(4) == false) personne.Email = reader.GetString(4);
                    if (reader.IsDBNull(5) == false) personne.Telephone = reader.GetString(5);
                    if (reader.IsDBNull(6) == false) personne.Adresse = reader.GetString(6);

                    // Colonnes spécifiques AMI
                    if (reader.IsDBNull(9) == false) (personne as Ami).TelPerso = reader.GetString(9);
               
                    // Colonnes spécifiques CLIENT
                    if (reader.IsDBNull(7) == false) (personne as Client).Guid = reader.GetString(7);
                    if (reader.IsDBNull(8) == false) (personne as Client).NumClient = reader.GetInt32(8);
                    
                    if (reader.IsDBNull(10) == false)
                    {
                        if (stype == "A") (personne as Ami).Anniversaire = reader.GetDateTime(10);
                        else (personne as Client).Anciennete = reader.GetDateTime(10);
                    }
                    liste.Add(personne);
                }
                reader.Close(); //fermeture obligatoire !

                conn.Close(); //fermeture obligatoire !
            }
            Console.WriteLine(liste.Count);           
            return liste;
        }

        public void AjouterContact(Personne contact)
        {
            using (MySqlConnection conn = new MySqlConnection(CSB.ConnectionString))
            {
                conn.Open(); //ouverture de la connexion

                string sql = "";
                if (contact.Id > 0) //existant ou nouveau ?
                {
                    sql = "update personnes set nom=@Nom, prenom=@prenom, adresse=@adresse, " +
                                                "email=@email, telephone=@telephone, " +
                                                "guid=@guid, noclient=@noclient, " +
                                                "telperso=@telperso, date=@date " +
                            "where id=@id";
                }
                else
                {
                    sql = "insert into personnes (nom,prenom,adresse,email,telephone," +
                                                    "guid,noclient,telperso,date,type) " +
                            "values (@nom,@prenom,@adresse,@email,@telephone," +
                                    "@guid,@noclient,@telperso,@date,@type)";
                }

                MySqlCommand command = new MySqlCommand(sql, conn);


                command.Parameters.Add(new MySqlParameter("@nom", contact.Nom));
                command.Parameters.Add(new MySqlParameter("@prenom", contact.Prenom));
                command.Parameters.Add(new MySqlParameter("@adresse", contact.Adresse));
                command.Parameters.Add(new MySqlParameter("@email", contact.Email));
                command.Parameters.Add(new MySqlParameter("@telephone", contact.Telephone));
                if (contact.GetType() == typeof(Client))
                {
                    command.Parameters.Add(new MySqlParameter("@guid", (contact as Client).Guid));
                    command.Parameters.Add(new MySqlParameter("@noclient", (contact as Client).NumClient));
                    command.Parameters.Add(new MySqlParameter("@date", (contact as Client).Anciennete));
                    command.Parameters.Add(new MySqlParameter("@telperso", null));
                }
                else
                {
                    command.Parameters.Add(new MySqlParameter("@telperso", (contact as Ami).TelPerso));
                    command.Parameters.Add(new MySqlParameter("@guid", null));
                    command.Parameters.Add(new MySqlParameter("@noclient", null));
                    command.Parameters.Add(new MySqlParameter("@date", (contact as Ami).Anniversaire));
                }

                if (contact.Id > 0)  // si update
                {
                    command.Parameters.Add(new MySqlParameter("@id", contact.Id));
                }
                else  // si insert
                {
                    if (contact.GetType() == typeof(Client))
                        command.Parameters.Add(new MySqlParameter("@type", "C"));
                    else
                        command.Parameters.Add(new MySqlParameter("@type", "A"));
                }
                command.ExecuteNonQuery();
                conn.Close(); //fermeture obligatoire !
            }
        }

        public void Supprimer(Personne contact)
        {
            using (MySqlConnection conn = new MySqlConnection(CSB.ConnectionString))
            {
                conn.Open(); //ouverture de la connexion

                string sql = "";
                if (contact.Id > 0) //existant ou nouveau ?
                {
                    sql =  "delete from personnes where id=@id ";
                }
              
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.Parameters.Add(new MySqlParameter("@id", contact.Id));
                
                command.ExecuteNonQuery();
                conn.Close(); //fermeture obligatoire !
            }
        }
    }
}
