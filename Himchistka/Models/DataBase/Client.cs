using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Himchistka.Models.DataBase
{
    public class Client
    {
        [DisplayName("Идентификатор клиента")]

        public int Id { get; set; }
        [DisplayName("Телефон")]

        public string Phone { get; set; }
        [DisplayName("Электронная почта")]

        public string Email { get; set; }
        [DisplayName("Адрес проживания")]

        public string Adress { get; set; }

        
    }
}
