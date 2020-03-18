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

        //Initialisation de la vue modèle avec l'entité modèle
        public ContactVueModele(Personne personne)
        {
            if (personne == null)
            {
                throw new NullReferenceException("Personne");
            }
            contact = personne;
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
            System.Windows.MessageBox.Show("Suppression du contact");
        }
    }
}
