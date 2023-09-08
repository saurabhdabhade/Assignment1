using LibraryClass.Models;
using LibraryClass.Models.DTO;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> Get(string Cust_Email);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Add(Customer entity);
        Task<Customer> Update(Customer entity);
        Task Delete(string Cust_Email);
        Task<LoginResponse> Login(LoginRequest loginRequestDTO);

    }
}
