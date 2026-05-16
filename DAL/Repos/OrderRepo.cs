using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DAL.Repos
{
    public class OrderRepo
    {
        OnlineFoodOrderingSystemDbContext db;
        
        public OrderRepo(OnlineFoodOrderingSystemDbContext db)
        {
            this.db = db;
        }
        public int Create(Order o)
        {
            db.Orders.Add(o);
            db.SaveChanges();
            return o.Id;
        }
        public bool CreateItem(OrderItem item)
        {
            db.OrderItems.Add(item);
            return db.SaveChanges() > 0;
        }
        public List<Order>GetByUser(int userId)
        {
            return db.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
        public List<Order> GetAll()
        {
            return db.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }
        public Order? Get(int id)
        {
            return db.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);
        }
        public bool UpdateStatus(int id, string status)
        {
            var order = db.Orders.Find(id);
            if (order == null) return false;
            order.Status = status;
            return db.SaveChanges() > 0;
        }
        public List<OrderItem> GetItems(int orderId)
        {
            return db.OrderItems
                .Where(i => i.OrderId == orderId)
                .ToList();
        }



    }
}
