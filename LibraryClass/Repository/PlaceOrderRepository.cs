using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Repository
{
    public class PlaceOrderRepository : IPlaceOrder
    {
        private readonly MyDBContext _dbContext;
        public PlaceOrderRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PlaceOrder> Add(PlaceOrder item)
        {
            var result = await _dbContext.placeOrders.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(int OrderID)
        {
            var result = await _dbContext.placeOrders.FirstOrDefaultAsync(u => u.OrderID == OrderID);
            if (result != null)
            {
                _dbContext.placeOrders.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PlaceOrder> Get(int OrderID)
        {
            return await _dbContext.placeOrders.FirstOrDefaultAsync(u => u.OrderID == OrderID);
        }

        public async Task<IEnumerable<PlaceOrder>> GetAll()
        {
            return await _dbContext.placeOrders.ToListAsync();
        }

        public async Task<PlaceOrder> Update(PlaceOrder item)
        {
            var result = await _dbContext.placeOrders.FirstOrDefaultAsync(u => u.OrderID == item.OrderID);
            if (result != null)
            {
                result.OrderID = item.OrderID;
                result.ItemName = item.ItemName;
                result.Cust_Email = item.Cust_Email;

                if (item.IQuantity != 0)
                {
                    result.IQuantity = item.IQuantity;
                }
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
