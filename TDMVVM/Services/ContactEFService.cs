using ContactDLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMVVM.Services
{
    public class ContactEFService
    {
        private static volatile ContactEFService instance;
        private static object syncRoot = new Object();
        private ContactEFService(){}

        public static ContactEFService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ContactEFService();
                    }
                }

                return instance;
            }
        }

        public List<Personne> ChargerListeContacts()
        {
            List<Personne> liste = new List<Personne>();

            using (Model.ContactsContext context = new Model.ContactsContext())
            {
                var lstClients = context.Clients.ToList();
                var lstAmis = context.Amis.ToList();
                liste.AddRange(lstClients);
                liste.AddRange(lstAmis);
            }
            return liste;
        }

        public bool EnregistrerContact(Personne contact)
        {
            using (Model.ContactsContext context = new Model.ContactsContext())
            {
                if (contact.Id > 0)
                {
                    if (contact.GetType() == typeof(Client)) context.Clients.Attach((Client)contact);
                    else context.Amis.Attach((Ami)contact);

                }
                else
                {
                    if (contact.GetType() == typeof(Client)) context.Clients.Add((Client)contact);
                    else context.Amis.Add((Ami)contact);
                }
                //répercute les changements en base
                context.SaveChanges();
            }
            return true;
        }

        public bool SupprimerContact(Personne contact)
        {
            using (Model.ContactsContext context = new Model.ContactsContext())
            {

                if (contact.Id > 0)
                {
                    context.Entry(contact).State = EntityState.Deleted;
                    if (contact.GetType() == typeof(Client)) context.Clients.Remove((Client)contact);
                    else context.Amis.Remove((Ami)contact);
                    context.SaveChanges();
                }
            }
            return true;
        }
    }
}
