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


        #region Get

        Task<IEnumerable<ConferenceResponse>> GetConferencesAsync();

        Task<SessionResponse> GetSessionAsync(int id_session);

        Task<Tag> GetTagAsync(string id_tag);

        Task<Location> GetLocationAsync(int id_location);

        Task<Person> GetPersonAsync(int id_person);

        Task<Talk> GetTalkAsync(int id_talk);

        Task<IEnumerable<string>> GetConference_TagsAsync(int id_conference);

        Task<IEnumerable<Person>> GetSession_ChairsAsync(int id_session);

        Task<IEnumerable<Person>> GetTalk_PersonsAsync(int id_talk);

        Task<IEnumerable<Tag>> GetTagsAsync();




        #region Get extra 

            Task<List<SessionResponse>> GetSessionsByConference(int id_conference);
            Task<ConferenceResponse> GetConferenceAsync(int id_conference);
            Task<IEnumerable<TalksResponse>> GetSession_TalksAsync(int id_session);

        #endregion

        #endregion






        #region POST

        Task<Tuple<bool, int>> CreateConferenceAsync(ConferenceResponse conferenceResponse);
        Task<Tuple<bool, string>> CreateTagAsync(Tag tag);
        Task<Tuple<bool, int, string>> CreateConference_TagAsync(Conference_Tags conference_Tags);

        Task<Tuple<bool, int>> CreateSessionAsync(SessionResponse sessionResponse);
        Task<Location> CreateLocationAsync(Location location);
        Task<Person> CreatePersonAsync(Person person);
        Task<Session_Chair> CreateSession_ChairAsync(Session_Chair session_Chair);

        #endregion





        #region PUT
        Task<bool> UpdateConferenceAsync(ConferenceResponse conference);
        Task<Tuple<bool, int>> UpdateSessionAsync(SessionResponse sessionResponse);// Tuple<bool, int> is used to indicate if the request was succesful or not and the error codes(int<=0) or session id
        #endregion

        #region DELETE
        Task<bool> DeleteConferenceAsync(int conference_id);
        Task<bool> DeleteSessionAsync(int session_id);

        #endregion
    }
}
