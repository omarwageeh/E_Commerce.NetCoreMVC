using E_Commerce.Data.Context;
using E_Commerce.Dto;
using E_Commerce.Model;
using E_Commerce.Repository.Interface;

namespace E_Commerce.Service
{
    public class UserService
    {
        private readonly IUnitOfWork _uow;
        public UserService(IUnitOfWork uow) 
        {
            _uow = uow;
        }
       public void Register(CustomerDto customer)
        {
            Customer user = new Customer()
            {
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
            };
            _uow.UserRepository.Add(user);
            _uow.CustomerRepository.Add(user);
            _uow.SaveChanges();
        }
    public async Task<User?> LoginUser(string email, string password)
        {
            var user = await  _uow.UserRepository.Get(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}