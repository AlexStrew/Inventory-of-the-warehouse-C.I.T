using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class RevisionItem
{
    public int IdQueue { get; set; }

    public int? ListId { get; set; }


    public int? InventoryId { get; set; }

    public DateTime? DateScan { get; set; }

    public bool? IsDone { get; set; }

 

}
 public partial class RevisionItemNomenInv
{
    public int IdQueue { get; set; }

    public int? ListId { get; set; }

    public string? InvNum { get; set; }

    public string? NameSubject { get; set; }

    public int? InventoryId { get; set; }

    public DateTime? DateScan { get; set; }

    public bool? IsDone { get; set; }

}