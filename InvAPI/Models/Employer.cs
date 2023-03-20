using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Employer
{
    public int IdEmpolyer { get; set; }

    public string FullName { get; set; }


    public virtual ICollection<Workplace> Workplaces { get; } = new List<Workplace>();
}
