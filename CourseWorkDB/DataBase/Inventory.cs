using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class Inventory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime DateOfPurchase { get; set; }

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}
