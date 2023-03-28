using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Movement
{
    public int IdMovement { get; set; }

    public int IdInventory { get; set; }

    public DateTime? DateMove { get; set; }

    public int? PlacementId { get; set; }

    public string? Planner { get; set; }

    public int? EmployerId { get; set; }

    //public virtual Inventory IdInventoryNavigation { get; set; } = null!;
    //public virtual Inventory? Inventory { get; set; }

    //public virtual Placement? Placement { get; set; }
}
