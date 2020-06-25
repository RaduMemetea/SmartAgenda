using System;
using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public class Conference
    {
        public Conference() { }
        public Conference(Conference conference)//copy constructor
        {
            ID = conference.ID;
            Name = conference.Name;
            Start_Date = conference.Start_Date;
            End_Date = conference.End_Date;
        }

        public Conference(int id, string name, DateTime start_Date, DateTime end_Date)
        {
            ID = id;
            Name = name;
            Start_Date = start_Date;
            End_Date = end_Date;
        }

        [Required]
        public int ID { get; set; }
        
        [Required, MinLength(3), MaxLength(20)]
        public String Name { get; set; }

        public DateTime Start_Date { get; set; }
        
        public DateTime End_Date { get; set; }


    }
}
