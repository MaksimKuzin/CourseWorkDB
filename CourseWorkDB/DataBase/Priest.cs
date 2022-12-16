using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class Priest
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime InitialDate { get; set; }

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Parishioner> Parishioners { get; } = new List<Parishioner>();
}
