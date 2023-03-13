using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class WriteOff
{
    public int IdWriteoff { get; set; }

    public int IdInventory { get; set; }

    public int? CountWriteoff { get; set; }

    public string? Reason { get; set; }

    public virtual Inventory IdInventoryNavigation { get; set; } = null!;
}
