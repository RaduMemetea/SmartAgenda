using DataModels;
using DataModels.Complex;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Models
{
    public class TalksResponse : Talk
    {
        public TalksResponse() { }

        public TalksResponse(Talk talk, int sessionID, DateTime hour, bool highlight, IEnumerable<Person> persons) : base(talk)
        {
            SessionID = sessionID;
            Persons = persons;
            Hour = hour;
            Highlight = highlight;
        }


        public int SessionID { get; set; }

        public DateTime Hour { get; set; }

        public bool Highlight { get; set; }

        public virtual IEnumerable<Person> Persons { get; set; }




        public Talk GetTalk
        {
            get
            {
                return new Talk
                {
                    ID = this.ID,
                    Name = this.Name,
                    Abstract = this.Abstract
                };
            }
        }

        public Session_Talks GetSession_Talk
        {
            get
            {
                return new Session_Talks
                {
                    SessionID = this.SessionID,
                    TalkID = this.ID,
                    Hour = this.Hour,
                    Highlight = this.Highlight

                };
            }
        }

        public string ParsePersonsString
        {
            get
            {
                if (Persons is null) return "";


                string persons = "";
                int i = 0;

                foreach (var person in this.Persons)
                {
                    if (i == 0)
                    {
                        persons = $"{person.FullName}";
                        i = 1;
                    }
                    else
                        persons = $"{persons}, {person.FullName}";

                }
                return persons;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    List<Person> persons = new List<Person>();
                    var personsSplit = value.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    for (int pi = 0; pi < personsSplit.Length; pi++)
                    {
                        var person = personsSplit[pi];
                        var pers = person.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        string firstName = pers[0];
                        string lastName = pers.Last();
                        if (pers.Length > 2)
                            for (int i = 1; i < pers.Length - 1; i++)
                                firstName += (" " + pers[i]);

                        persons.Add(new Person { ID = 0, First_Name = firstName, Last_Name = lastName });
                    }

                    Persons = persons.ToArray();
                }
            }
        }

    }

}
