using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public class Session
    {

        public Session() { }
        public Session(Session session) //copy constructor
        {
            ID = session.ID;
            Name = session.Name;
            Start_Hour = session.Start_Hour;
            End_Hour = session.End_Hour;
            ConferenceID = session.ConferenceID;
            LocationID = session.LocationID;
        }


        [Key]
        public int ID { get; set; }
        public String Name { get; set; }
        public DateTime Start_Hour { get; set; }
        public DateTime End_Hour { get; set; }

        [ForeignKey("Conference")]
        public int ConferenceID { get; set; }
        [ForeignKey("Location")]
        public int LocationID { get; set; }

    }
}
