using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class SacredEvent
{
    public int EventId { get; set; }

    public string Place { get; set; } = null!;

    public DateTime FinishDate { get; set; }

    public string Transport { get; set; } = null!;

    public string? Route { get; set; }

    public string SourceOfFunding { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
