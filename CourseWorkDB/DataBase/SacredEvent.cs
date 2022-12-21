using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class SacredEvent
{
    [DisplayName("Идентификатор мероприятия")]
    public int EventId { get; set; }
    [DisplayName("Место проведения")]
    public string Place { get; set; } = null!;
    [DisplayName("Дата окончания")]
    public DateTime FinishDate { get; set; }
    [DisplayName("Тип транспорта")]
    public string Transport { get; set; } = null!;
    [DisplayName("Маршрут")]
    public string? Route { get; set; }
    [DisplayName("Источник финансирования")]
    public string SourceOfFunding { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
