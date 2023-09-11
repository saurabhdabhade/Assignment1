using AutoMapper;
using Castle.Core.Resource;
using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository;
using LibraryClass.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/RegisterController")]
    public class RegisterController : ControllerBase
    {
        private readonly MyDBContext _dbContext;
        private readonly IRegisterRepository _registerRepository;
        private readonly IMapper _mapper;
        APIResponse _apiresponse = new APIResponse();
        private IRegisterRepository object1;
        private IMapper object2;

        public RegisterController(IRegisterRepository registerRepository, MyDBContext dbContext, IMapper mapper)
        {
            _registerRepository = registerRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet("{RegisterID}")]
        public async Task<ActionResult<Register>> GetRegister(int RegisterID)
        {
            try
            {
                var result = await _registerRepository.Get(RegisterID);
                if (result == null)
                {
                    return NotFound();
                }
                _apiresponse.Result = result;
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data From The Database");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRegisterdCustomers()
        {
            try
            {
                var resultRegister = new List<Register>();
                IEnumerable<Register> registerList = await _registerRepository.GetAll();
                foreach (var reg in registerList)
                {
                    if (reg.IsDeleted == false)
                    {
                        resultRegister.Add(reg);
                    }
                }
                _apiresponse.Result = resultRegister;
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Customer Data");
            }
        }

        [HttpPost]
        public async Task<ActionResult> RegisterUsers(RegisterDTO registerRequest)
        {
            try
            {
                String subject = "Welcome";
                String body = "Welcome, " + registerRequest.First_Name + " " + registerRequest.Last_Name + " You Have Registered Successfully...";
                var result = _mapper.Map<Register>(registerRequest);
                await _registerRepository.Registers(result);
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(registerRequest.Email, subject, body);
                _apiresponse.Result = result;
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Customer Data");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRegister(Register register)
        {
            try
            {
                await _registerRepository.Update(register);
                _apiresponse.Result = register;
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating with Item Record");
            }
        }

        [HttpDelete("{RegisterID}")]
        public async Task<ActionResult> DeleteRegister(int RegisterID)
        {
            try
            {
                var register = await _registerRepository.Get(RegisterID);
                if (register == null)
                {
                    return NotFound($"Item With ID = {RegisterID} Not Found");
                }
                register.IsDeleted = true;
                await _registerRepository.Update(register);
                // await _registerRepository.Delete(RegisterID);
                _apiresponse.Result = $"Student With ID = {RegisterID} Is Deleted";
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting with Item Record");
            }
        }

        [HttpPost("logins")]
        public IActionResult Login(Log register)
        {
            var r = _dbContext.registers.Where(x => x.Email == register.Email && x.Password == register.Password);
            if (r == null)
            {
                return NotFound();
            }
            _apiresponse.Result = r;
            return Ok(_apiresponse);
        }
    }
}

