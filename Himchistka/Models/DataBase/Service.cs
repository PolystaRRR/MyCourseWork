using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Himchistka.Models.DataBase
{
    public class Service
    {
        [DisplayName("Идентификатор услуги")]
        public int Id { get; set; }
        [DisplayName("Наименование услуги")]
        public string ServiceName { get; set; }
        [DisplayName("Стоимость услуги")]
        public int ServicePrice { get; set; }

        [DisplayName("Описание услуги")]
        public string ServiceDescription { get; set; }

        [DisplayName("Тип обработки")]
        public string ProcessingType { get; set; }

        [DisplayName("Затраченное время на услугу")]
        public int ServiceTimeSpent { get; set; }
        [DisplayName("Затраты средств")]

        public int ResourcesExpention { get; set; }
        [DisplayName("Идентификатор сотрудника")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]

        public virtual Order Order { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
