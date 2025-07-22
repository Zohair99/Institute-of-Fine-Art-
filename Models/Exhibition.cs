using System;
using System.ComponentModel.DataAnnotations;

namespace IOFA.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Pic { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
    }

}

