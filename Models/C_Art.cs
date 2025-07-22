using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOFA.Models
{
    [Table("C_Art")]
    public class C_Art
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UploadName { get; set; }

        public string UploadEmal { get; set; }

        public string ImagePath { get; set; } // <-- New column


    }

}
