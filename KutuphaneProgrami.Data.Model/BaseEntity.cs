using System.ComponentModel.DataAnnotations;

namespace KutuphaneProgrami.Data.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
