using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Himchistka.Models.DataBase
{
    public class Client
    {
        [DisplayName("Идентификатор клиента")]

        public int PhysicalPersonId { get; set; }
        [DisplayName("Телефон")]

        public string Phone { get; set; }
        [DisplayName("Электронная почта")]

        public string Email { get; set; } = null!;
        [DisplayName("Адрес проживания")]

        public string Adress { get; set; }

        //relationships

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual PhysicalPerson PhysicalPerson { get; set; } = null!;
    }
}
