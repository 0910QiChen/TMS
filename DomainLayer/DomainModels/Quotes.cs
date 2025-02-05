﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.DomainModels
{
    public class Quotes
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuoteID { get; set; }
        [Required]
        public string QuoteType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        [Required]
        [Range(0.00, Double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Premium { get; set; }
        [Required]
        public string Sales { get; set; }
    }
}