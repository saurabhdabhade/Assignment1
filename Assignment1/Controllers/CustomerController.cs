using Azure;
using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository.IRepository;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Services;
using MVCApplication1.Services.Iservice;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using OfficeOpenXml;
using System.Collections;
using System.Reflection.Metadata;

namespace MVCApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> ExcelDownload()
        {
            var response = await _customerService.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(Convert.ToString(response.Result));

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                // Set column headers
                worksheet.Cells[1, 1].Value = "Cust_Email";
                worksheet.Cells[1, 2].Value = "Cust_FirstName";
                worksheet.Cells[1, 3].Value = "Cust_LastName";
                worksheet.Cells[1, 4].Value = "Cust_Phone";

                // Apply formatting to the header row
                using (var headerRange = worksheet.Cells[1, 1, 1, 5])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Populate data rows
                int rowIndex = 2;
                for (int i = 0; i < result.Count; i++)
                {
                    worksheet.Cells[rowIndex, 1].Value = result[i].Cust_Email;
                    worksheet.Cells[rowIndex, 2].Value = result[i].Cust_FirstName;
                    worksheet.Cells[rowIndex, 3].Value = result[i].Cust_LastName;
                    worksheet.Cells[rowIndex, 4].Value = result[i].Cust_Phone;
                    rowIndex++;
                }


                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
            }
        }

        public IActionResult Create()
        {
            ViewData["logOut"] = true;
            return View();
        }
        public async Task<IActionResult> CreateCustomer(GetCustomerData model)
         {
            List<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();
            var authorsArray = new List<CustomerViewModel>();
            for (int i = 0;  i < model.Cust_Email.Count; i++)
            {
                var customerRequest = new CustomerViewModel()
                {
                    Cust_FirstName = model.Cust_FirstName[i],
                    Cust_LastName = model.Cust_LastName[i],
                    Cust_Phone = model.Cust_Phone[i],
                    Cust_Email = model.Cust_Email[i],
                };
                authorsArray.Add(customerRequest);
               //await _customerService.CreateAsync<APIResponse>(customerRequest);
            }
            await _customerService.CreateAsync1<APIResponse>(authorsArray);
            TempData["SuccessMessage"] = "Customer added successfully!";
            return RedirectToAction("GetAllCustomers", "Customer");

        }

        public async Task<IActionResult> GetAllCustomers(int page = 1, int pageSize = 5)
        {
            var response = await _customerService.GetAllAsync<APIResponse>();
            var customerList = JsonConvert.DeserializeObject<List<CustomerViewModel>>(Convert.ToString(response.Result));

            var totalCount = customerList.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var customersToDisplay = customerList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;

            ViewData["logOut"] = true;
            return View(customersToDisplay);
        }

        public async Task<IActionResult> Ajax_Response(string Cust_Email)
        {
            var response = await _customerService.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<CustomerViewModel>>(Convert.ToString(response.Result));
            var exist = result.Where(x => x.Cust_Email == Cust_Email);

            if (exist.Any())
            {
                return Json("Item Name Already exist ");
            }
            return Json("Valid Item Name");
        }
        public IActionResult Edit(string Cust_Email)
        {
            var response = _customerService.GetAsync<APIResponse>(Cust_Email);
            var result = JsonConvert.DeserializeObject<CustomerViewModel>(Convert.ToString(response.Result.Result));
            TempData["SuccessMessage"] = "Customer Edited successfully!";
            ViewData["logOut"] = true;
            return View(result);
        }
        public async Task<IActionResult> EditCustomer(CustomerViewModel customerRequest)
        {
             await _customerService.UpdateAsync<APIResponse>(customerRequest);
             return RedirectToAction("GetAllCustomers", "Customer");
        }

        public async Task<IActionResult> Delete(string Cust_Email)
        {
            await _customerService.DeleteAsync<APIResponse>(Cust_Email);
            return RedirectToAction("GetAllCustomers", "Customer");
        }

        public async Task<IActionResult> LoginMethod(LoginRequest loginRequestDTO)
        {
            loginRequestDTO.Username = "string";
            loginRequestDTO.Password = "string";
            var data =  await _customerService.Login<APIResponse>(loginRequestDTO);
            var result = JsonConvert.DeserializeObject<LoginResponse>(Convert.ToString(data.Result));
            HttpContext.Session.SetString("Token", result.Token);
            return RedirectToAction("GetAllCustomers", "Customer");
        }
    }
}