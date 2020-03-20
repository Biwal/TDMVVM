using ContactDLL;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDMVVM.Model
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ContactsContext : DbContext
    {
        public ContactsContext() : base()
        {
        }

        //les entités ...
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ami> Amis { get; set; }
    }
}
