using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FrontEnd.Pages.MyAgenda
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }

        public List<AgendaEntry> Entry { get; set; }

        public IndexModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var events = IdentityClient.GetUserAgendaAsync(id).Result;

            if (events == null || !events.Any()) return Page();

            Entry = new List<AgendaEntry>();

            var l = events.GroupBy(x => x.ConferenceId);

            foreach (var conference in events.GroupBy(x=> x.ConferenceId))
            {
                var conf = ApiClient.GetConferenceAsync(conference.Key).Result.GetConference;
                List<SessionResponse> sessions = new List<SessionResponse>();
                foreach (var session in conference.GroupBy(x => x.SessionId))
                {
                    var ses = ApiClient.GetSessionResponseAsync(session.Key).Result;
                    List<TalksResponse> tks= new List<TalksResponse>();

                    foreach (var talk in session)
                        tks.Add(ApiClient.GetTalkResponseAsync(talk.TalkId).Result);

                    ses.Talks = tks.ToArray();
                    sessions.Add(ses);
                }

                Entry.Add(new AgendaEntry
                {
                    Conference =conf,
                    Sessions = sessions
                });
                

            }


            return Page();
        }
    }
}