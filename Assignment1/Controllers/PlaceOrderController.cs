using AutoMapper;
using Azure;
using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Services;
using MVCApplication1.Services.Iservice;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Helpers;

namespace MVCApplication1.Controllers
{
    public class PlaceOrderController : Controller
    {
        private readonly IPlaceOrderService _placeOrderservice;
        private readonly IItemService _itemService;
        private readonly ICustomerService _customerService;

        public PlaceOrderController(IPlaceOrderService placeOrderservice, IItemService itemService, ICustomerService customerService)
        {
            _placeOrderservice = placeOrderservice;
            _itemService = itemService;
            _customerService = customerService;
        }

        public async Task<IActionResult> ExcelDownload()
        {
            var response = await _placeOrderservice.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<PlaceOrderViewModel>>(Convert.ToString(response.Result));

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                // Set column headers
                worksheet.Cells[1, 1].Value = "OrderID";
                worksheet.Cells[1, 2].Value = "ItemName";
                worksheet.Cells[1, 3].Value = "Cust_Email";
                worksheet.Cells[1, 4].Value = "IQuantity";
                worksheet.Cells[1, 5].Value = "IRate";

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
                    worksheet.Cells[rowIndex, 1].Value = result[i].OrderID;
                    worksheet.Cells[rowIndex, 2].Value = result[i].ItemName;
                    worksheet.Cells[rowIndex, 3].Value = result[i].Cust_Email;
                    worksheet.Cells[rowIndex, 4].Value = result[i].IQuantity;
                    worksheet.Cells[rowIndex, 5].Value = result[i].IRate;
                    rowIndex++;
                }

                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
            }
        }

        public async Task<IActionResult> CreateAsync()
        {
            List<string> itemsName = new List<string>();
            var response = await _itemService.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));

            ViewData["items"] = result;
            foreach (var item in result)
            {
                itemsName.Add(item.ItemName);
            }
            ViewBag.Item = itemsName;

            List<string> Emails = new List<string>();
            var response1 = await _customerService.GetAllAsync<APIResponse>();
            var result1 = JsonConvert.DeserializeObject<List<CustomerViewModel>>(Convert.ToString(response1.Result));
            ViewData["customers"] = result1;
            foreach (var item in result1)
            {
                Emails.Add(item.Cust_Email);
            }
            ViewBag.CustomerEmails = Emails;
            ViewData["logOut"] = true;
            return View();
        }
         
        public async Task<IActionResult> CreateOrder(GetData getdata)
        {
            
            var data = new ItemViewModel();
            string userIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            getdata.ipAddress = userIpAddress;
            List<PlaceOrderViewModel> list = new List<PlaceOrderViewModel>();
            for (int i = 0; i < getdata.ItemName.Count; i++)
            {
                var placeOrderRequest = new PlaceOrderViewModel()
                {
                    ItemName = getdata.ItemName[i],
                    IQuantity = getdata.IQuantity[i],
                    IRate = getdata.IRate[i],
                    Cust_Email = getdata.Cust_Email[i],
                    ipAddress = getdata.ipAddress,
                    EventDateTime = getdata.EventDateTime
                };
                var response = _itemService.GetAsync<APIResponse>(getdata.ItemName[i]);
                data = JsonConvert.DeserializeObject<ItemViewModel>(Convert.ToString(response.Result.Result));
                int present = data.IQuantity;
                data.IQuantity = present - getdata.IQuantity[i];
                await _itemService.UpdateAsync<APIResponse>(data);
                await _placeOrderservice.CreateAsync<APIResponse>(placeOrderRequest);
            }   
            TempData["SuccessMessage"] = "Order Placed successfully!";
            return RedirectToAction("GetAllOrders", "PlaceOrder");
        }

        public async Task<IActionResult> GetAllOrders(int page = 1, int pageSize = 5)
        {
            var response = await _placeOrderservice.GetAllAsync<APIResponse>();
            var placeOrderList = JsonConvert.DeserializeObject<List<PlaceOrderViewModel>>(Convert.ToString(response.Result));

            var totalCount = placeOrderList.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var placeOrdersToDisplay = placeOrderList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;

            ViewData["logOut"] = true;
            return View(placeOrdersToDisplay);
        }

        public async Task<IActionResult> Ajax_item(string ItemName)
        {
            var response = await _itemService.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));
            var itemdata = result.Where(x => x.ItemName == ItemName).FirstOrDefault();
            return Json(itemdata);
        }
        
        public IActionResult Edit(int OrderID)
        {
            var response = _placeOrderservice.GetAsync<APIResponse>(OrderID);
            var result = JsonConvert.DeserializeObject<PlaceOrderViewModel>(Convert.ToString(response.Result.Result));
            ViewData["logOut"] = true;
            return View(result);
        }

        public async Task<IActionResult> EditOrder(PlaceOrderViewModel placeOrderRequest)
        {
            await _placeOrderservice.UpdateAsync<APIResponse>(placeOrderRequest);
            return RedirectToAction("GetAllOrders", "PlaceOrder");
        }

        public async Task<IActionResult> CancleOrder(int orderID)
        {
           var data =  await _placeOrderservice.GetAsync<APIResponse>(orderID);
            await _placeOrderservice.DeleteAsync<APIResponse>(orderID);
            
            var result = JsonConvert.DeserializeObject<PlaceOrderViewModel>(Convert.ToString(data.Result));
            ItemViewModel itemViewModel = new ItemViewModel();
            var response = _itemService.GetAsync<APIResponse>(result.ItemName);
            itemViewModel = JsonConvert.DeserializeObject<ItemViewModel>(Convert.ToString(response.Result.Result));
            int present = itemViewModel.IQuantity;
            itemViewModel.IQuantity = present + result.IQuantity;
            await _itemService.UpdateAsync<APIResponse>(itemViewModel);
            TempData["SuccessMessage"] = "Your Order Is Cancelled & Mail Has Sent To Your Mail ID Of Cancelling Order";
            return RedirectToAction("GetAllOrders", "PlaceOrder");
        }
    }
}
