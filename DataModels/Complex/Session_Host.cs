using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.Complex
{
    public class Session_Host
    {
        [Key]
        [ForeignKey("Session")]
        public int SessionID { get; set; }
        [Key]
        [ForeignKey("Person")]
        public int PersonID { get; set; }

    }
}
