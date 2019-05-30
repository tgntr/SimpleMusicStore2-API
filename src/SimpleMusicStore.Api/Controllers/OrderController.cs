using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Models.Binding;

namespace SimpleMusicStore.Api.Controllers
{
    //[ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orders;

        public OrderController(IOrderService orders)
            : base()
        {
            _orders = orders;
        }

        public async Task AddToCart(int id)
        {
            
        }


    }
}