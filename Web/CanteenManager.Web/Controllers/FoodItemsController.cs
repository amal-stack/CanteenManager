using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanteenManager.Web.Data;
using CanteenManager.Web.ViewModels;

namespace CanteenManager.Web.Models
{
    public class FoodItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodItems
        public async Task<IActionResult> Index()
        {
            return View(await _context
                .FoodItems
                .Select(f => new FoodItemsListModel
            { 
              Id =  f.Id, 
              Name = f.Name, 
              Price = f.Price, 
              Description =  f.Description, 
              CategoryId =  f.CategoryId,
              CategoryName = f.Category.Name
            }).ToListAsync());
        }

        public async Task<IActionResult> Image(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            return File(await (from f in _context.FoodItems
                               where f.Id == id
                               select f.Image
                              ).FirstOrDefaultAsync(),
                              "image/jpeg"
                            );
        }

        // GET: FoodItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        // GET: FoodItems/Create
        public IActionResult Create(int? categoryId)
        {
            if (categoryId is null)
            {
                return NotFound();
            }

            return View(new CreateFoodItemModel { CategoryId = categoryId.Value });
        }

        // POST: FoodItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModels.CreateFoodItemModel model)
        {
            byte[] imageContent = null;
            if (model.Image is not null)
            {
                using var memoryStream = new MemoryStream();
                model.Image.CopyTo(memoryStream);
                if (memoryStream.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError(nameof(model.Image), "Image must be lower than 2 MiB");
                }
                else
                {
                    imageContent = memoryStream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                FoodItem food = new()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Image = imageContent,
                };
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: FoodItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Image")] FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemExists(foodItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = await _context.FoodItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItem = await _context.FoodItems.FindAsync(id);
            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(e => e.Id == id);
        }
    }
}
