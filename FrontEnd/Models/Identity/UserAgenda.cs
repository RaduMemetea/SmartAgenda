using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models.Identity
{
    public class UserAgenda
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ConferenceId { get; set; }
        [Required]
        public int SessionId { get; set; }
        [Required]
        public int TalkId { get; set; }
    }
}
