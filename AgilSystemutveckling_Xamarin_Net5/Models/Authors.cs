﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Authors
    {
        [Key]
        public int Id  { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
