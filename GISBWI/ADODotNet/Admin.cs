//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GISBWI.ADODotNet
{
    using System;
    using System.Collections.Generic;
    
    public partial class admin
    {
        public admin()
        {
            this.artikels = new HashSet<artikel>();
            this.galeris = new HashSet<galeri>();
            this.services = new HashSet<service>();
        }
    
        public int idadmin { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> privilege_idprivilege { get; set; }
        public Nullable<int> skpd_idskpd { get; set; }
    
        public virtual privilege privilege { get; set; }
        public virtual skpd skpd { get; set; }
        public virtual ICollection<artikel> artikels { get; set; }
        public virtual ICollection<galeri> galeris { get; set; }
        public virtual ICollection<service> services { get; set; }
    }
}
