using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Models;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Order order)
        {
            Update(order);
        }

        public async Task<IEnumerable<Order>> GetAllOrder(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.OrderId)
                .Include(e=>e.Customer)
                .Include(f=>f.ShipViaNavigation)
                .Include(g=>g.Employee)
                .Include(h=>h.OrderDetails)
                .ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId, bool trackChanges)
        {
            return await FindByCondition(c => c.OrderId.Equals(orderId), trackChanges)
                .Include(e => e.Customer)
                .Include(f => f.ShipViaNavigation)
                .Include(g => g.Employee)
                .Include(h => h.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public void Insert(Order order)
        {
            Create(order);
        }

        public void Remove(Order order)
        {
            Delete(order);
        }
    }
}
