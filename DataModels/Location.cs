using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class Location
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Details { get; set; }

        [NotMapped]
        public string GetFullInfo { get { return $"{this.Name} - {this.Details}"; } }

    }
}