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
            set => contact = value;
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
            _parent.CS.AjouterContact(Contact);
            _parent.GetListeContacts();

            System.Windows.MessageBox.Show("Enregistrement du contact");
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
            _parent.CS.Supprimer(Contact);
            _parent.GetListeContacts();
            System.Windows.MessageBox.Show("Suppression du contact");
        }
    }
}
