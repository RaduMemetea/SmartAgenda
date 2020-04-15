using DataModels;
using System.Collections.Generic;

namespace FrontEnd.Pages.Models
{
    public class SessionResponse : Session
    {
        public SessionResponse() { }

        public SessionResponse(Session session, Location location, IEnumerable<Person> chairs, IEnumerable<TalksResponse> talks) : base(session)
        {
            Location = location;
            Chairs = chairs;
            Talks = talks;
        }

        public virtual Location Location { get; set; }
        public virtual IEnumerable<Person> Chairs { get; set; }
        public virtual IEnumerable<TalksResponse> Talks { get; set; }

    }
}
