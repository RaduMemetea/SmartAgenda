using DataModels;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Pages.Session
{
    public class EditModel : PageModel
    {


        [BindProperty]
        public SessionResponse SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }
        public ConferenceResponse Conference { get; private set; }
        [BindProperty]
        public string chairsList { get; set; } = "";


        public EditModel(IApiClientService apiClient)
        {
            ApiClient = apiClient;
        }


        public async Task<IActionResult> OnGetAsync(int? session_id)
        {
            if (session_id == null)
            {
                return NotFound();
            }

            SessionResponse = ApiClient.GetSessionAsync(session_id.Value).Result;

            Conference = ApiClient.GetConferenceAsync(SessionResponse.ConferenceID).Result;

            if (SessionResponse.Chairs != null && SessionResponse.Chairs.Any())
            {
                int i = 0;
                foreach (var person in SessionResponse.Chairs)
                    if (i == 0)// remove the case when the list beggins with ", {person}"
                    {
                        chairsList += $"{person.GetFullName}";
                        i = 1;
                    }
                    else
                        chairsList = $"{chairsList}, {person.GetFullName}";

            }

            if (SessionResponse == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? session_id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (session_id == null)
            {
                return RedirectToPage("/Error");
            }

            var session = ApiClient.GetSessionAsync(session_id.Value).Result;
            SessionResponse.ID = session.ID;
            SessionResponse.ConferenceID = session.ConferenceID;
            SessionResponse.Location.ID = 0; // remove the case when someone update a location and it changes N other entities


            if (chairsList != null && chairsList.Length > 0)
            {
                List<Person> chairs = new List<Person>();
                var chairsSplit = chairsList.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                for (int ci = 0; ci < chairsSplit.Length; ci++)
                {
                    var person = chairsSplit[ci];
                    var pers = person.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string firstName = pers[0];
                    string lastName = pers.Last();
                    if (pers.Length > 2)
                        for (int i = 1; i < pers.Length - 1; i++)
                            firstName += (" " + pers[i]);

                    chairs.Add(new Person { ID = 0, First_Name = firstName, Last_Name = lastName });
                }

                SessionResponse.Chairs = chairs.ToArray();
            }



            var response = ApiClient.UpdateSessionAsync(SessionResponse);
            if (response.Result.Item1 == false || response.Result.Item2 <= 0)
                return RedirectToPage("/Error");

            return RedirectToPage("/Conference/Index", new { conference_id = SessionResponse.ConferenceID });
        }

      
    }
}
