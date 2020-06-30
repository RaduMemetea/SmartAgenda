using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.MyAgenda
{
    [Authorize]
    public class AddModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        public AddModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }


        public async void OnGetAsync(int? talkID)
        {
            if (talkID is null) return;

            var talk = ApiClient.GetTalkResponseAsync(talkID.Value);

            if (talk is null) return;


            var response = await IdentityClient.AddUserAgendaAsync(new UserAgenda
            {
                UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value,
                ConferenceId = ApiClient.GetConferenceFromTalkID(talkID.Value).Result,
                SessionId = talk.Result.SessionID,
                TalkId = talk.Result.ID

            });

            if (response is null) return;

        }
    }
}