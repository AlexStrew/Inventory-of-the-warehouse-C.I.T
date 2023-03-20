using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class RevisionItem
{
    public int IdQueue { get; set; }

    public int? ParentId { get; set; }

    public int? InventoryId { get; set; }

    public DateTime? DateScan { get; set; }

    public bool? IsDone { get; set; }

    public virtual Inventory? Inventory { get; set; }
}
