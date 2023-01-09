using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CourseWorkDB.DataBase;

public partial class Donation
{
    [DisplayName("Идентификатор пожертвования")]
    public int Id { get; set; }
    [DisplayName("Сумма")]
    public decimal Sum { get; set; }
    [DisplayName("Цель трат")]
    public string Purpose { get; set; } = null!;
    [DisplayName("Идентификатор прихожанина")]
    public int ParishionerId { get; set; }

    public virtual Parishioner Parishioner { get; set; } = null!;
}
