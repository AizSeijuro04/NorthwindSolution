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
    public class OrderDetailRepository : RepositoryBase<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(OrderDetail orderDetail)
        {
            Update(orderDetail);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.OrderId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(int orderDetailId, bool trackChanges)
        {
            return await FindByCondition(c => c.OrderId.Equals(orderDetailId), trackChanges)
                .Include(p => p.Product)
                .Include(o => o.Order)
                .SingleOrDefaultAsync();
        }

        public void Insert(OrderDetail orderDetail)
        {
            Create(orderDetail);
        }

        public void Remove(OrderDetail orderDetail)
        {
            Delete(orderDetail);
        }
    }
}
