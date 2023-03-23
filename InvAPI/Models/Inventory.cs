using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvAPI.Models;

public partial class Inventory
{
    public int Id { get; set; }

    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [NotMapped]
    public string? InvNum { get; set; }

    public static string CalculateInvNumProcessed(string inv_num)
    {
        throw new NotSupportedException();
    }
    public int? NomenclatureId { get; set; }

    public int? MoveId { get; set; }

    public int? CompanyId { get; set; }

    public int? PaymentNum { get; set; }

    public string? Comment { get; set; }

    public string? Invoice { get; set; }

    public int? WorkplaceId { get; set; }

    [JsonProperty("dateInvCreate")]
    public DateTime? DateInv { get; set; }

    public virtual ICollection<ActiveTask> ActiveTasks { get; } = new List<ActiveTask>();

    //public virtual Company? Company { get; set; }

    public virtual ICollection<Movement> Movements { get; } = new List<Movement>();

    //public virtual Nomenclature? Nomenclature { get; set; }

    public virtual ICollection<RevisionItem> RevisionItems { get; } = new List<RevisionItem>();

    //public virtual ICollection<Workplace> Workplaces { get; } = new List<Workplace>();

    public virtual ICollection<WriteOff> WriteOffs { get; } = new List<WriteOff>();
}
