//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleMemberShip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersSet
    {
        public UsersSet()
        {
            this.EvennementSet = new HashSet<EvennementSet>();
            this.CarteSet = new HashSet<CarteSet>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Role { get; set; }
        public int Tel { get; set; }
        public string Sexe { get; set; }
    
        public virtual ICollection<EvennementSet> EvennementSet { get; set; }
        public virtual ICollection<CarteSet> CarteSet { get; set; }
    }
}
