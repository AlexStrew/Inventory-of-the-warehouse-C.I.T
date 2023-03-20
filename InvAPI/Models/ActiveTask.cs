using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class ActiveTask
{
    public int IdTask { get; set; }

    public int? ParentId { get; set; }

    public int? InventoryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Inventory? Inventory { get; set; }
}
