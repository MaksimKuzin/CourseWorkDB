using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class Activity
{
    public int EventId { get; set; }

    public short? Duration { get; set; }

    public short? ParishionerAmount { get; set; }

    public virtual Event Event { get; set; } = null!;
}
