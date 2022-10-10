using AutoMapper;
using Northwind.Contracts.Dto.Order;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrderDetailService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(OrderDetailDto orderDetailDto)
        {
            var edit = _mapper.Map<OrderDetail>(orderDetailDto);
            _repositoryManager.OrderDetailRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllOrderDetail(bool trackChanges)
        {
            var orderDetailModel = await _repositoryManager.OrderDetailRepository.GetAllOrderDetail(trackChanges);
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetailModel);
            return orderDetailDto;
        }

        public async Task<OrderDetailDto> GetOrderDetailById(int orderDetailId, bool trackChanges)
        {
            var orderDetailModel = await _repositoryManager.OrderDetailRepository.GetOrderDetailById(orderDetailId, trackChanges);
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetailModel);
            return orderDetailDto;
        }

        public void Insert(OrderDetailForCreateDto orderDetailForCreateDto)
        {
            throw new NotImplementedException();
        }

        public void Remove(OrderDetailDto orderDetailDto)
        {
            var hapus = _mapper.Map<OrderDetail>(orderDetailDto);
            _repositoryManager.OrderDetailRepository.Remove(hapus);
            _repositoryManager.Save();
        }
    }
}
