using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using FrontEnd.Models;

namespace FrontEnd.Pages.Session
{
    public class DetailsModel : PageModel
    {

        public SessionResponse SessionResponse { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

       

            if (SessionResponse == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
