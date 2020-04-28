using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels.Complex
{
    public class Talk_Persons
    {
        [Key]
        [ForeignKey("Talk")]
        public int TalkID { get; set; }
        [Key]
        [ForeignKey("Person")]
        public int PersonID { get; set; }
    }
}
