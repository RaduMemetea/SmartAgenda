using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.MyAgenda
{
    public class IndexModel : PageModel
    {
        public IApiClientService ApiClient { get; }

        public List<Tuple<DataModels.Conference, SessionResponse, TalksResponse>> Entry { get; set; }

        public IndexModel(IApiClientService apiClientService)
        {
            ApiClient = apiClientService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Entry = new List<Tuple<DataModels.Conference, SessionResponse, TalksResponse>>();
            Entry.Add(new Tuple<DataModels.Conference, SessionResponse, TalksResponse>(
                    ApiClient.GetConferenceAsync(1).Result,
                    ApiClient.GetSessionResponseAsync(3).Result,
                    ApiClient.GetTalkResponseAsync(18).Result
            ));

            Entry.Add(new Tuple<DataModels.Conference, SessionResponse, TalksResponse>(
                    ApiClient.GetConferenceAsync(1).Result,
                    ApiClient.GetSessionResponseAsync(5).Result,
                    ApiClient.GetTalkResponseAsync(21).Result
            ));

            Entry.Add(new Tuple<DataModels.Conference, SessionResponse, TalksResponse>(
                    ApiClient.GetConferenceAsync(30).Result,
                    ApiClient.GetSessionResponseAsync(44).Result,
                    null
            ));
            return Page();
        }
    }
}