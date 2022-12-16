using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public int PriestId { get; set; }

    public virtual Activity? Activity { get; set; }

    public virtual DivineService? DivineService { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();

    public virtual Priest Priest { get; set; } = null!;

    public virtual SacredEvent? SacredEvent { get; set; }

    public virtual ICollection<Parishioner> Parishioners { get; } = new List<Parishioner>();
}
