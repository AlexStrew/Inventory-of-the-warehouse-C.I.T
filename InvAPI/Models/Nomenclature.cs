using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Nomenclature
{
    public int IdNomenclature { get; set; }

    public string? NameDevice { get; set; }

    public int? CountDevice { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public DateTime? DateCreation { get; set; }

    public DateTime? DateChange { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();

    //public virtual ICollection<Workplace> Workplaces { get; } = new List<Workplace>();
}
