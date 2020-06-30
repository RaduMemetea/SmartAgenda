using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Models
{
    public class SessionResponse : Session
    {
        public SessionResponse()
        {
        }

        public SessionResponse(Session session, Location location, IEnumerable<Person> hosts, IEnumerable<TalksResponse> talks) : base(session)
        {
            Location = location;
            Hosts = hosts;
            Talks = talks;
        }

        public virtual Location Location { get; set; }
        public virtual IEnumerable<Person> Hosts { get; set; }
        public virtual IEnumerable<TalksResponse> Talks { get; set; }



        public Session GetSession
        {
            get
            {
                return new Session
                {
                    ID = this.ID,
                    ConferenceID = this.ConferenceID,
                    Name = this.Name,
                    Start_Hour = this.Start_Hour,
                    End_Hour = this.End_Hour,
                    LocationID = this.LocationID
                };
            }
        }

        public string GetHostsBeautyfied()
        {
            if (Hosts == null || !Hosts.Any()) return "";

            string strBeautyfied = "";
            for (int i = 0; i < Hosts.Count(); i++)
            {
                strBeautyfied += Hosts.ElementAt(i).FullName;

                if (Hosts.Count() > 1 && i == (Hosts.Count() - 2))
                    strBeautyfied += " and ";

                if (Hosts.Count() > 1 && i < (Hosts.Count() - 2))
                    if (i != 0)
                        strBeautyfied += ", ";

            }

            return strBeautyfied;
        }


        public string ParseHostsString
        {
            get
            {
                if (Hosts is null) return "";


                string hosts = "";
                int i = 0;

                foreach (var person in Hosts)
                {
                    if (i == 0)
                    {
                        hosts = $"{person.FullName}";
                        i = 1;
                    }
                    else
                        hosts = $"{hosts}, {person.FullName}";

                }
                return hosts;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    List<Person> hosts = new List<Person>();
                    var hostsSplit = value.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    for (int ci = 0; ci < hostsSplit.Length; ci++)
                    {
                        var person = hostsSplit[ci];
                        var pers = person.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        string firstName = pers[0];
                        string lastName = pers.Last();
                        if (pers.Length > 2)
                            for (int i = 1; i < pers.Length - 1; i++)
                                firstName += (" " + pers[i]);

                        hosts.Add(new Person { ID = 0, First_Name = firstName, Last_Name = lastName });
                    }

                    Hosts = hosts.ToArray();
                }
            }
        }

    }
}
