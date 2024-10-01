using System;
using System.Collections.Generic;

namespace Software2_API.Repositories.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public int IdState { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual State IdStateNavigation { get; set; } = null!;
}
