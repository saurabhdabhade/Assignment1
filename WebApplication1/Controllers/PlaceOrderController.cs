using AutoMapper;
using LibraryClass.Data;
using LibraryClass.Models;
using LibraryClass.Models.DTO;
using LibraryClass.Repository;
using LibraryClass.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/PlaceOrderController")]
    public class PlaceOrderController : ControllerBase
    {
        private readonly IPlaceOrder _placeOrderRepository;
        private readonly IMapper _mapper;
        private readonly APIResponse _apiResponse = new APIResponse();


        public PlaceOrderController(IPlaceOrder placeOrderRepository, IMapper mapper)
        {
            _placeOrderRepository = placeOrderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetPlaceOrders()
        {
            try
            {
                var allOrders = await _placeOrderRepository.GetAll();
                _apiResponse.Result = allOrders;
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data From The Database");
            }
        }

        [HttpGet("{OrderID}")]
        public async Task<ActionResult<PlaceOrder>> GetPlaceOrder(int OrderID)
        {
            try
            {
                var result = await _placeOrderRepository.Get(OrderID);
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

        [HttpPost]
        public async Task<ActionResult<PlaceOrder>> CreatePlaceOrder(PlaceOrderDTO placeOrderDTO)
        {
            try
            {
                String subject = "Welcome";
                String body = "Welcome, Your Oredr ID Number " + placeOrderDTO.OrderID + " With Bill Of " + placeOrderDTO.IRate
                    + " For The Item " + placeOrderDTO.ItemName + " With The " + placeOrderDTO.IQuantity + " Quantity Is Placed Successfully...";

                var result = _mapper.Map<PlaceOrder>(placeOrderDTO);
                await _placeOrderRepository.Add(result);
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(placeOrderDTO.Cust_Email, subject, body);
                _apiResponse.Result = result;
                //_apiResponse.Result = customer;
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Creating New PlaceOrder Record");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlaceOrder(PlaceOrder placeOrder)
        {
            try
            {
                await _placeOrderRepository.Update(placeOrder);
                _apiResponse.Result = placeOrder;
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating New PlaceOrder Record");
            }
        }

        [HttpDelete("{OrderID}")]
        public async Task<ActionResult> DeletePlaceOrder(int OrderID)
        {
            PlaceOrderDTO placeOrderDTO = new PlaceOrderDTO();
            try
            {
                //string userIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                String subject = "Apology";
                String body = "Ohh Sorry!, Your Order Is Cancelled...";
                var placeOrder = await _placeOrderRepository.Get(OrderID);
                if (placeOrder == null)
                {
                    return NotFound($"PlaceOrder With ID = {OrderID} Not Found");
                }
                //find placeOrder by orderid
                var rr = await _placeOrderRepository.Get(OrderID);
                await _placeOrderRepository.Delete(OrderID);
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(rr.Cust_Email, subject, body);
                _apiResponse.Result = $"Student With ID = {OrderID} Is Deleted";
                return Ok(_apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting with PlaceOrder Record");
            }
        }
    }
}
