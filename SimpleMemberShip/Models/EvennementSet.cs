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
    
    public partial class EvennementSet
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Place { get; set; }
        public System.DateTime Date { get; set; }
        public int UsersId { get; set; }
    
        public virtual UsersSet UsersSet { get; set; }
    }
}
