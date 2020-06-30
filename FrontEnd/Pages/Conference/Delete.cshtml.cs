using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FrontEnd.Pages.Conference
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        [BindProperty]
        public ConferenceResponse Conference { get; set; }

        public DeleteModel(IApiClientService apiClientService, IApiIdentityService identityService)
        {
            ApiClient = apiClientService;
            IdentityClient = identityService;
        }

        public async Task<IActionResult> OnGetAsync(int? conference_id)
        {
            if (conference_id == null)
            {
                return NotFound();
            }

            if (IdentityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = conference_id.Value }).Result == null)
                return NotFound();

            Conference = ApiClient.GetConferenceAsync(conference_id.Value).Result;

            if (Conference == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? conference_id)
        {
            if (conference_id == null)
            {
                return NotFound();
            }

            var result = ApiClient.DeleteConferenceAsync(conference_id.Value).Result;
            if (result == false)
                return RedirectToPage("/Error");

            await IdentityClient.DeleteUserOwnership(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value,  conference_id.Value);


            return RedirectToPage("/Index");

        }
    }
}
