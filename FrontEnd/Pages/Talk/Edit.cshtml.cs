using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FrontEnd.Pages.Talk
{
    public class EditModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        [BindProperty]
        public TalksResponse Talk { get; set; }

        [BindProperty]
        public string personList { get; set; }



        public EditModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }

        public IActionResult OnGet(int? talk_id)
        {
            if (talk_id == null)
                return NotFound();

            Talk = ApiClient.GetTalkResponseAsync(talk_id.Value).Result;

            if (Talk == null)
                return NotFound();

            if (IdentityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = ApiClient.GetConferenceFromTalkID(Talk.ID).Result }).Result == null)
                return base.NotFound();

            personList = Talk.ParsePersonsString;

            return Page();
        }


        public IActionResult OnPost(int talk_id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oldTalk = ApiClient.GetTalkResponseAsync(talk_id).Result;

            if (oldTalk == null)
                return RedirectToPage("/Error");


            Talk.ID = oldTalk.ID;
            Talk.SessionID = oldTalk.SessionID;
            Talk.ParsePersonsString = personList;


            var response = ApiClient.UpdateTalkResponseAsync(Talk);
            if (response.Result.Item1 == false || response.Result.Item2 <= 0)
                return RedirectToPage("/Error");

            return RedirectToPage("/Conference/Index", new { conference_id = ApiClient.GetConferenceFromTalkID(response.Result.Item2).Result });
        }



    }
}
