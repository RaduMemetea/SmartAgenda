using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Talk
{
    [Authorize]
    public class AddModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        [BindProperty]
        public TalksResponse Talk { get; set; }

        public DataModels.Session Session { get; set; }

        [BindProperty]
        public string personList { get; set; }

        public int ConferenceID { get; set; }


        public AddModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }

        public IActionResult OnGet(int? session_id)
        {
            if (session_id == null)
                return NotFound();

            Session = ApiClient.GetSessionAsync(session_id.Value).Result;

            if(Session is null)
                return NotFound();

            if (IdentityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = Session.ConferenceID }).Result == null)
                return NotFound();

            return Page();
        }


        public IActionResult OnPost(int session_id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            Talk.ID = 0;
            Talk.SessionID = session_id;
            Talk.ParsePersonsString = personList;


            var response = ApiClient.CreateTalkResponseAsync(Talk);
            if (response.Result.Item1 == false || response.Result.Item2 <= 0)
                return RedirectToPage("/Error");



            return RedirectToPage("/Conference/Index", new { conference_id = ApiClient.GetConferenceFromTalkID(response.Result.Item2).Result });
        }

    }
}
