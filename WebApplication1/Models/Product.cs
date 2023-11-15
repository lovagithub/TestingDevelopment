﻿using System.Diagnostics;

namespace WebApplication1.Models
{
    public class Product
    {
        public int ArticleNumber { get; set; }
        public string? SupplierArticleNumber { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public static implicit operator Product(ProductEntity product)
        {
            try
            {
                return new Product
                {
                    ArticleNumber = product.ArticleNumber,
                    Name = product.Name,
                    SupplierArticleNumber = product.SupplierArticleNumber,
                    Description = product.Description
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }

        }
    }
}
    