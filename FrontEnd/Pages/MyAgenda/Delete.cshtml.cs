using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.MyAgenda
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        public DeleteModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }


        public async void OnGetAsync(int? talkID)
        {

            if (talkID is null) return;

            var talk = ApiClient.GetTalkResponseAsync(talkID.Value);

            if (talk is null) return;


            var entry = IdentityClient.GetUserAgendaAsync(new UserAgenda
            {
                UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value,
                ConferenceId = ApiClient.GetConferenceFromTalkID(talkID.Value).Result,
                SessionId = talk.Result.SessionID,
                TalkId = talk.Result.ID
            });

            if (entry == null) return;

            await IdentityClient.DeleteUserAgenda(entry.Result.UserId, entry.Result.ConferenceId, entry.Result.SessionId, entry.Result.TalkId);
            return;
        }
    }
}