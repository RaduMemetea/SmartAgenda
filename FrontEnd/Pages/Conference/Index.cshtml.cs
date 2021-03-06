﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Pages.Conference
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        protected readonly IApiClientService ApiClient;
        private readonly IApiIdentityService IdentityClient;

        [BindProperty]
        public ConferenceResponse conference { get; set; }
        public List<SessionResponse> Sessions { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IApiClientService apiClient, IApiIdentityService apiIdentityService)
        {
            _logger = logger;
            ApiClient = apiClient;
            IdentityClient = apiIdentityService;
        }
        public async Task<IActionResult> OnGet(int conference_id)
        {
            conference = await ApiClient.GetConferenceAsync(conference_id);

            if (conference == null)
                return RedirectToPage("/Error");

            var response = ApiClient.GetSessionsByConference(conference.ID).Result;
            if (response is null || !response.Any())
                Sessions = new List<SessionResponse>() { };
            else
                Sessions = response.ToList();

            return Page();
        }

    }
}