using AutoMapper;
using Castle.Core.Resource;
using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository;
using LibraryClass.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Metadata;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/ItemController")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiresponse = new APIResponse();
        public ItemController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var resultItem = new List<Item>();
                var allItems = await _itemRepository.GetAll();
                foreach(var items in allItems)
                {
                    if(items.deleteflag == false)
                    {
                        resultItem.Add(items);
                    }
                }
                _apiresponse.Result = resultItem;
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data From The Database");
            }
        }


        [HttpGet("{ItemName}")]
        public async Task<ActionResult<Item>> GetItem(string ItemName)
        {
            try
            {
                var result = await _itemRepository.Get(ItemName);
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

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(List<ItemDTO> item)
        {
            ItemDTO custDTO = new ItemDTO();
            try
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (item[i].IQuantity > 0)
                    {
                        ItemDTO custDTO1 = new ItemDTO();
                        custDTO1.ItemName = item[i].ItemName;
                        custDTO1.IRate = item[i].IRate;
                        custDTO1.IQuantity = (int)item[i].IQuantity;
                        custDTO1.EventDateTime = item[i].EventDateTime;
                        custDTO = custDTO1;
                        var result = _mapper.Map<Item>(custDTO);
                        await _itemRepository.Add(result);
                        _apiresponse.Result = result;
                    }
                }
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Creating New Item Record");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(Item item)
        {
            try
            {
                await _itemRepository.Update(item);
                _apiresponse.Result = item;
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating with Item Record");
            }
        }

        [HttpDelete("{ItemName}")]
        public async Task<ActionResult> DeleteItem(string ItemName)
        {
            try
            {
                var item = await _itemRepository.Get(ItemName);
                if (item == null)
                {
                    return NotFound($"Item With ID = {ItemName} Not Found");
                }
                item.deleteflag = true;
                await _itemRepository.Update(item);
                //await _itemRepository.Delete(ItemName);
                _apiresponse.Result = $"Student With ID = {ItemName} Is Deleted";
                return Ok(_apiresponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting with Item Record");
            }
        }
    }
}
