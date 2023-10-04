using System;
using Microsoft.EntityFrameworkCore;
using SQS.Publisher.Controllers;
using SQS.Publisher.Data;
using SQS.Publisher.Messaging;
using SQS.Publisher.Models;

namespace SQS.Publisher.Services
{
	public class OrderService : IOrderService
    {

        private readonly OrderContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly ISqsMessage _sqsMessager;
        public OrderService( OrderContext context, ILogger<OrderService> logger, ISqsMessage sqsMessager)
		{
            _context = context;
            _logger = logger;
            _sqsMessager = sqsMessager;
		}

        public async Task<int> CreateOrders(Orders order)
        {
            _logger.LogInformation("Creating Order....");
            var exisitingOrder = await _context.Orders.FindAsync(order.Id);
            if( exisitingOrder is not null)
            {

                throw new Exception("Item exits");
                
            }


            _context.Orders.Add(order);
            var value = await _context.SaveChangesAsync();

            var message = new OrderCreated { Id = order.Id, publishedDate = DateTime.Now.ToLongDateString() };

            if (value == 1) await _sqsMessager.SendMessageAsync(message);

            return value;

        }

        public async Task<int> DeleteOrders(Guid id)
        {
            _logger.LogInformation("Deleting Order....");
            var exisitingOrder = await _context.Orders.FindAsync(id);
            if (exisitingOrder is null)
            {

                throw new Exception("Item does not exits");

            }

             _context.Orders.Remove(exisitingOrder);
            var value = await _context.SaveChangesAsync();
            return value;
        }

        public async Task<Orders> GetOrder(Guid id)
        {
            _logger.LogInformation("Fetching Order....");
            var order =  await _context.Orders.FindAsync(id);
            if (order is null) throw new Exception("item not found");
            return order;
        }



        public async Task<List<Orders>>  GetOrders()
        {
            var result =  await _context.Orders.ToListAsync();
            return result;
        }

        public async Task<int> UpdateOrders(Orders order)
        {
            var exisitingOrder = await _context.Orders.FindAsync(order.Id);
            if (exisitingOrder is not null)
            {
                _context.Update(order);
                var value = await _context.SaveChangesAsync();

                return value;
            }

            throw new Exception("Order does not exits");
        }
    }
}

