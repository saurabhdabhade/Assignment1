using LibraryClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Repository.IRepository
{
    public interface IPlaceOrder
    {
        Task<PlaceOrder> Get(int OrderID);
        Task<IEnumerable<PlaceOrder>> GetAll();
        Task<PlaceOrder> Add(PlaceOrder item);
        Task<PlaceOrder> Update(PlaceOrder item);
        Task Delete(int OrderID);
    }
}
