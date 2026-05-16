using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;

namespace BAL.Services
{
    public class OrderService
    {
        OrderRepo repo;
        Mapper mapper;

        public OrderService(OrderRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }
        public bool PlaceOrder(int userId, List<CartItemDTO> cartItems)
        {
            if (cartItems == null || cartItems.Count == 0) return false;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                Total = cartItems.Sum(i => i.Total)

            };
            int orderId = repo.Create(order);

            foreach(var item in cartItems)
            {
                var orderItem = new OrderItem
                { 
                    OrderId = orderId,
                    FoodId = item.FoodId,
                    FoodName = item.FoodName,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                repo.CreateItem(orderItem);


            }
            return true;


        }
        public List<OrderDTO>GetMyOrders(int userId)
        {
            var data = repo.GetByUser(userId);
            return mapper.Map<List<OrderDTO>>(data);
        }
        public List<OrderDTO>GetAllOrders()
        {
            var data = repo.GetAll();
            return mapper.Map<List<OrderDTO>>(data);

        }
        public OrderDTO? GetOrder(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<OrderDTO>(data);
        }
        public bool UpdateStatus(int id, string status)
        {
            return repo.UpdateStatus(id, status);
        }
    }
}
