﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Authors
    {
        
        public int Id  { get; set; }
        public string? Name { get; set; }
    }
}
