using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDBContext _dbContext;
        private string secretKey;

        public CustomerRepository(MyDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            secretKey = configuration["ApiSettings:Secret"];

        }
        public async Task<Customer> Add(Customer entity)
        {
            var result = await _dbContext.customers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(string Email)
        {
            var result = await _dbContext.customers.FirstOrDefaultAsync(u => u.Cust_Email == Email);
            if (result != null)
            {
                _dbContext.customers.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Customer> Get(string Email)
        {
            return await _dbContext.customers.FirstOrDefaultAsync(u => u.Cust_Email == Email);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.customers.ToListAsync();
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequestDTO)
        {
            if (loginRequestDTO == null)
            {
                return new LoginResponse()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            // Encoding of a key=> we need it in bytes and it is in string.
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginRequestDTO.Password),
                  
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponse loginResponseDTO = new LoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                //User = userq
            };
            return loginResponseDTO;
        }

        public async Task<Customer> Update(Customer entity)
        {
            var result = await _dbContext.customers.FirstOrDefaultAsync(u => u.Cust_Email == entity.Cust_Email);
            if (result != null)
            {
                result.Cust_FirstName = entity.Cust_FirstName;
                result.Cust_LastName = entity.Cust_LastName;
                result.Cust_Phone = entity.Cust_Phone;
                result.Cust_Email = entity.Cust_Email;

                if (entity.Cust_Phone != 0)
                {
                    result.Cust_Phone = entity.Cust_Phone;
                }
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
