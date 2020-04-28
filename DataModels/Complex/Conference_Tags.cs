using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
