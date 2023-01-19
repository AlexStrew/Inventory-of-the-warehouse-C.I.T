//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inventarisation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Workplace
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Workplace()
        {
            this.Inventory = new HashSet<Inventory>();
        }
    
        public int id_workplace { get; set; }
        public Nullable<int> id_inventory { get; set; }
        public string name_workplace { get; set; }
        public Nullable<int> id_placement { get; set; }
        public string mol { get; set; }
        public Nullable<int> device_id { get; set; }
        public Nullable<int> employer_id { get; set; }
    
        public virtual Employer Employer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual Nomenclature Nomenclature { get; set; }
        public virtual Placements Placements { get; set; }
    }
}
