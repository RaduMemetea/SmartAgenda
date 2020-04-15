using DataModels;
using DataModels.Complex;
using System;
using System.Collections.Generic;

namespace FrontEnd.Pages.Models
{
    public class TalksResponse : Talk
    {
        public TalksResponse()
        {
        }
        public TalksResponse(Session_Talks session_Talks,Talk talk, IEnumerable<Person> persons):base(talk)
        {
            Persons = persons;
            Hour = session_Talks.Hour;
            Highlight = session_Talks.Highlight;
        }


        public virtual IEnumerable<Person> Persons { get; set; }      
        public DateTime Hour { get; set; }

        public bool Highlight { get; set; }


    }
}
