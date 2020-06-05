using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Models
{
    public class ConferenceResponse : Conference
    {
        public ConferenceResponse() { }

        public ConferenceResponse(Conference conference, IEnumerable<string> tags) : base(conference)
        {
            Tags = tags;
        }

        public virtual IEnumerable<string> Tags { get; set; }

    }
}
