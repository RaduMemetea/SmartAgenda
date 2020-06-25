using DataModels;
using DataModels.Complex;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public interface IApiClientService
    {







        Task<IEnumerable<Tag>> GetConference_TagsAsync(int id_conference);

        Task<IEnumerable<Person>> GetSession_HostsAsync(int id_session);
        Task<IEnumerable<Session_Talks>> GetSession_TalksAsync(int id_session, int id_talk = 0);

        Task<IEnumerable<Person>> GetTalk_PersonsAsync(int id_talk);




        #region POST

        Task<Tuple<bool, int, string>> CreateConference_TagAsync(int id_conference, Tag tag);

        Task<Session_Host> CreateSession_HostAsync(int session_id, int person_id);


        Task<Session_Talks> CreateSession_TalksAsync(Session_Talks session_Talk);
        Task<Talk_Persons> CreateTalk_PersonsAsync(int talkID, Person person);


        #endregion



        Task<bool> UpdateSession_TalksAsync(Session_Talks session_Talks);
        Task<bool> UpdateTalk_PersonsAsync(int id_talk, IEnumerable<Person> persons);


        Task<bool> DeleteTalk_Person(int id_talk, int id_person);
        Task<bool> DeleteSession_HostAsync(int id_session, int id_person);


        #region Conference



        #region Get

        Task<IEnumerable<ConferenceResponse>> GetConferencesAsync();
        Task<ConferenceResponse> GetConferenceAsync(int id_conference);

        #endregion


        #region Post

        Task<Tuple<bool, int>> CreateConferenceResponseAsync(ConferenceResponse conferenceResponse);

        #endregion


        #region Put

        Task<Tuple<bool, int>> UpdateConferenceResponseAsync(ConferenceResponse conference);

        #endregion


        #region Delete

        Task<bool> DeleteConferenceAsync(int id_conference);

        #endregion



        #endregion


        #region Session



        #region Get
        Task<SessionResponse> GetSessionResponseAsync(int id_session);
        Task<IEnumerable<SessionResponse>> GetSessionsByConference(int id_conference);
        Task<Session> GetSessionAsync(int id_session);

        #endregion


        #region Post
        Task<Tuple<bool, int>> CreateSessionResponseAsync(SessionResponse sessionResponse);
        Task<Session> CreateSessionAsync(Session session);

        #endregion


        #region Put
        Task<Tuple<bool, int>> UpdateSessionResponseAsync(SessionResponse sessionResponse);// Tuple<bool, int> is used to indicate if the request was succesful or not and the error codes(int<=0) or session id

        #endregion


        #region Delete
        Task<bool> DeleteSessionAsync(int id_session);

        #endregion



        #endregion



        #region Talk



        #region Get
        Task<Talk> GetTalkAsync(int id_talk);
        Task<TalksResponse> GetTalkResponseAsync(int id_talk);

        Task<int> GetConferenceFromTalkID(int id_talk);
        #endregion


        #region Post
        Task<Talk> CreateTalkAsync(Talk talk);
        Task<Tuple<bool, int>> CreateTalkResponseAsync(TalksResponse talksResponse);

        #endregion


        #region Put
        Task<Tuple<bool, int>> UpdateTalkResponseAsync(TalksResponse talk);
        Task<bool> UpdateTalkAsync(Talk talk);

        #endregion


        #region Delete
        Task<bool> DeleteTalkAsync(int id_talk);

        #endregion



        #endregion


        #region Tag



        #region Get
        Task<Tag> GetTagAsync(string id_tag);
        Task<IEnumerable<Tag>> GetTagsAsync();

        #endregion


        #region Post
        Task<Tuple<bool, string>> CreateTagAsync(Tag tag);

        #endregion


        #region Put

        #endregion


        #region Delete

        #endregion



        #endregion


        #region Person



        #region Get
        Task<Person> GetPersonAsync(int id_person);

        #endregion


        #region Post
        Task<Person> CreatePersonAsync(Person person);

        #endregion



        #endregion


        #region Location



        #region Get
        Task<Location> GetLocationAsync(int id_location);

        #endregion


        #region Post
        Task<Location> CreateLocationAsync(Location location);

        #endregion


        #endregion




        #region Example



        #region Get

        #endregion


        #region Post

        #endregion


        #region Put

        #endregion


        #region Delete

        #endregion



        #endregion

    }
}
