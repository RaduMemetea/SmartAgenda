using DataModels;
using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public class ConferenceResponse : Conference
    {
        public ConferenceResponse() { }

        public ConferenceResponse(Conference conference, IEnumerable<Tag> tags) : base(conference)
        {
            Tags = tags;
        }

        public virtual IEnumerable<Tag> Tags { get; set; }


        public Conference GetConference
        {
            get
            {
                return new Conference
                {
                    ID = this.ID,
                    Name = this.Name,
                    Start_Date = this.Start_Date,
                    End_Date = this.End_Date
                };
            }
        }
        public string ParseTagString // used for convertin the Tags list into a string and the oposite
        {
            get
            {
                if (Tags is null) return "";


                string tags = "";
                int i = 0;
                
                foreach (var tag in this.Tags)
                {
                    if (i == 0)
                    {
                        tags = $"{tag.ID}";
                        i = 1;
                    }
                    else
                        tags = $"{tags}, {tag.ID}";

                }
                return tags;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    List<Tag> tags = new List<Tag>();
                    var tagsSplit = value.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                    foreach (var tag in tagsSplit)
                        tags.Add(new Tag(tag));

                    this.Tags = tags.ToArray();
                }
            }
        }


    }
}
