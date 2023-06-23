﻿using System.ComponentModel;

namespace Himchistka.Models.DataBase
{
    public class PhysicalPerson
    {
        [DisplayName("Идентификатор физического лица")]
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Фамилия")]

        public string Surname { get; set; }
        [DisplayName("Отчество")]

        public string MiddleName { get; set; }
    }
}
