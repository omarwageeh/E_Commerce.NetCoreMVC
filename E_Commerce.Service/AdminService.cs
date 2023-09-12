using E_Commerce.Dto;
using E_Commerce.Model;
using E_Commerce.Model.Enum;
using E_Commerce.Repository.Interface;
using E_Commerce.Repository.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public class AdminService
    {
        private readonly IUnitOfWork _uow;
        public AdminService(IUnitOfWork uow) 
        {
            _uow = uow;
        }
        public void AddAdmin(AdminDto admin)
        {
            Admin user = new Admin()
            {
                FullName = admin.FullName,
                Email = admin.Email,
                JobTitle = admin.JobTitle,
                HireDate = admin.HireDate,
            };
            _uow.UserRepository.Add(user);
            _uow.AdminRepository.Add(user);
            _uow.SaveChanges();
        }
        public async Task<IEnumerable?> GetAdmins()
        {

            var admins = await _uow.AdminRepository.GetAll();
            return admins;
        }
        public async Task DeleteAdmin(string email)
        {
            var admin = await _uow.AdminRepository.Get(a => a.Email == email);
            if(admin == null) 
            {
                return;
            }
            _uow.AdminRepository.Delete(admin);
            _uow.SaveChanges();
        }
        public async void EditAdmin(AdminDto admin)
        {
            var user = await _uow.AdminRepository.Get(a=>a.Email == admin.Email);
            if (user == null)
            {
               return;
            }
            user.FullName = admin.FullName;
            user.JobTitle = admin.JobTitle;
            _uow.AdminRepository.Update(user);
            _uow.SaveChanges();
        }

    }
}
