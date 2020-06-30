using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class AgendaEntry
    {
        public Conference Conference { get; set; }
        public List<SessionResponse> Sessions { get; set; }
    }
}
