using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Himchistka.Models.DataBase
{
    public class Order
    {
        [DisplayName("Идентификатор заказа")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [DisplayName("Дата приёма")]
        //[Required(ErrorMessage = "Выберите дату")]
        public DateTime AcceptanceDate { get; set; }

        [DisplayName("Дата готовности")]
        //[Required(ErrorMessage = "Выберите дату")]
        public DateTime ReadyDate { get; set; }

        [DisplayName("Адрес приемного пункта")]
        //[Required(ErrorMessage = "Выберите адрес приемного пункта")]
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
        [ForeignKey("ClientId")]

        public virtual Client Client { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();


        public virtual Service Service { get; set; }
    }
}

