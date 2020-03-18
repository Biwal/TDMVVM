using ContactDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return Contact.Prenom + "  " + Contact.Nom;  }
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
    }
}
