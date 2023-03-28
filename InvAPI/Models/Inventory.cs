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
    [JsonProperty("inv_num")]
    [NotMapped]
    public string? InvNum { get; set; }

    public static string CalculateInvNumProcessed(string inv_num)
    {
        throw new NotSupportedException();
    }


    public int? MoveId { get; set; }

    public int? CompanyId { get; set; }

    public string? PaymentNum { get; set; }

    public string? Comment { get; set; }

    public string? Invoice { get; set; }

    public int? SubjectId { get; set; }

    public string? SerialNumber { get; set; }



    [JsonProperty("dateInvCreate")]
    public DateTime? DateInv { get; set; }

    public virtual ICollection<ActiveTask> ActiveTasks { get; } = new List<ActiveTask>();

    //public virtual Movement? Movements { get; set; }

    //public virtual ICollection<Movement> Movements { get; } = new List<Movement>();

    
    public virtual ICollection<RevisionItem> RevisionItems { get; } = new List<RevisionItem>();

    //public virtual ICollection<Workplace> Workplaces { get; } = new List<Workplace>();

    public virtual ICollection<WriteOff> WriteOffs { get; } = new List<WriteOff>();
}
