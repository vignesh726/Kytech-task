using System;
using System.Collections.Generic;

namespace Kytech.models;

public partial class UserDetail
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public int? Age { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }
}
