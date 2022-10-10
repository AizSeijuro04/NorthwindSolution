using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.Product;
using Northwind.Services.Abstraction;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Northwind.Web.Controllers
{
    public class ProductOnSaleController : Controller
    {
        private readonly IServiceManager _context;

        public ProductOnSaleController(IServiceManager context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult> CreateFullOrder(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var productId = productDto.ProductId;

                var order = new OrderForCreateDto
                {
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Now.AddDays(3)
                };

                var orderDetail = new OrderDetailForCreateDto
                {
                    ProductId = productId,
                    UnitPrice = (decimal)productDto.UnitPrice,
                    Quantity = Convert.ToInt16(productDto.QuantityPerUnit),
                    Discount = 0
                };
                _context.ProductService.CreateOrderDetail(order, orderDetail);
                return RedirectToAction(nameof(Index));
            }


            return View(productDto);
        }

        // GET: ProductOnSale
        public async Task<ActionResult> Index()
        {
            var productOnSale = await _context.ProductService.GetProductOnSales(false);
            return View(productOnSale);
        }

        // GET: ProductOnSale/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.ProductService.GetProductOnSalesById((int)id, false);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductOnSale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductOnSale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnSale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductOnSale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductOnSale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductOnSale/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
