﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        public List<Product> GetAllProducts();
        public Product GetProductById(int id);
        public bool AddProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int id);
    }
}
