using System.ComponentModel.DataAnnotations;

namespace IOFA.Models
{
    public class Award
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string StudentName { get; set; }

        public string Position { get; set; }
    }
}
