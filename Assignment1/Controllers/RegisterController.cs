using LibraryClass.Data;
using LibraryClass.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Models.ViewModel.Services.Iservice;
using Newtonsoft.Json;
using MVCApplication1.Services.Iservice;
using MVCApplication1.Services;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using WebApplication1.Controllers;
using System.Web.Helpers;
using LibraryClass.Models.DTO;
using Azure.Core;
using NuGet.Protocol;
using NuGet.Protocol.Plugins;
using Microsoft.CodeAnalysis.Scripting;


namespace MVCApplication1.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService _service;
        private readonly ICustomerService _customerService;
        private readonly MyDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public object BCrypt { get; private set; }

        public RegisterController(IHttpContextAccessor httpContextAccessor, IRegisterService service, ICustomerService customerService, MyDBContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _service = service;
            _customerService = customerService;
            _dbContext = dbContext;
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> RegisterUser(string email, RegisterViewModel model)
        {
            if (_dbContext.registers.All(x => x.Email != email))
            {
                model.LastPassword1 = "pass1";
                model.LastPassword2 = "pass2";
                model.Password = Encryption.Encrypt(model.Password);
                model.Confirm_Password = Encryption.Encrypt(model.Confirm_Password);

                if (model.Password != model.Confirm_Password)
                {
                    TempData["SuccessMessage"] = "The Password & Confirm Password Must Be Same";
                    return RedirectToAction("Create", "Register");
                }

                await _service.RegisterAsync<APIResponse>(model);
                return RedirectToAction("Logins", "Register");
            }
            
            TempData["SuccessMessage"] = "You Can't Register With Same Email ID As Of Our Record";
            return RedirectToAction("Create", "Register");
        }

        public async Task<IActionResult> GetAllRegisteredCustomers()
        {
            var response = await _service.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<RegisterViewModel>>(Convert.ToString(response.Result));
            ViewData["logOut"] = true;
            ViewData["Logins"] = false;
            ViewData["Register"] = false;
            ViewData["Emails"] = _httpContextAccessor.HttpContext.Session.GetString("Emails");

            return View(result);
        }

        public IActionResult Edit(int RegisterID)
        {
            var response = _service.GetAsync<APIResponse>(RegisterID);
            var result = JsonConvert.DeserializeObject<RegisterViewModel>(Convert.ToString(response.Result.Result));

            return View(result);
        }
        public async Task<IActionResult> EditRegister(RegisterViewModel registerRequest)
        {
            await _service.UpdateAsync<APIResponse>(registerRequest);
            return RedirectToAction("GetAllRegisteredCustomers", "Register");
        }

        public async Task<IActionResult> Delete(int RegisterID)
        {
            await _service.DeleteAsync<APIResponse>(RegisterID);
            return RedirectToAction("GetAllRegisteredCustomers", "Register");
        }

        public IActionResult Logins()
        {
            var email = _httpContextAccessor.HttpContext.Session.GetString("Email");
            ViewData["Email"] = email;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginViewModel loginRequest)
        {
            LoginRequest logiRequest = new LoginRequest();
            logiRequest.Username = loginRequest.Email;
            logiRequest.Password = loginRequest.Password;
            var result1 = _service.Token_Call<APIResponse>(logiRequest);
            var result = await _service.Login<APIResponse>(loginRequest);
            string r = null;
            var rr = _dbContext.registers.FirstOrDefault(x => x.Email == loginRequest.Email);
            rr.Password = Encryption.Decrypt(rr.Password);
            
            if (_dbContext.registers.Where(x => x.Email == loginRequest.Email && rr.Password == loginRequest.Password).IsNullOrEmpty())
            {
                ViewData["LogOut"] = true;
                TempData["SuccessMessage"] = "Please Enter Valid Credentials";
                return RedirectToAction("Logins", "Register");
            }
            else
            {
                // Set the email in the session
                HttpContext.Session.SetString("Email", loginRequest.Email);
                ViewData["LogOut"] = false;
                TempData["SuccessMessage"] = "You Have Logged In successfully!...";
                return RedirectToAction("GetAllRegisteredCustomers", "Register");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            return RedirectToAction("Logins", "Register");
        }

        public async Task<IActionResult> ForgotPass()
        {
            List<string> Emails = new List<string>();
            var response1 = await _customerService.GetAllAsync<APIResponse>();
            var result1 = JsonConvert.DeserializeObject<List<CustomerViewModel>>(Convert.ToString(response1.Result));
            ViewData["customers"] = result1;
            foreach (var item in result1)
            {
                Emails.Add(item.Cust_Email);
            }
            ViewBag.Emails = Emails;
            return View();
        }

        public async Task<IActionResult> ForgotPasswordEmailCheck(string email)
        {

            var result = _dbContext.registers.FirstOrDefault(x => x.Email == email);
            if (result == null)
            {
                return RedirectToAction("ForgotPass", "Register");
            }
            var resetPassUrl = Url.Action("ResetPass", "Register", new { email = result.Email });

            // Return the URL as a JSON response
            return Json(new { redirectTo = resetPassUrl });
        }

        public async Task<IActionResult> ResetPass(string email)
        {
            var result = _dbContext.registers.FirstOrDefault(x => x.Email == email);
            ViewData["email"] = result.Email;
            return View(new RegisterViewModel { Email = email });
        }

        public async Task<IActionResult> ResetPassword(string email, string newPassword, string confirmPassword)
        {
            RegisterViewModel reg = new RegisterViewModel();
            var result = _dbContext.registers.FirstOrDefault(x => x.Email == email);
            string vv = result.Password;
            if (newPassword == confirmPassword && newPassword != result.LastPassword1 
                && newPassword != result.LastPassword2)
            {
                String subject = "Congratulations!";
                String body = "Congratulations, Your Password Is Updated Successfully!...";
                reg.LastPassword1 = newPassword;
                reg.LastPassword2 = result.Password;
                reg.Password = Encryption.Encrypt(newPassword);
                reg.Confirm_Password = Encryption.Encrypt(newPassword);
                reg.Email = email;
                reg.Last_Name = result.Last_Name;
                reg.First_Name = result.First_Name;
                reg.RegisterID = result.RegisterID;
                await _service.UpdateAsync<RegisterViewModel>(reg);
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(reg.Email, subject, body);
            }
            else if (newPassword == result.LastPassword1 && newPassword == result.LastPassword2 
                && vv == newPassword && vv == confirmPassword)
            {
                TempData["SuccessMessage"] = "Your Password Should Not Be Matched With Your Last Two Passwords.";
                return RedirectToAction("ResetPass", "Register");
            }
            TempData["SuccessMessage"] = "Your Password Is Updated successfully!...";
            return RedirectToAction("Logins", "Register");
        }
    }
}

