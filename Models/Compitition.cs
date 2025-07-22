using System;

namespace IOFA.Models
{
    public class Compitition
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Theme { get; set; }

        public string Description { get; set; }

        public DateTime? SubmitDate { get; set; } // Use DateTime? if the date can be NULL
    }
}
