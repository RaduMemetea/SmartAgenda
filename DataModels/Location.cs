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
        public string GetFullInfo
        {
            get
            {
                if (Details == null || Details.Equals(""))
                    return $"{this.Name}";
                else
                    return $"{this.Name} - {this.Details}";
            }
        }

    }
}