using DataModels;
using FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public interface IApiClientService
    {
        Task<IEnumerable<ConferenceResponse>> GetConferencesAsync();
        Task<SessionResponse> GetSessionAsync(int id_session);

        Task<List<SessionResponse>> GetSessionsByConference(int id_conference);
        Task<ConferenceResponse> GetConferenceAsync(int id_conference);

        #region Utilities

        //Task<IEnumerable<string>> GetConference_TagsAsync(int id_conference);
        Task<IEnumerable<Person>> GetSession_ChairsAsync(int id_session);
        Task<IEnumerable<TalksResponse>> GetSession_TalksAsync(int id_session);
        Task<IEnumerable<Person>> GetTalk_PersonsAsync(int id_talk);

        #endregion


        #region Base

        Task<Tag> GetTagAsync(string id_tag);
        Task<Location> GetLocationAsync(int id_location);
        Task<Person> GetPersonAsync(int id_person);
        Task<Talk> GetTalkAsync(int id_talk);

        #endregion 
        Task<IEnumerable<Tag>> GetTagsAsync();




        #region POST

        Task<int> CreateConferenceAsync(ConferenceResponse conferenceResponse);

        #endregion



        #region DELETE
        Task<bool> DeleteConferenceAsync(int conference_id);

        #endregion
    }
}
