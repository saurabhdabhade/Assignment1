using AutoMapper;
using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository;
using LibraryClass.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/CustomerController")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        APIResponse _apiResponse = new APIResponse(); 

        public CustomerController(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var resultCustomer = new List<Customer>();
                var result = await _customerRepository.GetAll();
                foreach (var custom in result)
                {
                    if (custom.IsDeleted == false)
                    {
                        resultCustomer.Add(custom);
                    }
                }
                _apiResponse.Result = resultCustomer;
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data From The Database");
            }
        }

        [HttpGet("{Email}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomer(string Email)
        {
            try
            {
                var result = await _customerRepository.Get(Email);
                if (result == null)
                {
                    return NotFound();
                }
                _apiResponse.Result = result;
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data From The Database");
            }
        }

        /*[HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerDTO customer)
        {
            try
            {
                String subject = "Welcome";
                String body = "Welcome, " + customer.Cust_FirstName + " " + customer.Cust_LastName;
                var result = _mapper.Map<Customer>(customer);
                await _customerRepository.Add(result);
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(customer.Cust_Email, subject, body);
                _apiResponse.Result = result;
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Creating New Customer Record");
            }
        }*/

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer1(List<CustomerDTO> customer)
        {
            CustomerDTO custDTO = new CustomerDTO();
            try
            {
                for (int i = 0; i < customer.Count; i++)
                {
                    String subject = "Welcome";
                    String body = "Welcome, " + customer[i].Cust_FirstName + " " + customer[i].Cust_LastName;
                    CustomerDTO custDTO1 = new CustomerDTO();
                    custDTO1.Cust_FirstName = customer[i].Cust_FirstName;
                    custDTO1.Cust_LastName = customer[i].Cust_LastName;
                    custDTO1.Cust_Email = customer[i].Cust_Email;
                    custDTO1.Cust_Phone = customer[i].Cust_Phone;
                    custDTO1.EventDateTime = customer[i].EventDateTime;
                    custDTO = custDTO1;
                    var result = _mapper.Map<Customer>(custDTO);
                    await _customerRepository.Add(result);
                    EmailSender emailSender = new EmailSender();
                    emailSender.SendEmail(customer[i].Cust_Email, subject, body);
                    _apiResponse.Result = result;
                }
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Creating New Customer Record");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            try
            {
                await _customerRepository.Update(customer);
                _apiResponse.Result = customer;
                return Ok(_apiResponse);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating with Customer Record");
            }
        }
        [HttpDelete("{Email}")]
        public async Task<IActionResult> DeleteCustomer(string Email)
        {
            try
            {
                var cust = await _customerRepository.Get(Email);
                if (cust == null)
                {
                    return NotFound($"Customer With ID = {Email} Not Found");
                }
                cust.IsDeleted = true;
                await _customerRepository.Update(cust);
                //await _customerRepository.Delete(Email);
                _apiResponse.Result = $"Student With ID = {Email} Is Deleted";
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting with Customer Record");
            }
        }

        [HttpPost("{login}")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {

            APIResponse _apiResponse = new APIResponse();
            var loginResponse = await _customerRepository.Login(model);
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username or Password incorrect");
                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = loginResponse;
            return Ok(_apiResponse);
        }
    }
}
