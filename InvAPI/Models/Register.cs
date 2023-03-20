using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class Register
{
    public string Username { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }
}
