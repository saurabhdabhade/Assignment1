using LibraryClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Repository.IRepository
{
    public interface IItemRepository
    {
        Task<Item> Get(string ItemName);
        Task<IEnumerable<Item>> GetAll();
        Task<Item> Add(Item item);
        Task<Item> Update(Item item);
        Task Delete(string ItemName);
    }
}
