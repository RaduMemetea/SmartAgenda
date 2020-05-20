using System;

namespace DataModels
{
    public class Tag
    {
        public Tag(string iD, string description="")
        {
            ID = iD;
            Description = description;
        }

        public String ID { get; set; }
        public String Description { get; set; }
    }
}
