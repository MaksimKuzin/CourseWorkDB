using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class Event
{
    [DisplayName("Идентификатор мероприятия")]
    public int Id { get; set; }
    [DisplayName("Название")]
    public string Name { get; set; } = null!;
    [DisplayName("Дата")]
    public DateTime Date { get; set; }
    [DisplayName("Тип мероприятия")]
    public string Type { get; set; }

    public int PriestId { get; set; }

    public virtual Activity? Activity { get; set; }

    public virtual DivineService? DivineService { get; set; }

    public virtual ICollection<Inventory> Inventories { get; } = new List<Inventory>();

    public virtual Priest Priest { get; set; } = null!;

    public virtual SacredEvent? SacredEvent { get; set; }

    public virtual ICollection<Parishioner> Parishioners { get; } = new List<Parishioner>();
}
