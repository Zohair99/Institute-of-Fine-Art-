using System;
using System.Collections.Generic;

namespace IOFA.Models;

public partial class Remark
{
    public int Id { get; set; }

    public int ArtId { get; set; }

    public string? TeacherName { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Art Art { get; set; } = null!;
}
