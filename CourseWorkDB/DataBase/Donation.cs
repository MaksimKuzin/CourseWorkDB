using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class Donation
{
    public int Id { get; set; }

    public decimal Sum { get; set; }

    public string Purpose { get; set; } = null!;

    public int ParishionerId { get; set; }

    public virtual Parishioner Parishioner { get; set; } = null!;
}
