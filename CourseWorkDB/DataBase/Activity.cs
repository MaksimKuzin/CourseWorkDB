using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class Activity
{
    public int EventId { get; set; }
    [DisplayName("Продолжительность")]
    public short? Duration { get; set; }
    [DisplayName("Количество прихожан")]
    public short? ParishionerAmount { get; set; }
    public virtual Event Event { get; set; } = null!;
}
