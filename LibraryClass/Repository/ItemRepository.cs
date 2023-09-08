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
    public class ItemRepository : IItemRepository
    {
        private readonly MyDBContext _dbContext;
        public ItemRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Item> Add(Item item)
        {
            var result = await _dbContext.items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(string ItemName)
        {
            var result = await _dbContext.items.FirstOrDefaultAsync(u => u.ItemName == ItemName);
            if(result != null)
            {
                _dbContext.items.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Item> Get(string ItemName)
        {
            return await _dbContext.items.FirstOrDefaultAsync(u => u.ItemName == ItemName);
        }

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _dbContext.items.ToListAsync();
        }

        public async Task<Item> Update(Item item)
        {
            var result = await _dbContext.items.FirstOrDefaultAsync(u => u.ItemName ==  item.ItemName);
            if( result != null )
            {
                result.ItemName = item.ItemName;
                result.IRate = item.IRate;
                result.IQuantity = item.IQuantity;

                if( item.IQuantity != 0 )
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
