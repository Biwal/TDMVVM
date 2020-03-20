using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContactDLL
{
    public abstract class Personne
    {
        private int id;
        private string nom;
        private string prenom;
        private string email;
        private string adresse;
        private string telephone;

        [Column("id")]
        public int Id { get => id; set => id = value; }
        [Column("nom")]
        [Required]
        [StringLength(80)]
        public string Nom { get => nom; set => nom = value; }
        [Column("prenom")]
        [Required]
        [StringLength(80)]
        public string Prenom { get => prenom; set => prenom = value; }
        [Column("email")]
        [StringLength(160)]
        public string Email { get => email; set => email = value; }
        [Column("adresse")]
        [StringLength(255)]
        public string Adresse { get => adresse; set => adresse = value; }
        [Column("telephone")]
        [StringLength(20)]
        public string Telephone { get => telephone; set => telephone = value; }
     
    }
}
