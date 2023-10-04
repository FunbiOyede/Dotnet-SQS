using System;
using SQS.Publisher.Models;

namespace SQS.Publisher.Services
{
	public interface IOrderService
	{
		Task<List<Orders>> GetOrders();
        Task<int> DeleteOrders(Guid id);
        Task<int> UpdateOrders(Orders order);
        Task<int> CreateOrders(Orders order);
        Task<Orders> GetOrder(Guid id );
    }
}

