using CourseWorkDB.DataBase;
using System.ComponentModel;

namespace CourseWorkDB.Models
{
    public class ParishionerHelper
    {
        [DisplayName("Идентификатор прихожанина")]
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; } = null!;
        [DisplayName("Фамилия")]
        public string Surname { get; set; } = null!;
        [DisplayName("Отчество")]
        public string Patronymic { get; set; } = null!;
        [DisplayName("Возраст")]
        public short Age { get; set; }
        [DisplayName("Пол")]
        public bool Sex { get; set; }
        [DisplayName("Адрес")]
        public string Address { get; set; } = null!;
        [DisplayName("Сотовый номер")]
        public string PhoneNumber { get; set; } = null!;

        public int PriestId { get; set; }
        [DisplayName("Сумма пожертвований")]
        public decimal DonationSum { get; set; }

        public virtual ICollection<Donation> Donations { get; } = new List<Donation>();

        public virtual Priest Priest { get; set; } = null!;

        public virtual ICollection<Event> Events { get; } = new List<Event>();
    }
}
