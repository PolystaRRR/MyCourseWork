using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Himchistka.Models.DataBase
{
    public class Client
    {
        [DisplayName("ИД клиента")]
        [Key]
        public int PhysicalPersonId { get; set; }
        [DisplayName("Телефон")]

        public string PhoneNumber { get; set; }
        [DisplayName("Электронная почта")]

        public string? Email { get; set; } = null!;
        [DisplayName("Адрес")]

        public string? Adress { get; set; }

        //Relationships

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual PhysicalPerson PhysicalPerson { get; set; } = null!;
    }
}
