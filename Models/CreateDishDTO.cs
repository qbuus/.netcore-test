﻿using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreateDishDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}