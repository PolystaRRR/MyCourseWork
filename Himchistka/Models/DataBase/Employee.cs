using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Himchistka.Models.DataBase
{
    public class Employee
    {
        [DisplayName("ИД сотрудника")]
        public int PhysicalPersonId { get; set; }
        [DisplayName("Должность")]
        public string Post { get; set; }

        //Relationships
        public virtual PhysicalPerson PhysicalPerson { get; set; } = null!;
        public virtual ICollection<Service> Services { get; set; } = null!;
        
    }
}
