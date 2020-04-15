using System;
using System.Collections.Generic;

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

        public int ID { get; set; }
        public String Name { get; set; }
        public DateTimeOffset Start_Date { get; set; }
        public DateTimeOffset End_Date { get; set; }


    }
}
