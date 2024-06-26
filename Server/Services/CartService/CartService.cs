﻿
using LouiseTieDyeStore.Shared;
using Newtonsoft.Json;

namespace LouiseTieDyeStore.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);

            if (product.Sold)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "This item has already been purchased."
                };
            }

            cartItem.UserId = await _authService.GetUserId();

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var userId = await _authService.GetUserId();

            var count = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .CountAsync();
            return new ServiceResponse<int> { Data = count };
        }

        // Turns CartItems into CartProductResponse List
        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .Include(p => p.Images)
                    .Include(p => p.ProductType)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var itemInDB = await _context.CartItems.AnyAsync(ci => ci.ProductId == product.Id);
 
                if (product.Sold && itemInDB)
                {
                    _context.CartItems.Remove(item);
                    await _context.SaveChangesAsync();
                    continue;
                }

                if (product.Sold && !itemInDB)
                {
                    continue;
                }

                var cartProduct = new CartProductResponse
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.Images.First(i => i.IsMainImage).Url, 
                    Price = product.Price,
                    ProductType = product.ProductType.Name,
                    Size = product.Size,
                    Description = product.Description
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        // Get CartItems from DB
        public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts(string? userEmail = null)
        {
            if (userEmail == null)
            {
                userEmail = await _authService.GetUserEmail();
            }

            return await GetCartProducts(await _context.CartItems
                .Where(ci => ci.UserEmail == userEmail).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId)
        {
            int userId = await _authService.GetUserId();

            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId &&
                 ci.UserId == userId);

            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        // TODO: Why am I returning a list of products?
        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
        {
 
            string userEmail = cartItems.Select(ci => ci.UserEmail).First();

            // Get list of ProductIds that are already in Db Cart
            List<int> storedProductIds = await _context.CartItems
                .Where(ci => ci.UserEmail == userEmail)
                .Select(ci => ci.ProductId).ToListAsync();

            // Do not Re-Add items to DB cart
            var newItems = new List<CartItem>();

            foreach (var cartItem in cartItems)
            {
                if (!storedProductIds.Contains(cartItem.ProductId))
                {
                    newItems.Add(cartItem);
                }
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user != null)
            {
                newItems.ForEach(newItem => newItem.UserId = user.Id);
            }
           
            _context.CartItems.AddRange(newItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProducts(); // possibly remove/change this??
        }
    }
}
