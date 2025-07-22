using System;
using System.Collections.Generic;

namespace IOFA.Models;

public partial class Register
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? CPassword { get; set; }

    public string? Phone { get; set; }

    public string? Artist { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now; // Default value

}
