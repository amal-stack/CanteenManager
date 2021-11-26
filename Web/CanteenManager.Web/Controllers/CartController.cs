using CanteenManager.Web.Data;
using CanteenManager.Web.Models;
using CanteenManager.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanteenManager.Web.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public CartController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Add(int? foodItemId, string returnUrl)
        {
            if (foodItemId is null)
            {
                return NotFound();
            }

            var cartId = await _context
                .Carts
                .Where(c => c.UserId == _userManager.GetUserId(User))
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            var existingCartItem = await _context
                .CartItems
                .FirstOrDefaultAsync(ci => 
                    ci.FoodItemId == foodItemId 
                    && ci.CartId == cartId);

            if (existingCartItem == null)
            {
                var newCartItem = new CartItem
                {
                    CartId = cartId,
                    FoodItemId = foodItemId.Value,
                    Quantity = 1
                };
                _context.Add(newCartItem);
                await _context.SaveChangesAsync();
                return LocalRedirect(returnUrl);
            }

            existingCartItem.Quantity += 1;
            _context.Update(existingCartItem);
            await _context.SaveChangesAsync();
            return LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> Remove(int? foodItemId, string returnUrl)
        {
            if (foodItemId is null)
            {
                return NotFound();
            }

            var cartId = await _context
                .Carts
                .Where(c => c.UserId == _userManager.GetUserId(User))
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            var existingCartItem = await _context
                .CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.FoodItemId == foodItemId
                    && ci.CartId == cartId);

            if (existingCartItem is null)
            {
                return LocalRedirect(returnUrl);
            }

            if (existingCartItem.Quantity == 1)
            {
                _context.Remove(existingCartItem);
                await _context.SaveChangesAsync();
            }

            existingCartItem.Quantity -= 1;
            _context.Update(existingCartItem);
            await _context.SaveChangesAsync();
            return LocalRedirect(returnUrl);
        }
    }
}
