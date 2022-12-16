using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class DivineService
{
    public int EventId { get; set; }

    public string? Justification { get; set; }

    public string? Prayer { get; set; }

    public virtual Event Event { get; set; } = null!;
}
