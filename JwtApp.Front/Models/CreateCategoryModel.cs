﻿using System.ComponentModel.DataAnnotations;

namespace JwtApp.Front.Models
{
    public class CreateCategoryModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur!")]
        public string Definiton { get; set; } = null!;
    }
}
