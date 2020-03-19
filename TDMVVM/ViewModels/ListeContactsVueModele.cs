using ContactDLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace TDMVVM.ViewModels
{
    class ListeContactsVueModele : ViewModelBase
    {
        //la liste des contacts via leur vue-modèle de représentation
        private readonly ObservableCollection<ContactVueModele> listeContacts;
        //vue permettant de naviguer dans la liste des contacts (filtre, tri, sélection courante)
        private readonly ICollectionView collectionView;
        

        public ListeContactsVueModele()
        {
            //Chargement de la liste des contacts du modèle grâce à la classe de service
            List<Personne> lst = PersonneSingleton.Instance.ListPersonnes;

            //ajout de chaque contact dans l'ObservableCollection 
            //via l'instanciation d'une vue modele pour chacun d'eux
            listeContacts = new ObservableCollection<ContactVueModele>();
            foreach (Personne person in lst)
            {
                listeContacts.Add(new ContactVueModele(person, this));
            }

            //définition de la collection view
            collectionView = CollectionViewSource.GetDefaultView(listeContacts);
            //ajout de l'événement à déclencher quand la vue courante change
            collectionView.CurrentChanged += OnCollectionViewCurrentChanged;

            //on se place sur le 1er élément
            collectionView.MoveCurrentToFirst();
        }

        private void OnCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("ContactSelected");
        }

        //Accesseur à la liste des contacts vue-modèle
        public ObservableCollection<ContactVueModele> ListeContacts
        {
            get { return listeContacts; }
        }

        //Accesseur à la sélection courant (le 1er de la liste par défaut)
        public ContactVueModele ContactSelected
        {
            get
            {
                if (collectionView.CurrentItem == null) return null;
                return (ContactVueModele)collectionView.CurrentItem;
            }
        }

        private RelayCommand commandeSuivant;
        public ICommand CommandeSuivant
        {
            get
            {
                if (commandeSuivant == null)
                {
                    commandeSuivant = new RelayCommand(GoSuivant, CanGoSuivant);
                }
                return commandeSuivant;
            }
        }
        public void GoSuivant()
        {
            collectionView.MoveCurrentToNext();
        }
        public bool CanGoSuivant()
        {
            if (collectionView.CurrentPosition < ((System.Windows.Data.ListCollectionView)collectionView).Count - 1)
                return true;
            return false;
        }
        
        private RelayCommand commandePrecedent;
        public ICommand CommandePrecedent
        {
            get
            {
                if (commandePrecedent == null)
                {
                    commandePrecedent = new RelayCommand(GoPrecedent, CanGoPrecedent);
                }
                return commandePrecedent;
            }
        }
        public void GoPrecedent()
        {
            collectionView.MoveCurrentToPrevious();
        }
        public bool CanGoPrecedent()
        {
            if (collectionView.CurrentPosition > 0)
                return true;
            return false;
        }

        //Pour le setter : si la recherche est vide, on applique un style différent
        public bool TexteRechercherNoMatch
        {
            get { return collectionView.IsEmpty; }
        }
        //Propriété récupérant le texte de recherche saisi
        public string TexteRechercher
        {
            set
            {
                collectionView.Filter = item => IsMatch(item, value);
                OnPropertyChanged("TexteRechercherNoMatch");
            }
        }
        // Méthode appellée pour chaque élément de la collection
        // pour déterminer si l'élément correspond ou non à la recherche.
        private bool IsMatch(object item, string value)
        {
            if (!(item is ContactVueModele)) return false;

            value = value.ToUpper();
            ContactVueModele p = (ContactVueModele)item;
            return (p.Contact.Nom.ToUpper().Contains(value)
                    || p.Contact.Prenom.ToUpper().Contains(value));
        }

        private RelayCommand commandeTrier;
        public ICommand CommandeTrier
        {
            get
            {
                if (commandeTrier == null)
                {
                    commandeTrier = new RelayCommand(new Action<object>(TrierLaListe));
                }
                return commandeTrier;
            }
        }

        // Tri de la CollectionView :
        // l'utilisation de la méthode DeferRefresh permet d'effectuer le tri qu'à la sortie de l'instruction.
        // sinon, la collection serait réarrangée à chaque fois, au clear puis à chaque Add  !!
        public void TrierLaListe(object pNomProprieteDeTri)
        {
            if (pNomProprieteDeTri == null)
            {
                //tri par défaut
                using (collectionView.DeferRefresh())
                {
                    collectionView.SortDescriptions.Clear();
                    collectionView.SortDescriptions.Add(new SortDescription("Nom", ListSortDirection.Ascending));
                    collectionView.SortDescriptions.Add(new SortDescription("Prenom", ListSortDirection.Ascending));
                }
            }
            else
            {
                using (collectionView.DeferRefresh())
                {
                    collectionView.SortDescriptions.Clear();
                    collectionView.SortDescriptions.Add(new SortDescription(pNomProprieteDeTri.ToString(), ListSortDirection.Ascending));
                }
            }

        }
    }
}
