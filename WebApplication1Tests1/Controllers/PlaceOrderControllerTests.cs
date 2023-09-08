using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryClass.Repository.IRepository;
using Moq;
using LibraryClass.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Tests
{
    [TestClass()]
    public class PlaceOrderControllerTests
    {
        private Mock<IPlaceOrder> _mockplaceOrderRepository;
        private Mock<IMapper> _mockMapper;
        private PlaceOrderController _controller;

        public PlaceOrderControllerTests()
        {
            _mockplaceOrderRepository = new Mock<IPlaceOrder>();
            _mockMapper = new Mock<IMapper>();
            _controller = new PlaceOrderController(_mockplaceOrderRepository.Object, _mockMapper.Object);
        }

        [TestMethod()]
        public async Task GetPlaceOrdersTest()
        {
            var items = new List<PlaceOrder> { new PlaceOrder(), new PlaceOrder() }; // Sample items
            _mockplaceOrderRepository.Setup(repo => repo.GetAll()).ReturnsAsync(items);

            // Act
            var result = await _controller.GetPlaceOrders() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(items, response.Result);
        }

        /*[TestMethod()]
        public void GetPlaceOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePlaceOrderTest()
        {
            Assert.Fail();

        }*/

        [TestMethod()]
        public async Task UpdatePlaceOrderTest()
        {
            var placeOrder = new PlaceOrder
            {
                OrderID = 1038,
                ItemName = "Celling Fan",
                Cust_Email = "dabhades1999@gmail.com",
                IQuantity = 2,
                IRate = 2600
            };
            _mockplaceOrderRepository.Setup(repo => repo.Update(placeOrder)).Returns(Task.FromResult<PlaceOrder>(placeOrder));

            // Act
            var result = await _controller.UpdatePlaceOrder(placeOrder) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(placeOrder, response.Result);
        }

        [TestMethod()]
        public async Task DeletePlaceOrderTest()
        {
            var orderId = 1038;
            var placeOrder = new PlaceOrder
            {
                OrderID = 1038,
                ItemName = "Celling Fan",
                Cust_Email = "dabhades1999@gmail.com",
                IQuantity = 2,
                IRate = 2600
            };
            _mockplaceOrderRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(placeOrder);
            _mockplaceOrderRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);


            // Act
            var result = await _controller.DeletePlaceOrder(orderId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual($"Student With ID = {orderId} Is Deleted", response.Result);
        }
    }
}