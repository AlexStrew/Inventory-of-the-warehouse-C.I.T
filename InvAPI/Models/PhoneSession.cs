using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class PhoneSession
{
    public int IdSession { get; set; }

    public string? NumSession { get; set; }

    public DateTime? DateCreationSession { get; set; }

    public DateTime? DateExpireSession { get; set; }

    public string? UsernameSession { get; set; }
}
