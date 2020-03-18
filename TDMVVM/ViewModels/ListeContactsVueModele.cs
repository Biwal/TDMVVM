using ContactDLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
                listeContacts.Add(new ContactVueModele(person));
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

    }
}
