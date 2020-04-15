using System;

namespace DataModels.Complex
{
    public class Session_Talks
    {
        public Session_Talks() { }

        public Session_Talks(Session_Talks session_Talks)
        {
            SessionID = session_Talks.SessionID;
            TalkID = session_Talks.TalkID;
            Hour = session_Talks.Hour;
            Highlight = session_Talks.Highlight;
        }

        public int SessionID { get; set; }
        public int TalkID { get; set; }

        public DateTime Hour { get; set; }


        //TODO make a feature to highlight the talk in the session
        public bool Highlight { get; set; }// null/false == ignore, true == make it stand out in session

    }
}
