using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Placement
{
    public int IdPlacement { get; set; }

    public string? NamePlacement { get; set; }

    public virtual ICollection<Movement> Movements { get; } = new List<Movement>();

    //public virtual ICollection<Workplace> Workplaces { get; } = new List<Workplace>();
}
