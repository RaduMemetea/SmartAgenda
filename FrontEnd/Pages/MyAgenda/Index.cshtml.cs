using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.MyAgenda
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IApiClientService apiClient { get; }
        public IApiIdentityService identityClient{ get; }

        public List<Tuple<DataModels.Conference, SessionResponse, TalksResponse>> Entry { get; set; }

        public IndexModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            apiClient = apiClientService;
            identityClient = apiIdentityService;
        }

        public async Task<IActionResult> OnGetAsync()
        {

          //var result = await identityClient.AddUserOwnershipAsync(new UserOwnership { UserId = "a7d3d8d9-0a37-404b-ae30-b5ed964da29c", ConferenceId = 1 });
            var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            Entry = new List<Tuple<DataModels.Conference, SessionResponse, TalksResponse>>();
            Entry.Add(new Tuple<DataModels.Conference, SessionResponse, TalksResponse>(
                    apiClient.GetConferenceAsync(1).Result,
                    apiClient.GetSessionResponseAsync(3).Result,
                    apiClient.GetTalkResponseAsync(18).Result
            ));

            Entry.Add(new Tuple<DataModels.Conference, SessionResponse, TalksResponse>(
                    apiClient.GetConferenceAsync(1).Result,
                    apiClient.GetSessionResponseAsync(5).Result,
                    apiClient.GetTalkResponseAsync(21).Result
            ));

            Entry.Add(new Tuple<DataModels.Conference, SessionResponse, TalksResponse>(
                    apiClient.GetConferenceAsync(30).Result,
                    apiClient.GetSessionResponseAsync(44).Result,
                    null
            ));
            return Page();
        }
    }
}