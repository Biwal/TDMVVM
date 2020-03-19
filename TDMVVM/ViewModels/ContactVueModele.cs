using ContactDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TDMVVM.ViewModels
{
    class ContactVueModele : ViewModelBase
    {
        //l'entité du modèle
        private Personne contact;
        //Accesseur sur le modèle (Property en lecture seule)
        public Personne Contact
        {
            get { return contact; }
        }

        public string Identite
        {
            get { return Contact.Prenom + "  " + Contact.Nom; }
        }

        // Pour gérer l'affichageen fonction du type de contact
        public bool EstClient
        {
            get
            {
                if (contact.GetType() == typeof(Client)) return true;
                return false;
            }
        }

        public bool EstAmi
        {
            get
            {
                if (contact.GetType() == typeof(Ami)) return true;
                return false;
            }
        }

        private readonly ListeContactsVueModele _parent;

        public ListeContactsVueModele Parent => _parent;

        //Initialisation de la vue modèle avec l'entité modèle
        public ContactVueModele(Personne personne, ListeContactsVueModele parent)
        {
            if (personne == null)
            {
                throw new NullReferenceException("Personne");
            }
            contact = personne;
            _parent = parent;
        }

        private RelayCommand commandeEnregistrer;
        public ICommand CommandeEnregistrer
        {
            get
            {
                if (commandeEnregistrer == null) commandeEnregistrer = new RelayCommand(EnregistrerContact);

                return commandeEnregistrer;
            }
        }
        public void EnregistrerContact()
        {
            // La persistance sera gérée dansle prochain TD
            if (Contact == null) AddContact();
            if (EstAmi) PutAmi();
            if (EstClient) Console.WriteLine("clicli");
            System.Windows.MessageBox.Show("Enregistrement du contact");
        }

        public void AddContact()
        {

        }
        public void PutAmi()
        {
        }

        private RelayCommand commandeSupprimer;
        public ICommand CommandeSupprimer
        {
            get
            {
                if (commandeSupprimer == null) commandeSupprimer = new RelayCommand(SupprimerContact);
                return commandeSupprimer;
            }
        }
        public void SupprimerContact()
        {
            _parent.ListeContacts.Remove(this);
            System.Windows.MessageBox.Show("Suppression du contact");
        }
    }
}
