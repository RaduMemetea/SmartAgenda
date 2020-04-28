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

        public Conference(int iD, string name, DateTimeOffset start_Date, DateTimeOffset end_Date)
        {
            ID = iD;
            Name = name;
            Start_Date = start_Date;
            End_Date = end_Date;
        }

        [Required]
        public int ID { get; set; }
        
        [Required, MinLength(3), MaxLength(20)]
        public String Name { get; set; }

        public DateTimeOffset Start_Date { get; set; }
        
        public DateTimeOffset End_Date { get; set; }


    }
}
