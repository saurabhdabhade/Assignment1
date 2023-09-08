using Azure;
using LibraryClass.Data;
using LibraryClass.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCApplication1.Models;
using MVCApplication1.Models.ViewModel;
using MVCApplication1.Services;
using MVCApplication1.Services.Iservice;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Drawing.Printing;
using System.Text.Json.Serialization;

namespace MVCApplication1.Controllers
{
    public class ItemController : Controller
    {
        private readonly MyDBContext _myDBContext;
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService, MyDBContext myDBContext)
        {
            _itemService = itemService;
            _myDBContext = myDBContext;
        }

        public async Task<IActionResult> ExcelDownload()
        {
            var response = await _itemService.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                // Set column headers
                worksheet.Cells[1, 1].Value = "ItemName";
                worksheet.Cells[1, 2].Value = "IRate";
                worksheet.Cells[1, 3].Value = "IQuantity";


                // Apply formatting to the header row
                using (var headerRange = worksheet.Cells[1, 1, 1, 5])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Populate data rows
                int rowIndex = 2;
                for (int i=0;i< result.Count;i++)
                {
                    worksheet.Cells[rowIndex, 1].Value = result[i].ItemName;
                    worksheet.Cells[rowIndex, 2].Value = result[i].IRate;
                    worksheet.Cells[rowIndex, 3].Value = result[i].IQuantity;
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
        public async Task<IActionResult> Create_Item(ItemViewModel itemRequest) 
        {
            await _itemService.CreateAsync<APIResponse>(itemRequest);
            TempData["SuccessMessage"] = "Item added successfully!";
            return RedirectToAction("GetallItems", "Item");
        }

        public async Task<IActionResult> GetallItems(int page = 1, int pageSize = 5)
        {
            var response = await _itemService.GetAllAsync<APIResponse>();
            var itemList = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));

            var totalCount = itemList.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var itemsToDisplay = itemList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;

            ViewData["logOut"] = true;
            return View(itemsToDisplay);
        }

        public async Task<IActionResult> Ajax_Call(string ItemName)
        {
            var response = await _itemService.GetAllAsync<APIResponse>();
            var result = JsonConvert.DeserializeObject<List<ItemViewModel>>(Convert.ToString(response.Result));
            var exist = result.Where(x => x.ItemName == ItemName);
            if(exist.Any())
            {
                return Json("Item Name is Already exist ");
            }
            return Json("Valid Item Name");
        }
        public IActionResult Edit(string ItemName)
        {
            var response = _itemService.GetAsync<APIResponse>(ItemName);
            var result = JsonConvert.DeserializeObject<ItemViewModel>(Convert.ToString(response.Result.Result));
            TempData["SuccessMessage"] = "Item Edited successfully!";
            ViewData["logOut"] = true;
            return View(result);
        }

        public async Task<IActionResult> EditItem(ItemViewModel itemRequest)
        {
            if (itemRequest.IQuantity > 0)
            {
                var response = await _itemService.UpdateAsync<APIResponse>(itemRequest);
            }
            return RedirectToAction("GetallItems","Item");
        }
        public async Task<IActionResult> DeleteItem(string itemName)
        {
            await _itemService.DeleteAsync<APIResponse>(itemName);
            return RedirectToAction("GetallItems","Item");
        }
    }
}
