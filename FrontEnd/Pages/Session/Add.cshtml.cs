using DataModels;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Pages.Session
{
    public class AddModel : PageModel
    {

        [BindProperty]
        public SessionResponse SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }
        public ConferenceResponse Conference { get; private set; }
        [BindProperty]
        public string chairsList { get; set; }

        public AddModel(IApiClientService apiClient)
        {
            ApiClient = apiClient;
        }

        public IActionResult OnGet(int conference_id)
        {
            SessionResponse = new SessionResponse();

            Conference = ApiClient.GetConferenceAsync(conference_id).Result;

            return Page();
        }


        public IActionResult OnPost(int conference_id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            SessionResponse.ID = 0;
            SessionResponse.ConferenceID = conference_id;
            SessionResponse.Location.ID = 0;

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
                        for (int i = 1; i < pers.Length-1; i++)
                            firstName += (" " + pers[i]);

                    chairs.Add(new Person { ID = 0, First_Name = firstName, Last_Name = lastName });
                }

                SessionResponse.Chairs = chairs.ToArray();
            }
            var response = ApiClient.CreateSessionAsync(SessionResponse);
            if (response.Result.Item1 == false || response.Result.Item2 <= 0)
                return RedirectToPage("/Error");

            return RedirectToPage("/Conference/Index", new { conference_id });
        }
    }
}
