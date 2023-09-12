﻿using E_Commerce.Dto;
using E_Commerce.Model;
using E_Commerce.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>?> GetProducts(Expression<Func<Product, bool>> predicate)
        {
            return await _unitOfWork.ProductRepository.GetAllWithInclude(predicate, "Category");
        }
        public async void AddProduct(ProductDto product)
        {
            var cat = await _unitOfWork.CategoryRepository.Get(c => c.Name == product.catName) ?? await _unitOfWork.CategoryRepository.Add(new Category { Name = product.catName });
            if (cat == null) return;
            Product prod = new Product()
            {
                NameEn = product.NameEn,
                NameAr = product.NameAr,
                UnitPrice = product.UnitPrice,
                StockQuantity = product.StockQuantity,
                Category = cat,
            };
            await _unitOfWork.ProductRepository.Add(prod);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteProduct(Guid id)
        {
            var product =  await _unitOfWork.ProductRepository.Get(p => p.Id == id);
            if (product == null) return;
            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
