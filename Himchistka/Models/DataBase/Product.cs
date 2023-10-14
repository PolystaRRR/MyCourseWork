using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Himchistka.Models.DataBase
{
    public class Product
    {
        [DisplayName("ИД изделия")]
        [Key]
        public int Id { get; set; }
        [DisplayName("Наименование")]

        public string ProductName { get; set; }
        [DisplayName("Размер")]
        public string? Size { get; set; }

        [DisplayName("Обьявленная ценность")]
        public int? DeclaredValue { get; set; }
        [DisplayName("Вид ткани")]
        public string FabricType { get; set; }
        [DisplayName("Пятна")]

        public bool DirtStains { get; set; }
        [DisplayName("Цвет")]

        public string? Color { get; set; }
        [DisplayName("Дефекты")]

        public string? Defects { get; set; }
        [DisplayName("Ярлык")]

        public string Label { get; set; }

        //Relationships

        [DisplayName("ИД заказа")]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
