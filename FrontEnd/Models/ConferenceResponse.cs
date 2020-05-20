using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Models
{
    public class ConferenceResponse : Conference
    {
        public ConferenceResponse() { }

        public ConferenceResponse(Conference conference, IEnumerable<string> tag_ids) : base(conference)
        {
            Tags = tag_ids;
        }

        public virtual IEnumerable<string> Tags { get; set; }

    }
}
