using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class QuoteVM
    {
        public int QuoteID { get; set; }
        [Required]
        public string QuoteType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required]
        public decimal Premium { get; set; }
        [Required]
        public string Sales { get; set; }
    }
}