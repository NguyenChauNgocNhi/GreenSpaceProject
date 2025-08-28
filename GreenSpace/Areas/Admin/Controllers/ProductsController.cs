using Azure;
using GreenSpace.Data;
using GreenSpace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenSpace.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Images) 
                .ToListAsync();

            return View(products);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, string? tags, List<IFormFile>? images)
        {
            if (!ModelState.IsValid)
                return View(product);

            if (await _context.Products.AnyAsync(p => p.Slug == product.Slug))
            {
                ModelState.AddModelError("Slug", "Slug này đã tồn tại. Vui lòng nhập slug khác.");
                return View(product);
            }

            product.TouchCreated();
            product.TouchUpdated();

            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 

            if (!string.IsNullOrEmpty(tags))
            {
                var tagList = tags.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var tagName in tagList)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                              ?? new Tag { Name = tagName };
                    product.Tags.Add(tag);
                }
                await _context.SaveChangesAsync();
            }

            if (images != null && images.Count > 0)
            {
                bool isFirst = true;
                foreach (var file in images)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    var productImage = new ProductImage
                    {
                        ProductId = product.ProductId,
                        ImageUrl = "images/" + fileName
                    };
                    _context.ProductImages.Add(productImage);

                    if (isFirst)
                    {
                        product.FirstImageUrl = productImage.ImageUrl;
                        isFirst = false;
                    }
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Tags)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound();

            ViewBag.Tags = string.Join(", ", product.Tags.Select(t => t.Name));
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Product product, string? tags, List<IFormFile>? images)
        {
            if (id != product.ProductId) return NotFound();
            if (!ModelState.IsValid) return View(product);

            var existingProduct = await _context.Products
                .Include(p => p.Tags)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (existingProduct == null) return NotFound();

            if (await _context.Products.AnyAsync(p => p.Slug == product.Slug && p.ProductId != id))
            {
                ModelState.AddModelError("Slug", "Slug này đã tồn tại. Vui lòng nhập slug khác.");
                return View(product);
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.OriginalPrice = product.OriginalPrice;
            existingProduct.Description = product.Description;
            existingProduct.Category = product.Category;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.Slug = product.Slug;
            existingProduct.TouchUpdated();

            existingProduct.Tags.Clear();
            if (!string.IsNullOrEmpty(tags))
            {
                var tagList = tags.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var tagName in tagList)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                              ?? new Tag { Name = tagName };
                    existingProduct.Tags.Add(tag);
                }
            }

            if (images != null && images.Count > 0)
            {
                _context.ProductImages.RemoveRange(existingProduct.Images);

                bool isFirst = true;
                foreach (var file in images)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);

                    var productImage = new ProductImage
                    {
                        ProductId = existingProduct.ProductId,
                        ImageUrl = "images/" + fileName
                    };
                    _context.ProductImages.Add(productImage);

                    if (isFirst)
                    {
                        existingProduct.FirstImageUrl = productImage.ImageUrl;
                        isFirst = false;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var product = await _context.Products
                        .Include(p => p.Images) 
                        .Include(p => p.Tags)   
                        .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null) return NotFound();

            if (product.Images != null && product.Images.Any())
            {
                _context.ProductImages.RemoveRange(product.Images);
            }

            if (product.Tags != null && product.Tags.Any())
            {
                product.Tags.Clear();
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
