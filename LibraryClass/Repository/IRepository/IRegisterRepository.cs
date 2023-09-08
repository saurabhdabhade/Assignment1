using LibraryClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Repository.IRepository
{
    public interface IRegisterRepository
    {
        Task<Register> Registers(Register register);
        Task<IEnumerable<Register>> GetAll();
        Task<Register> Get(int RegisterID);
        Task<Register> Update(Register item);
        Task Delete(int RegisterID);
    }
}
