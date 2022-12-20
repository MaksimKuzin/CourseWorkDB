using System.ComponentModel;

namespace CourseWorkDB.Models
{
    public class DivineServiceHelper
    {
        [DisplayName("Навание")]
        public string Name { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Идентификатор священносулжителя")]
        public int PriestId { get; set; }
        [DisplayName("Обоснование")]
        public string? Justification { get; set; }
        [DisplayName("Молитва")]
        public string? Prayer { get; set; }
    }
}
