using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrder(bool trackChanges);

        Task<OrderDto> GetOrderById(int orderId, bool trackChanges);

        void Insert(OrderForCreateDto orderForCreateDto);

        void Edit(OrderDto orderDto);

        void Remove(OrderDto orderDto);
    }
}
