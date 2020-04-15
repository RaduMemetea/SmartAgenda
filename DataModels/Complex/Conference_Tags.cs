using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataModels.Complex
{
   public class Conference_Tags
    {
        [Key]
        [ForeignKey("Conference")]
        public int ConferenceID { get; set; }

        [Key]
        [ForeignKey("Tag")]
        public String TagID { get; set; }

    }
}
