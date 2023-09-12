using E_Commerce.Model;
using E_Commerce.Repository.Interface;
using E_Commerce.Repository.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>?> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetAll();
        }
        public async Task<IEnumerable<Category>?> GetCategories(Expression<Func<Category, bool>> predicate)
        {
            return await _unitOfWork.CategoryRepository.GetAll(predicate);
        }
        public void AddCategory(string name)
        {
            Category category = new Category();
            category.Name = name;
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.SaveChanges();
        }
        public async void EditCategory(string from, string to)
        {
            var category = await _unitOfWork.CategoryRepository.Get(a => a.Name == from);
            if (category == null)
                return;
            category.Name = to;
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.SaveChanges();
        }
        public async void DeleteCategory(string name)
        {
            var category = await _unitOfWork.CategoryRepository.Get(c => c.Name == name);
            if (category == null)
                return;
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.SaveChanges();
        }
    }
}
