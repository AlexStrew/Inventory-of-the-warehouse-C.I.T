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
    
    public partial class Movements
    {
        public int id { get; set; }
        public int id_inventory { get; set; }
        public Nullable<System.DateTime> date_move { get; set; }
        public Nullable<int> id_placement { get; set; }
        public string planner { get; set; }
    
        public virtual Inventory Inventory { get; set; }
        public virtual Placements Placements { get; set; }
    }
}
