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
using LibraryClass.Models.DTO;
using NUnit.Framework.Interfaces;
using LibraryClass.Repository;

namespace WebApplication1.Tests
{
    [TestClass()]
    public class ItemControllerTests
    {
        private Mock<IItemRepository> _mockItemRepository;
        private Mock<IMapper> _mockMapper;
        private ItemController _controller;

        public ItemControllerTests()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ItemController(_mockItemRepository.Object, _mockMapper.Object);
        }
        
        [TestMethod()]
        public async Task GetItemsTest()
        {
            var items = new List<Item> { new Item(), new Item() }; // Sample items
            _mockItemRepository.Setup(repo => repo.GetAll()).ReturnsAsync(items);

            // Act
            var result = await _controller.GetItems() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(items, response.Result);
        }

        [TestMethod()]
        public async Task GetItemTest()
        {
            string itemName = "Table Fan"; // Replace with the actual item name for your test
            var expectedStatusCode = 200; // Replace with the expected status code

            // Mock the behavior of your repository
            _mockItemRepository.Setup(repo => repo.Get(itemName)).ReturnsAsync(new Item());

            // Act
            var result = await _controller.GetItem(itemName);

            // Assert
            var objectResult = (OkObjectResult)result.Result;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(expectedStatusCode, objectResult.StatusCode);
        }

        /*[TestMethod()]
        public async Task CreateItemTest()
        {
            var itemDTO = new ItemDTO
            {
                ItemName = "Table Fan",
                IRate = 1200,
                IQuantity = 10, 
                EventDateTime = DateTime.Now
            };

            var mappedItem = new Item
            {
                ItemName = "Table Fan",
                IRate = 1200, 
                IQuantity = 10,
                EventDateTime = DateTime.Now
            };

            _mockMapper.Setup(mapper => mapper.Map<Item>(itemDTO)).Returns(mappedItem);

            // Act
            var result = await _controller.CreateItem(itemDTO) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);

            // Use Assert.AreEqual to compare itemDTO and response.Result
            Assert.AreEqual(itemDTO.ItemName, response.Result.ItemName);
            Assert.AreEqual(itemDTO.IRate, response.Result.IRate);
            Assert.AreEqual(itemDTO.IQuantity, response.Result.IQuantity);
        }*/

        [TestMethod()]
        public async Task UpdateItemTest()
        {
            var updatedItem = new Item {
                ItemName = "Table Fan",
                IRate = 1200,
                IQuantity = 25
            };
            _mockItemRepository.Setup(repo => repo.Update(updatedItem)).Returns(Task.FromResult<Item>(updatedItem));

            // Act
            var result = await _controller.UpdateItem(updatedItem) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(updatedItem, response.Result);
        }

        [TestMethod()]
        public async Task DeleteItemTest()
        {
            var itemName = "Washing Machine";
            var item = new Item {
                ItemName = "Washing Machine",
                IRate = 15000,
                IQuantity = 15
            };
            _mockItemRepository.Setup(repo => repo.Get(It.IsAny<string>())).ReturnsAsync(item);
            _mockItemRepository.Setup(repo => repo.Delete(It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteItem(itemName) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual($"Student With ID = {itemName} Is Deleted", response.Result);
        }
    }
}