using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreenSpace.Data;
using GreenSpace.Models;

namespace GreenSpace.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Auths");
            }

            var cartItems = await _context.CartItems
                .Where(c => c.Username == username)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "CartItems");
            }

            ViewBag.CartItems = cartItems;
            ViewBag.TotalAmount = cartItems.Sum(c => (c.Price ?? 0) * c.Quantity);
            ViewBag.ShippingFee = 30000;
            ViewBag.GrandTotal = ViewBag.TotalAmount + 30000;

            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Auths");
            }

            var cartItems = await _context.CartItems
                .Where(c => c.Username == username)
                .ToListAsync();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "CartItems");
            }

            order.Username = username;
            order.OrderCode = $"ORD{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
            order.CreatedAt = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;

            double totalAmount = cartItems.Sum(c => (c.Price ?? 0) * c.Quantity);
            double shippingFee = 30000;
            order.TotalAmount = totalAmount + shippingFee;
            order.ShippingFee = shippingFee;

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage));

                TempData["ErrorMessage"] = "Model không hợp lệ: " + errors;

                ViewBag.CartItems = cartItems;
                ViewBag.TotalAmount = totalAmount;
                ViewBag.ShippingFee = shippingFee;
                ViewBag.GrandTotal = totalAmount + shippingFee;

                return View(order);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderItems = cartItems.Select(c => new OrderItem
            {
                OrderId = order.Id,
                ProductId = c.ProductId ?? 0,
                Name = c.Name ?? "Sản phẩm",
                Price = c.Price ?? 0,
                Quantity = c.Quantity,
                ImageUrl = c.ImageUrl
            }).ToList();

            _context.OrderItems.AddRange(orderItems);
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đặt hàng thành công!";
            return RedirectToAction("Index"); 
        }

        // GET: Orders/Index
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            var orders = await _context.Orders
                .Where(o => o.Username == username)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return View(orders);
        }

    }
}
