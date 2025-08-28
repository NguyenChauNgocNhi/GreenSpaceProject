using GreenSpace.Data;
using GreenSpace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSpace.Controllers
{
    [Authorize]
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CartItems/{username}
        [HttpGet]
        public async Task<IActionResult> Index(string username)
        {
            IQueryable<CartItem> query = _context.CartItems;

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(c => c.Username == username);
            }

            var cartItems = await query.ToListAsync();

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: CartItems/Add
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartItem cartItem)
        {
            var username = User.Identity.Name; 

            if (username == null)
                return Unauthorized("Vui lòng đăng nhập trước khi thêm vào giỏ hàng.");


            cartItem.Username = username;

            if (cartItem.ProductId == null || string.IsNullOrEmpty(cartItem.Username))
            {
                return BadRequest("Thiếu thông tin sản phẩm hoặc tên người dùng.");
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == cartItem.ProductId);

            if (product == null)
            {
                return NotFound($"Không tìm thấy sản phẩm với ID {cartItem.ProductId}.");
            }

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Username == cartItem.Username && c.ProductId == cartItem.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += (cartItem.Quantity > 0 ? cartItem.Quantity : 1);
                _context.CartItems.Update(existingCartItem);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    message = "Cập nhật số lượng sản phẩm trong giỏ hàng thành công!",
                    data = existingCartItem
                });
            }
            else
            {
                var newCartItem = new CartItem
                {
                    Username = cartItem.Username,
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = cartItem.Quantity > 0 ? cartItem.Quantity : 1,
                    ImageUrl = product.FirstImageUrl,
                    Slug = product.Slug
                };

                _context.CartItems.Add(newCartItem);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Thêm sản phẩm vào giỏ hàng thành công!",
                    data = newCartItem
                });
            }
        }

        // PUT: CartItems/Update
        [HttpPost]
        public async Task<IActionResult> Update(CartItem cartItem)
        {
            var existingItem = await _context.CartItems.FindAsync(cartItem.Id);

            if (existingItem != null)
            {
                existingItem.Quantity = cartItem.Quantity;
                _context.CartItems.Update(existingItem);
                await _context.SaveChangesAsync();
                return Ok("Cập nhật số lượng thành công!");
            }

            return BadRequest("Sản phẩm không tồn tại trong giỏ hàng!");
        }
    }
}
