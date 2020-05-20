using DataModels;
using DataModels.Complex;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public class ApiClient : IApiClientService
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ConferenceResponse>> GetConferencesAsync()
        {
            var response = await httpClient.GetAsync("/api/Conferences");
            if (!response.IsSuccessStatusCode)
                return null;

            List<ConferenceResponse> conferences = new List<ConferenceResponse>();

            foreach (var item in await response.Content.ReadAsAsync<IEnumerable<Conference>>())
                conferences.Add(new ConferenceResponse(item, GetConference_TagsAsync(item.ID).Result));

            return conferences;
        }


        public async Task<ConferenceResponse> GetConferenceAsync(int id_conference)
        {
            var response = await httpClient.GetAsync($"/api/Conferences/{id_conference}");
            if (!response.IsSuccessStatusCode)
                return null;

            var conference = await response.Content.ReadAsAsync<Conference>();

            return new ConferenceResponse(conference, GetConference_TagsAsync(conference.ID).Result);
        }



        #region Basics

        public async Task<Tag> GetTagAsync(string id_tag)
        {
            var response = await httpClient.GetAsync($"/api/Tags/{id_tag}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsAsync<Tag>();
        }

        public async Task<IEnumerable<Tag>> GetTagsAsync()
        {
            var response = await httpClient.GetAsync("/api/Tags");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsAsync<IEnumerable<Tag>>();
        }

        public async Task<Location> GetLocationAsync(int id_location)
        {
            var response = await httpClient.GetAsync($"/api/Locations/{id_location}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsAsync<Location>();
        }

        public async Task<Person> GetPersonAsync(int id_person)
        {
            var response = await httpClient.GetAsync($"/api/Person/{id_person}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsAsync<Person>();
        }

        public async Task<Talk> GetTalkAsync(int id_talk)
        {
            var response = await httpClient.GetAsync($"/api/Talks/{id_talk}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsAsync<Talk>();
        }



        #endregion





        public async Task<SessionResponse> GetSessionAsync(int id_session)
        {
            var response = await httpClient.GetAsync($"/api/Sessions/{id_session}");
            if (!response.IsSuccessStatusCode)
                return null;

            var session = response.Content.ReadAsAsync<Session>();

            return new SessionResponse(
                session.Result,
                GetLocationAsync(session.Result.LocationID).Result,
                GetSession_ChairsAsync(id_session).Result,
                GetSession_TalksAsync(id_session).Result);
        }

        public async Task<List<SessionResponse>> GetSessionsByConference(int id_conference)
        {
            var response = await httpClient.GetAsync($"/api/Conferences/{id_conference}/Sessions");
            if (!response.IsSuccessStatusCode)
                return null;

            var sessions = response.Content.ReadAsAsync<IEnumerable<Session>>();

            if (sessions.Result is null)
                return null;

            List<SessionResponse> sessionResponses = new List<SessionResponse>();
            foreach (var item in sessions.Result)
                sessionResponses.Add(
                    new SessionResponse(
                        item,
                        GetLocationAsync(item.LocationID).Result,
                        GetSession_ChairsAsync(item.ID).Result,
                        GetSession_TalksAsync(item.ID).Result));

            return sessionResponses;
        }


        public async Task<IEnumerable<Person>> GetSession_ChairsAsync(int id_session)
        {
            var response = await httpClient.GetAsync($"/api/Session_Chair/{id_session}");
            if (!response.IsSuccessStatusCode)
                return null;

            var chairs = response.Content.ReadAsAsync<IEnumerable<Session_Chair>>();

            if (chairs.Result is null)
                return null;

            List<Person> people = new List<Person>();

            foreach (var item in chairs.Result)
                people.Add(GetPersonAsync(item.PersonID).Result);

            return people;
        }


        public async Task<IEnumerable<TalksResponse>> GetSession_TalksAsync(int id_session)
        {
            var response = await httpClient.GetAsync($"/api/Session_Talks/{id_session}");
            if (!response.IsSuccessStatusCode)
                return null;

            var sTalks = response.Content.ReadAsAsync<IEnumerable<Session_Talks>>();
            List<TalksResponse> talks = new List<TalksResponse>();

            foreach (var item in sTalks.Result)
            {
                talks.Add(new TalksResponse(
                    item,
                    GetTalkAsync(item.TalkID).Result,
                    GetTalk_PersonsAsync(item.TalkID).Result));
            }

            return talks;

        }


        public async Task<IEnumerable<Person>> GetTalk_PersonsAsync(int id_talk)
        {
            var response = await httpClient.GetAsync($"/api/Talk_Persons/{id_talk}");
            if (!response.IsSuccessStatusCode)
                return null;

            var tPers = response.Content.ReadAsAsync<IEnumerable<Talk_Persons>>();

            if (tPers.Result is null)
                return null;

            List<Person> people = new List<Person>();
            foreach (var item in tPers.Result)
                people.Add(GetPersonAsync(item.PersonID).Result);

            return people;

        }





        #region Utilities

        private async Task<IEnumerable<string>> GetConference_TagsAsync(int id_conference)
        {
            var response = await httpClient.GetAsync($"/api/Conference_Tags/{id_conference}");
            if (!response.IsSuccessStatusCode)
                return null;

            var rTags = await response.Content.ReadAsAsync<List<Conference_Tags>>();
            if (rTags.Count == 0)
                return null;

            List<string> tags = new List<string>();
            foreach (var item in rTags)
                tags.Add(item.TagID);

            return tags;

        }




        #endregion


        #region POST

        public async Task<Tuple<bool, int>> CreateConferenceAsync(ConferenceResponse conferenceResponse)
        {
            Conference converted = new Conference(conferenceResponse.ID, conferenceResponse.Name, conferenceResponse.Start_Date, conferenceResponse.End_Date);

            var response = await httpClient.PostAsJsonAsync("/api/Conferences", converted);
            if (!response.IsSuccessStatusCode)
                return new Tuple<bool, int>(false, 0);

            response.EnsureSuccessStatusCode();
            var responseDecoded = response.Content.ReadAsAsync<Conference>().Result;
            if (((List<string>)conferenceResponse.Tags).Count >= 1)
            {
                foreach (var tag in conferenceResponse.Tags)
                {
                    if (CreateTagAsync(new Tag(tag)).Result.Item1 &&
                        CreateConference_TagAsync(new Conference_Tags(responseDecoded.ID, tag)).Result.Item1)

                        return new Tuple<bool, int>(true, responseDecoded.ID);
                    else
                        return new Tuple<bool, int>(false, 0);

                }
            }

            return new Tuple<bool, int>(true, response.Content.ReadAsAsync<Conference>().Result.ID); ;

        }
        public async Task<Tuple<bool, string>> CreateTagAsync(Tag tag)
        {
            if (GetTagAsync(tag.ID).Result == null)
            {
                var response = await httpClient.PostAsJsonAsync("/api/Tags", tag);

                if (!response.IsSuccessStatusCode)
                    return new Tuple<bool, string>(false, null);

                response.EnsureSuccessStatusCode();
            }
            return new Tuple<bool, string>(true, tag.ID);
        }

        public async Task<Tuple<bool, int, string>> CreateConference_TagAsync(Conference_Tags conference_Tags)
        {

            var response = await httpClient.PostAsJsonAsync("/api/Conference_Tags", conference_Tags);

            if (!response.IsSuccessStatusCode)
                return new Tuple<bool, int, string>(false, 0, null); ;

            response.EnsureSuccessStatusCode();

            return new Tuple<bool, int, string>(true, conference_Tags.ConferenceID, conference_Tags.TagID);
        }

        #endregion


        #region PUT

        public async Task<bool> UpdateConferenceAsync(ConferenceResponse conference)
        {
            Conference converted = new Conference(conference.ID, conference.Name, conference.Start_Date, conference.End_Date);

            var response = await httpClient.PutAsJsonAsync<Conference>("/api/Conferences", converted);
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();

            if (((List<string>)conference.Tags).Count >= 1)
            {
                return UpdateConference_TagsAsync(conference.ID, conference.Tags).Result;
            }


            return true;
        }

        //keep the one already present, delete all the old tags, insert the new tags
        public async Task<bool> UpdateConference_TagsAsync(int conference_id, IEnumerable<string> tags)
        {

            var OldTags = GetConference_TagsAsync(conference_id).Result.ToList<string>();// all the present tags

            var intersectingTags = OldTags.Intersect<string>(tags, StringComparer.InvariantCultureIgnoreCase).ToList<string>();
            var uniqueOld = OldTags.Except<string>(intersectingTags, StringComparer.InvariantCultureIgnoreCase).ToList<string>();//to be deleted
            var uniqueNew = tags.Except<string>(intersectingTags, StringComparer.InvariantCultureIgnoreCase).ToList<string>();//to be added

            if (uniqueOld != null && uniqueOld.Any())// delete all the tags that are not needed anymore
            {
                await DeleteConference_TagsAsync(conference_id, uniqueOld);
            }
            if (uniqueNew != null && uniqueNew.Any())// insert all the tags that are new
            {
                foreach (var tag in uniqueNew)
                {
                    await CreateConference_TagAsync(new Conference_Tags(conference_id, tag));
                }
            }

            return true;

        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteConferenceAsync(int conference_id)
        {
            var response = await httpClient.DeleteAsync($"/api/Conferences/{conference_id}");
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();
            return true;

        }
        public async Task<bool> DeleteConference_TagsAsync(int conference_id, IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                var response = await httpClient.DeleteAsync($"/api/Conference_Tags/{conference_id}/{tag}");
                if (!response.IsSuccessStatusCode)
                    return false;

                response.EnsureSuccessStatusCode();

            }
            return true;
        }


        #endregion
    }
}
