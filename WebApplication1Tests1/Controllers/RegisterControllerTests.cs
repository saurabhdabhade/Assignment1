using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryClass.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryClass.Repository.IRepository;
using Moq;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Tests
{
    [TestClass()]
    public class RegisterControllerTests
    {
        private RegisterController _registerController;
        private Mock<IRegisterRepository> _registerRepository;
        private Mock<IMapper> _mapper;
        
        public RegisterControllerTests(RegisterController registerController)
        {
            _registerRepository = new Mock<IRegisterRepository>();
            _mapper = new Mock<IMapper>();
            _registerController = registerController;      
         }

        [TestMethod()]
        public async Task GetRegisterTest()
        {
            int RegisteredID = 1; 
            var expectedStatusCode = 200; // Replace with the expected status code

            // Mock the behavior of your repository
             _registerRepository.Setup(repo => repo.Get(RegisteredID)).ReturnsAsync(new Register());

            // Act
            var result = await _registerController.GetRegister(RegisteredID);

            // Assert
            var objectResult = (OkObjectResult)result.Result;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(expectedStatusCode, objectResult.StatusCode);
        }

        [TestMethod()]
        public async void GetRegisterdCustomersTest()
        {
            var expectedRegisters = new List<Register> { new Register() , new Register() };
            _registerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedRegisters);

            // Act
            var result = await _registerController.GetRegisterdCustomers();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var apiResponse = okResult.Value as APIResponse; // Replace with your actual ApiResponse class
            Assert.IsNotNull(apiResponse);
            CollectionAssert.AreEqual(expectedRegisters, (System.Collections.ICollection)apiResponse.Result);
        }

        [TestMethod()]
        public void RegisterUsersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async void UpdateRegisterTest()
        {
            int RegisterID = 1;
            var register = new Register()
            {
                RegisterID = 1,
                First_Name = "Manswi",
                Last_Name = "Shirbhate",
                Password = "Sanu@123#",
                Confirm_Password = "Sanu@123#",
                LastPassword1 = "pass1",
                LastPassword2 = "pass2",
            };
            _registerRepository.Setup(repo => repo.Update(register)).Returns(Task.FromResult<Register>(register));

            // Act
            var result = await _registerController.UpdateRegister(register) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(register, response.Result);
        }

        [TestMethod()]
        public async void DeleteRegisterTest()
        {
            int RegisterID = 1;
            var register = new Register() 
            {
                RegisterID = RegisterID,
                First_Name = "Manswi",
                Last_Name = "Shirbhate",
                Password = "Sanu@123#",
                Confirm_Password = "Sanu@123#",
                LastPassword1 = "pass1",
                LastPassword2 = "pass2",
            };
            
            _registerRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(register);
            _registerRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await _registerController.DeleteRegister(RegisterID) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var response = result.Value as APIResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual($"Candidate With ID = {RegisterID} Is Deleted", response.Result);
        }
    }
}