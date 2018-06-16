using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.ViewModels;
using System;
using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Interfaces;
using System.Linq;
using ApplicationCore.Specifications;

namespace Microsoft.eShopWeb.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AdminManagerController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public AdminManagerController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var orders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification());
            var viewModel = orders
                .Select(o => new OrderViewModel()
                {
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems?.Select(oi => new OrderItemViewModel()
                    {
                        Discount = 0,
                        PictureUrl = oi.ItemOrdered.PictureUri,
                        ProductId = oi.ItemOrdered.CatalogItemId,
                        ProductName = oi.ItemOrdered.ProductName,
                        UnitPrice = oi.UnitPrice,
                        Units = oi.Units
                    }).ToList(),
                    OrderNumber = o.Id,
                    Total = o.Total(),
                    CustomerId = o.BuyerId

                });

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int orderId)
        {

            var orders = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification());
            var orderToRemove = await _orderRepository.GetByIdAsync(orderId);
            //orders.Remove(orderToRemove);

            await _orderRepository.DeleteAsync(orderToRemove);
            var ordersAfter = await _orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification());

            return RedirectToAction("Index");
        }
    }
}
