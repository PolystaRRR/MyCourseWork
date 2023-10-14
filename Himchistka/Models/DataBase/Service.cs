using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Himchistka.Models.DataBase
{
    public class Service
    {
        [DisplayName("ИД услуги")]
        public int Id { get; set; }
        [DisplayName("Наименование")]
        public string ServiceName { get; set; }
        [DisplayName("Стоимость услуги")]
        public int ServicePrice { get; set; }

        [DisplayName("Описание")]
        public string ServiceDescription { get; set; }

        [DisplayName("Тип обработки")]
        public string ProcessingType { get; set; }

        [DisplayName("Затраченное время (ч.)")]
        public int ServiceTimeSpent { get; set; }
        [DisplayName("Затраты на средства")]

        public int ResourcesExpention { get; set; }
        [DisplayName("ИД сотрудника")]

        //Relationships
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]

        public virtual Employee Employee { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
