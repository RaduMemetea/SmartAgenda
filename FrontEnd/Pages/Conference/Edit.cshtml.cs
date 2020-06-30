using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FrontEnd.Pages.Conference
{
    [Authorize]
    public class EditModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService identityClient { get; }
        [BindProperty]
        public ConferenceResponse Conference { get; set; }

        [BindProperty]
        public string tagList { get; set; }


        public EditModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            identityClient = apiIdentityService;
        }

        public IActionResult OnGet(int? conference_id)
        {
            if (conference_id == null)
                return NotFound();

            if (identityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = conference_id.Value }).Result == null)
                return NotFound();


            Conference = ApiClient.GetConferenceAsync(conference_id.Value).Result;

            if (Conference == null)
                return NotFound();


            if (Conference.Tags != null && Conference.Tags.Any())
                tagList = Conference.ParseTagString;

            return Page();
        }

        public IActionResult OnPost(int? conference_id)
        {
            if (!ModelState.IsValid)
                return Page();

            if (conference_id == null)
                return RedirectToPage("/Error");

            Conference.ID = conference_id.Value;
            Conference.ParseTagString = tagList;

            var result = ApiClient.UpdateConferenceResponseAsync(Conference).Result;
            if (result.Item1 == false || result.Item2 <= 0)
                return RedirectToPage("/Error");

            return RedirectToPage($"/Conference/Index", new { conference_id });
        }
    }
}