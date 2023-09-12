using E_Commerce.Dto;
using E_Commerce.Model;
using E_Commerce.Model.Enum;
using E_Commerce.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    
    public class CustomerService
    {
        private readonly IUnitOfWork _uow;
        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<IEnumerable<Order>?> GetOrders(Expression<Func<Order, bool>> predicate)
        {
            return await _uow.OrderRepository.GetAllWithInclude(predicate, "Customer");
        }

    }
}
