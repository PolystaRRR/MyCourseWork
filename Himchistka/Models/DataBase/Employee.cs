using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Himchistka.Models.DataBase
{
    public class Employee
    {
        [DisplayName("Идентификатор сотрудника")]
        public int Id { get; set; }
        [DisplayName("Должность")]
        public string Post { get; set; }


    }
}
