using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Company
{
    public int IdCompany { get; set; }

    public string? CompanyName { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();
}
