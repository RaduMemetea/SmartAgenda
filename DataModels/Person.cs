using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class Person
    {
        public int ID { get; set; }
        public String First_Name { get; set; }
        public String Last_Name { get; set; }
        public String Details { get; set; }

        [NotMapped]
        public string GetFullName { get { return $"{this.First_Name} {this.Last_Name}"; } }
    }
}
