using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Himchistka.Models.DataBase
{
    public class Order
    {
        [DisplayName("Идентификатор заказа")]

        public int Id { get; set; }
        [DisplayName("Дата приёма")]

        public DateTime AcceptanceDate { get; set; }

        [DisplayName("Дата готовности")]

        public DateTime ReadyDate { get; set; }

        [DisplayName("Адрес приемного пункта")]

        public string ReceptionPoint { get; set; }

        [DisplayName("Доставка")]

        public bool? Delivery { get; set; }
        [DisplayName("Скидка")]


        public int? Discount { get; set; }
        [DisplayName("Итоговая цена")]

        public int FinalPrice { get; set; }

        [DisplayName("Тип оплаты")]
        public string PaymentType { get; set; }
        

        [DisplayName("Идентификатор клиента")]
        public int ClientId { get; set; }


        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<Product> Products { get; } = new List<Product>();


        public virtual Service Service { get; set; }
    }
}

