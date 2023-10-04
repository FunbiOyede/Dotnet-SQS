using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using SQS.Publisher.Models;
using SQS.Publisher.Services;
using AutoMapper;

namespace SQS.Publisher.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        

        private readonly ILogger<OrderController> _logger;

        private readonly IOrderService _service;
        private readonly IMapper _mapper;


        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IMapper mapper)
        {
            _logger = logger;
            _service = orderService;
            _mapper = mapper;
        }



        [HttpPost]
        public  async Task<ActionResult> CreateOrder([FromBody] OrderData order)
        {
            var data = _mapper.Map<Orders>(order);
            var result =  await _service.CreateOrders(data);
            return Created("Created", new { Message = "Order Created Successfully" });
        }


        [HttpGet("/get/order")]
        public async Task<ActionResult> GetOrder([FromQuery] Guid id)
        {
            var result = await _service.GetOrder(id);

            return Ok(result);
        }


       [HttpGet("all")]
        public ActionResult<List<Orders>> GetAllOrders()
        {
            var result =  _service.GetOrders();

            return Ok(result);
        }
    }
}

