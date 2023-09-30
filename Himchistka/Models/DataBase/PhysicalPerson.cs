using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Himchistka.Models.DataBase
{
    public class PhysicalPerson
    {
        [DisplayName("ИД физического лица")]
        
        public int Id { get; set; }
        [DisplayName("Имя")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Фамилия")]

        public string Surname { get; set; }
        [DisplayName("Отчество")]


        public string MiddleName { get; set; }
        [DisplayName("Пол")]

        public bool Sex { get; set; }

        //Relationships
        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
