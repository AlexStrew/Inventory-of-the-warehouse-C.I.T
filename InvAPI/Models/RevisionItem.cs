using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class RevisionItem
{
    public int IdQueue { get; set; }
    [JsonProperty("")]
    public int? ListId { get; set; }
    public string? InvNum { get; set; }
    public string? NameDevice { get; set; }

    public int? InventoryId { get; set; }

    public DateTime? DateScan { get; set; }

    public bool? IsDone { get; set; }


}
