﻿using System;

namespace SolarCoffe.Web.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsArchived { get; set; }
    }
}