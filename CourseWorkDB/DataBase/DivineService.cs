using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class DivineService
{
    [DisplayName("Идентификатор мероприятия")]
    public int EventId { get; set; }
    [DisplayName("Обоснование")]
    public string? Justification { get; set; }
    [DisplayName("Молитва")]
    public string? Prayer { get; set; }

    public virtual Event Event { get; set; } = null!;
}
