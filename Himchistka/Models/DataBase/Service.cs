using System.ComponentModel;

namespace Himchistka.Models.DataBase
{
    public class Service
    {
        [DisplayName("Идентификатор услуги")]
        public int Id { get; set; }
        [DisplayName("Наименование услуги")]
        public string ServiceName { get; set; }
        [DisplayName("Тип обработки")]
        public string ProcessingType { get; set; }
        [DisplayName("Описание услуги")]
        public string ServiceDescription { get; set; }
        [DisplayName("Затраты средств")]
        public int ResourcesExpention { get; set; }
    }
}
