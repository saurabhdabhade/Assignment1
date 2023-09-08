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
    public class CustomerControllerTests
    {
        private Mock<ICustomerRepository> _mockCustomerRepository;
        private Mock<IMapper> _mockMapper;
        private CustomerController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CustomerController(_mockCustomerRepository.Object, _mockMapper.Object);
        }

        [TestMethod()]
        public async Task GetCustomersTest()
        {
             var customers = new List<Customer> { new Customer(), new Customer() }; // Sample customers
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(customers, response.Result);
        }

        /*[TestMethod()]
        public void GetCustomerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateCustomerTest()
        {
            Assert.Fail();
        }*/

        [TestMethod()]
        public async Task UpdateCustomerTest()
        {
            var updateCustomer = new Customer
            {
                Cust_Email = "dabhades1999@gmail.com\t",
                Cust_FirstName = "Mr. Saurabh\t",
                Cust_LastName = "Dabhade",
                Cust_Phone = 9922554927
            };
            _mockCustomerRepository.Setup(repo => repo.Update(updateCustomer)).Returns(Task.FromResult<Customer>(updateCustomer));

            // Act
            var result = await _controller.UpdateCustomer(updateCustomer) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(updateCustomer, response.Result);
        }

        [TestMethod()]
        public async Task DeleteCustomerTest()
        {
            {
                var cust_Email = "dabhades1999@gmail.com";
                var customer = new Customer
                {
                    Cust_Email = "dabhades1999@gmail.com",
                    Cust_FirstName = "Mr. Saurabh",
                    Cust_LastName = "Dabhade",
                    Cust_Phone = 9922554927
                };
                _mockCustomerRepository.Setup(repo => repo.Get(It.IsAny<string>())).ReturnsAsync(customer);
                _mockCustomerRepository.Setup(repo => repo.Delete(It.IsAny<string>())).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.DeleteCustomer(cust_Email) as OkObjectResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
                var response = result.Value as APIResponse;
                Assert.IsNotNull(response);
                Assert.IsNotNull(response.Result);
                Assert.AreEqual($"Student With ID = {cust_Email} Is Deleted", response.Result);
            }
        }       
    }
}