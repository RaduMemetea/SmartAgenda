using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FrontEnd.Pages.Conference
{
    public class DeleteModel : PageModel
    {
        public IApiClientService ApiClient { get; }

        [BindProperty]
        public ConferenceResponse Conference { get; set; }

        public DeleteModel(IApiClientService apiClientService)
        {
            ApiClient = apiClientService;
        }

        public async Task<IActionResult> OnGetAsync(int? conference_id)
        {
            if (conference_id == null)
            {
                return NotFound();
            }


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


            return RedirectToPage("/Index");

        }
    }
}
