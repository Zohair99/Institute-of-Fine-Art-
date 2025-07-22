using System;
using System.Collections.Generic;

namespace IOFA.Models;

public partial class Art
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Path { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? UploadedBy { get; set; }

    public string? UploaderName { get; set; }

    public virtual ICollection<Remark> Remarks { get; set; } = new List<Remark>();
}
