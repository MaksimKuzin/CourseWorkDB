using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class Inventory
{
    [DisplayName("Идентификатор инвентаря")]
    public int Id { get; set; }
    [DisplayName("Название")]
    public string Name { get; set; } = null!;
    [DisplayName("Стоимость")]
    public decimal Price { get; set; }
    [DisplayName("Дата приобретения")]
    public DateTime DateOfPurchase { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}
