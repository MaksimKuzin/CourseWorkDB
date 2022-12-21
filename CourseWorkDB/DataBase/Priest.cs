using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class Priest
{
    [DisplayName("Идентификатор священнослужителя")]
    public int Id { get; set; }
    [DisplayName("Сан")]
    public string Title { get; set; } = null!;
    [DisplayName("Имя")]
    public string Name { get; set; } = null!;
    [DisplayName("Дата обращения")]
    public DateTime InitialDate { get; set; }

    public virtual ICollection<Event> Events { get; } = new List<Event>();

    public virtual ICollection<Parishioner> Parishioners { get; } = new List<Parishioner>();
}
