using System;

namespace DataModels
{
    public class Tag
    {
        public Tag() { }

        public Tag(string id, string description = "")
        {
            ID = id;
            Description = description;
        }

        public String ID { get; set; }
        public String Description { get; set; }
    }
}
