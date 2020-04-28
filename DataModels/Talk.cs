using System;

namespace DataModels
{
    public class Talk
    {
        public Talk()
        {
        }

        public Talk(Talk talk)
        {
            ID = talk.ID;
            Name = talk.Name;
            Abstract = talk.Abstract;
        }

        public int ID { get; set; }
        public String Name { get; set; }
        public String Abstract { get; set; }
    }
}
