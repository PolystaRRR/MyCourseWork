using System.ComponentModel;

namespace Himchistka.Models.DataBase
{
    public class Order
    {
        [DisplayName("Идентификатор заказа")]

        public int Id { get; set; }
        [DisplayName("Дата приёмки")]

        public DateTime AcceptanceDate { get; set; }

        [DisplayName("Дата готовности")]

        public DateTime ReadyDate { get; set; }

        [DisplayName("Адрес приемного пункта")]

        public string ReceptionPoint { get; set; }

        [DisplayName("Доставка")]

        public bool Delivery { get; set; }
        [DisplayName("Скидка")]


        public int Discount { get; set; }
        [DisplayName("Итоговая цена")]

        public int FinalPrice { get; set; }
        

    }
}
