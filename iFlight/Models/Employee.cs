using System;
using System.Collections.Generic;

namespace iFlight.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? TelephoneNumber { get; set; }

    public string? PasswordKey { get; set; }
}
