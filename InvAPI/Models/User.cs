using System;
using System.Collections.Generic;

namespace InvAPI.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;
}
