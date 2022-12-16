using System;
using System.Collections.Generic;

namespace CourseWorkDB.DataBase;

public partial class Parishioner
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public short Age { get; set; }

    public bool Sex { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int PriestId { get; set; }

    public virtual ICollection<Donation> Donations { get; } = new List<Donation>();

    public virtual Priest Priest { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
