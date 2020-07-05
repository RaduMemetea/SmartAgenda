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




        public async Task<ConferenceResponse> GetConferenceAsync(int id_conference)
        {
            var response = await httpClient.GetAsync($"/api/Conferences/{id_conference}");
            if (!response.IsSuccessStatusCode)
                return null;

            var conference = await response.Content.ReadAsAsync<Conference>();

            return new ConferenceResponse(conference, GetConference_TagsAsync(conference.ID).Result);
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

        public async Task<Session> GetSessionAsync(int id_session)
        {
            var response = await httpClient.GetAsync($"/api/Sessions/{id_session}");
            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<Session>().Result;
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


        public async Task<SessionResponse> GetSessionResponseAsync(int id_session)
        {
            var response = await httpClient.GetAsync($"/api/Sessions/{id_session}");
            if (!response.IsSuccessStatusCode)
                return null;

            var session = response.Content.ReadAsAsync<Session>();

            var sesionTalks = GetSession_TalksAsync(id_session).Result;
            List<TalksResponse> talks = null;
            if (sesionTalks != null && sesionTalks.Any())
            {
                talks = new List<TalksResponse>();
                foreach (var item in sesionTalks)
                {
                    talks.Add(new TalksResponse(
                        GetTalkAsync(item.TalkID).Result,
                        item.SessionID,
                        item.Hour,
                        item.Highlight,
                        GetTalk_PersonsAsync(item.TalkID).Result
                        ));

                }
            }

            return new SessionResponse(
                session.Result,
                GetLocationAsync(session.Result.LocationID).Result,
                GetSession_HostsAsync(id_session).Result,
                talks
                );
        }


        public async Task<IEnumerable<SessionResponse>> GetSessionsByConference(int id_conference)
        {
            var response = await httpClient.GetAsync($"/api/Conferences/{id_conference}/Sessions");
            if (!response.IsSuccessStatusCode)
                return null;

            var sessions = response.Content.ReadAsAsync<IEnumerable<Session>>();

            if (sessions.Result is null)
                return null;

            List<SessionResponse> sessionResponses = new List<SessionResponse>();
            foreach (var session in sessions.Result)
                sessionResponses.Add(GetSessionResponseAsync(session.ID).Result);

            return sessionResponses;
        }


        public async Task<IEnumerable<Person>> GetSession_HostsAsync(int id_session)
        {
            var response = await httpClient.GetAsync($"/api/Session_Hosts/{id_session}");
            if (!response.IsSuccessStatusCode)
                return null;

            var Hosts = response.Content.ReadAsAsync<IEnumerable<Session_Host>>();

            if (Hosts.Result is null)
                return null;

            List<Person> people = new List<Person>();

            foreach (var item in Hosts.Result)
                people.Add(GetPersonAsync(item.PersonID).Result);

            return people;
        }


        public async Task<IEnumerable<Session_Talks>> GetSession_TalksAsync(int id_session, int id_talk = 0)
        {


            var response = await httpClient.GetAsync($"/api/Session_Talks/{id_session}/{id_talk}");
            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<IEnumerable<Session_Talks>>().Result;

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


        public async Task<IEnumerable<Tag>> GetConference_TagsAsync(int id_conference)
        {
            var response = await httpClient.GetAsync($"/api/Conference_Tags/{id_conference}");
            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<IEnumerable<Conference_Tags>>().Result.Select(tag => GetTagAsync(tag.TagID).Result);

        }

        public async Task<TalksResponse> GetTalkResponseAsync(int id_talk)
        {
            var talk = GetTalkAsync(id_talk).Result;
            if (talk is null)
                return null;

            var session = GetSession_TalksAsync(0, id_talk).Result.FirstOrDefault();
            if (session is null)
                return null;

            var persons = GetTalk_PersonsAsync(id_talk).Result;
            if (persons is null)
                return null;


            return new TalksResponse(talk, session.SessionID, session.Hour, session.Highlight, persons);

        }

        public async Task<int> GetConferenceFromTalkID(int id_talk)
        {
            var SessionId = GetSession_TalksAsync(0, id_talk).Result.Where(st => st.TalkID == id_talk).FirstOrDefault().SessionID;
            var ConferenceID = GetSessionAsync(SessionId).Result.ConferenceID;

            return ConferenceID;
        }



        #region POST


        public async Task<Tuple<bool, int>> CreateConferenceResponseAsync(ConferenceResponse conferenceResponse)
        {
            var converted = conferenceResponse.GetConference;

            var response = CreateConferenceAsync(converted).Result;
            if (response == null)
                return new Tuple<bool, int>(false, 0);


            if (conferenceResponse.Tags != null && conferenceResponse.Tags.Any())
                foreach (var tag in conferenceResponse.Tags)
                    if (!CreateConference_TagAsync(response.ID, tag).Result.Item1)
                        return new Tuple<bool, int>(false, 0);


            return new Tuple<bool, int>(true, response.ID); ;

        }

        public async Task<Conference> CreateConferenceAsync(Conference conference)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Conferences", conference);
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Conference>().Result;

        }


        public async Task<Tuple<bool, string>> CreateTagAsync(Tag tag)
        {
            if (tag.ID == null || GetTagAsync(tag.ID).Result == null)
            {
                var response = await httpClient.PostAsJsonAsync("/api/Tags", tag);

                if (!response.IsSuccessStatusCode)
                    return new Tuple<bool, string>(false, null);

                response.EnsureSuccessStatusCode();
            }

            return new Tuple<bool, string>(true, tag.ID);
        }


        public async Task<Tuple<bool, int, string>> CreateConference_TagAsync(int conference_id, Tag tag)
        {
            var tagResponse = CreateTagAsync(tag).Result;
            var response = await httpClient.PostAsJsonAsync("/api/Conference_Tags", new Conference_Tags { ConferenceID = conference_id, TagID = tagResponse.Item2 });

            if (!response.IsSuccessStatusCode)
                return new Tuple<bool, int, string>(false, 0, null); ;

            response.EnsureSuccessStatusCode();

            return new Tuple<bool, int, string>(true, conference_id, tagResponse.Item2);
        }

        public async Task<Tuple<bool, int>> CreateSessionResponseAsync(SessionResponse sessionResponse)
        {
            var converted = sessionResponse.GetSession;

            var location = CreateLocationAsync(sessionResponse.Location).Result;
            converted.LocationID = location != null ? location.ID : 0;              // if the location is not null then initialize with the response id otherwise 0

            if (converted.LocationID == 0)
                return new Tuple<bool, int>(false, -1);

            var response = CreateSessionAsync(converted).Result;
            if (response == null)
                return new Tuple<bool, int>(false, 0);

            converted.ID = sessionResponse.ID = response.ID;

            if (sessionResponse.Hosts != null && sessionResponse.Hosts.Any())
                foreach (var Host in sessionResponse.Hosts)
                {
                    var HostsResponse = CreatePersonAsync(Host).Result != null ? CreatePersonAsync(Host).Result.ID : 0;
                    if (HostsResponse == 0)
                        return new Tuple<bool, int>(false, -2);

                    await CreateSession_HostAsync(converted.ID, HostsResponse);
                }

            return new Tuple<bool, int>(true, converted.ID);


        }


        public async Task<Tuple<bool, int>> CreateTalkResponseAsync(TalksResponse talkResponse)
        {
            Talk converted = talkResponse.GetTalk;

            var response = CreateTalkAsync(converted).Result;
            if (response is null)
                return new Tuple<bool, int>(false, 0);

            converted.ID = talkResponse.ID = response.ID;

            var talk_sessionResponse = CreateSession_TalksAsync(talkResponse.GetSession_Talk).Result;
            if (talk_sessionResponse == null)
                return new Tuple<bool, int>(false, -1);

            if (talkResponse.Persons != null && talkResponse.Persons.Any())
            {
                List<Person> talk_Persons = talkResponse.Persons.ToList();

                if (talk_Persons != null && talk_Persons.Any())
                    foreach (var person in talk_Persons)
                    {
                        var personResponse = CreateTalk_PersonsAsync(converted.ID, person).Result;

                        if (personResponse == null)
                            return new Tuple<bool, int>(false, -2);

                    }
            }
            return new Tuple<bool, int>(true, converted.ID);

        }


        public async Task<Location> CreateLocationAsync(Location location)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Locations", location);
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Location>().Result;
        }
        public async Task<Person> CreatePersonAsync(Person person)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Person", person);
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Person>().Result;
        }

        public async Task<Session_Host> CreateSession_HostAsync(int session_id, int person_id)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Session_Hosts", new Session_Host { SessionID = session_id, PersonID = person_id });
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Session_Host>().Result;

        }

        public async Task<Session_Talks> CreateSession_TalksAsync(Session_Talks session_Talk)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Session_Talks", session_Talk);
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Session_Talks>().Result;

        }
        public async Task<Talk> CreateTalkAsync(Talk talk)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Talks", talk);
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Talk>().Result;

        }
        public async Task<Talk_Persons> CreateTalk_PersonsAsync(int talkID, Person person)
        {

            if (person.ID == 0)
            {
                var personResponse = CreatePersonAsync(person).Result;
                if (personResponse == null)
                    return null;
                person.ID = personResponse.ID;
            }

            var response = await httpClient.PostAsJsonAsync("/api/Talk_Persons", new Talk_Persons { TalkID = talkID, PersonID = person.ID });
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Talk_Persons>().Result;

        }
        public async Task<Session> CreateSessionAsync(Session session)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Sessions", session);
            if (!response.IsSuccessStatusCode)
                return null;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Session>().Result;

        }



        #endregion




        #region PUT

        public async Task<Tuple<bool, int>> UpdateConferenceResponseAsync(ConferenceResponse conference)
        {
            var converted = conference.GetConference;

            var response = await httpClient.PutAsJsonAsync($"/api/Conferences/{conference.ID}", converted);
            if (!response.IsSuccessStatusCode)
                return new Tuple<bool, int>(false, 0);

            response.EnsureSuccessStatusCode();

            if (conference.Tags != null && conference.Tags.Any())
            {
                var result = UpdateConference_TagsAsync(conference.ID, conference.Tags).Result;
                if (result == false)
                    return new Tuple<bool, int>(false, -1);
            }


            return new Tuple<bool, int>(true, conference.ID);
        }

        //keep the ones already present, delete all the old tags, insert the new tags
        public async Task<bool> UpdateConference_TagsAsync(int conference_id, IEnumerable<Tag> tags)
        {
            var tagCall = GetConference_TagsAsync(conference_id).Result;
            List<string> OldTags;// all the present tags

            if (tagCall != null)
                OldTags = tagCall.Select(t => t.ID).ToList();
            else
            {
                if (tags != null && tags.Any())// insert all the tags that are new
                    foreach (var tag in tags)
                        await CreateConference_TagAsync(conference_id, tag);
                return true;
            }

            var intersectingTags = OldTags.Intersect<string>(tags.Select(t => t.ID).ToList(), StringComparer.InvariantCultureIgnoreCase).ToList<string>();
            var uniqueOld = OldTags.Except<string>(intersectingTags, StringComparer.InvariantCultureIgnoreCase).ToList<string>();//to be deleted
            var uniqueNew = tags.Select(t => t.ID).ToList().Except<string>(intersectingTags, StringComparer.InvariantCultureIgnoreCase).ToList<string>();//to be added

            if (uniqueOld != null && uniqueOld.Any())// delete all the tags that are not needed anymore
                await DeleteConference_TagsAsync(conference_id, uniqueOld);

            if (uniqueNew != null && uniqueNew.Any())// insert all the tags that are new
                foreach (var tag in uniqueNew)
                    await CreateConference_TagAsync(conference_id, new Tag(tag));

            return true;

        }

        public async Task<Tuple<bool, int>> UpdateSessionResponseAsync(SessionResponse sessionResponse)
        {
            var converted = sessionResponse.GetSession;

            var location = CreateLocationAsync(sessionResponse.Location).Result;
            converted.LocationID = location != null ? location.ID : 0;              // if the location is not null then initialize with the response id otherwise 0

            if (converted.LocationID == 0)
                return new Tuple<bool, int>(false, -1);

            var response = await httpClient.PutAsJsonAsync($"/api/Sessions/{sessionResponse.ID}", converted);
            if (!response.IsSuccessStatusCode)
                return new Tuple<bool, int>(false, 0);

            if (sessionResponse.Hosts != null && sessionResponse.Hosts.Any())
            {
                var SCresponse = httpClient.GetAsync($"/api/Session_Hosts/{sessionResponse.ID}/0");
                if (SCresponse.Result.IsSuccessStatusCode)
                {
                    var scList = SCresponse.Result.Content.ReadAsAsync<IEnumerable<Session_Host>>().Result;
                    foreach (var item in scList)
                        DeleteSession_HostAsync(sessionResponse.ID, item.PersonID);
                }

                foreach (var Host in sessionResponse.Hosts)
                {
                    var HostsResponse = CreatePersonAsync(Host).Result != null ? CreatePersonAsync(Host).Result.ID : 0;
                    if (HostsResponse == 0)
                        return new Tuple<bool, int>(false, -2);

                    await CreateSession_HostAsync(converted.ID, HostsResponse);
                }

            }
            return new Tuple<bool, int>(true, converted.ID);


        }


        public async Task<Tuple<bool, int>> UpdateTalkResponseAsync(TalksResponse talk)
        {
            var converted = talk.GetTalk;
            var response = UpdateTalkAsync(converted).Result;

            if (response == false)
                return new Tuple<bool, int>(false, 0);

            var STresponse = UpdateSession_TalksAsync(talk.GetSession_Talk).Result;
            if (STresponse == false)
                return new Tuple<bool, int>(false, -1);

            var TPresponse = UpdateTalk_PersonsAsync(talk.ID, talk.Persons).Result;
            if (STresponse == false)
                return new Tuple<bool, int>(false, -2);

            return new Tuple<bool, int>(true, talk.ID);

        }

        public async Task<bool> UpdateTalkAsync(Talk talk)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Talks/{talk.ID}", talk);
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<bool> UpdateSession_TalksAsync(Session_Talks session_Talks)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Session_Talks/{session_Talks.SessionID}/{session_Talks.TalkID}", session_Talks);
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<bool> UpdateTalk_PersonsAsync(int id_talk, IEnumerable<Person> persons)
        {

            var OldData = GetTalk_PersonsAsync(id_talk).Result;

            foreach (var person in OldData)
            {
                var DTPrespones = DeleteTalk_Person(id_talk, person.ID).Result;
                if (DTPrespones == false)
                    return false;
            }

            foreach (var person in persons)
            {
                var per = CreatePersonAsync(person).Result;

                var CTPresponse = CreateTalk_PersonsAsync(id_talk, per).Result;
                if (CTPresponse is null)
                    return false;
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

        public async Task<bool> DeleteSessionAsync(int session_id)
        {
            var response = await httpClient.DeleteAsync($"/api/Sessions/{session_id}");
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();
            return true;

        }

        public async Task<bool> DeleteTalkAsync(int id_talk)
        {
            var response = await httpClient.DeleteAsync($"/api/Talks/{id_talk}");
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();
            return true;
        }
        public async Task<bool> DeleteTalk_Person(int id_talk, int id_person)
        {
            var response = await httpClient.DeleteAsync($"/api/Talk_Persons/{id_talk}/{id_person}");
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();
            return true;

        }

        public async Task<bool> DeleteSession_HostAsync(int id_session, int id_person)
        {
            var response = await httpClient.DeleteAsync($"/api/Session_Hosts/{id_session}/{id_person}");
            if (!response.IsSuccessStatusCode)
                return false;

            response.EnsureSuccessStatusCode();
            return true;

        }

        #endregion
    }
}
