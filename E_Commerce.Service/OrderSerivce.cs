using E_Commerce.Model;
using E_Commerce.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public class OrderSerivce
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderSerivce(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public async Task<Order> AddOrder(Order order)
        {
            return await _unitOfWork.OrderRepository.Add(order);
        }
        public bool UpdateOrder(Order order)
        {
            _unitOfWork.OrderRepository.Update(order);
            return _unitOfWork.SaveChanges() > 0;
        }
        public async Task<OrderDetails> AddOrderDetails(OrderDetails orderDetails)
        {
            return await _unitOfWork.OrderDetailsRepository.Add(orderDetails);
        }
        public async Task<OrderDetails> GetOrderDetails(string orderId, string productId)
        {
            return await _unitOfWork.OrderDetailsRepository.Get(od => od.OrderId == Guid.Parse(orderId) && od.ProductId == Guid.Parse(productId));
        }
        public async Task RemoveOrderDetails(string orderId, string productId)
        {

            _unitOfWork.OrderDetailsRepository.Delete(await GetOrderDetails(orderId, productId));
            await _unitOfWork.SaveChangesAsync();
        }
        public async void UpdateOrderDetails(OrderDetails orderDetails)
        {
            _unitOfWork.OrderDetailsRepository.Update(orderDetails);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
