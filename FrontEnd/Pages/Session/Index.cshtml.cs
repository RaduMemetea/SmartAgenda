using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Pages.Session
{
    public class IndexModel : PageModel
    {

        public IndexModel(IApiClientService apiClient)
        {
            ApiClient = ApiClient;
        }

        public IList<SessionResponse> SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }

        public async Task OnGetAsync()
        {
          
        }
    }
}
