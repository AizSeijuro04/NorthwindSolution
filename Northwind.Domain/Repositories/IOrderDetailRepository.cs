using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAllOrderDetail(bool trackChanges);

        Task<OrderDetail> GetOrderDetailById(int orderDetailId, bool trackChanges);

        void Insert(OrderDetail orderDetail);

        void Edit(OrderDetail orderDetail);

        void Remove(OrderDetail orderDetail);
    }
}
