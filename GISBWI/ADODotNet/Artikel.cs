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
    
    public partial class artikel
    {
        public int idartikel { get; set; }
        public string judul { get; set; }
        public string isi { get; set; }
        public Nullable<System.DateTime> tanggal_buat { get; set; }
        public Nullable<int> status { get; set; }
        public string keterangan { get; set; }
        public Nullable<int> admin_idadmin { get; set; }
        public Nullable<int> jenis_artikel_idjenis_artikel { get; set; }
    
        public virtual admin admin { get; set; }
        public virtual jenis_artikel jenis_artikel { get; set; }
    }
}
