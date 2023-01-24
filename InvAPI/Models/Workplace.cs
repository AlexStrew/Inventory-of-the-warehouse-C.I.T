using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Workplace
{
    public int IdWorkplace { get; set; }

    public int? IdInventory { get; set; }

    public string? NameWorkplace { get; set; }

    public int? PlacementIdWp { get; set; }

    public string? Mol { get; set; }

    public int? DeviceId { get; set; }

    public int? EmployerId { get; set; }

    public virtual Nomenclature? Device { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual Inventory? IdInventoryNavigation { get; set; }

    public virtual Placement? PlacementIdWpNavigation { get; set; }
}
