﻿using System.ComponentModel;

namespace Himchistka.Models.DataBase
{
    public class Product
    {
        [DisplayName("Идентификатор изделия")]

        public int Id { get; set; }
        [DisplayName("Наименование изделия")]

        public string ProductName { get; set; }
        [DisplayName("Обьявленная цена")]
        public int DeclaredValue { get; set; }
        [DisplayName("Вид ткани")]
        public string FabricType { get; set; }
        [DisplayName("Пятна")]

        public bool DirtStains { get; set; }
        [DisplayName("Цвет")]

        public string Color { get; set; }
        [DisplayName("Дефекты")]

        public string Defects { get; set; }
        [DisplayName("Ярлык")]

        public string Label { get; set; }
        [DisplayName("Идентификатор заказа")]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}