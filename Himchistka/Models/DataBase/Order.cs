using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Himchistka.Models.DataBase
{
    public class Order
    {
        [DisplayName("ИД заказа")]
        
        public int Id { get; set; }
        [DisplayName("Дата приёма")]
        
        public DateTime AcceptanceDate { get; set; }

        [DisplayName("Дата готовности")]
        
        public DateTime ReadyDate { get; set; }

        [DisplayName("Адрес приемного пункта")]
        public string ReceptionPoint { get; set; }

        [DisplayName("Доставка")]

        public bool Delivery { get; set; }
        [DisplayName("Скидка")]

        [DefaultValue(0)]
        public int? Discount { get; set; }
        [DisplayName("Итоговая цена")]
        
        public int FinalPrice { get; set; }

        [DisplayName("Тип оплаты")]
        public string PaymentType { get; set; }

        //Relationships

        [DisplayName("ИД клиента")]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]

        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();


        [DisplayName("ИД услуги")]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; } = null!;



        
    }
}

