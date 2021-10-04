using APlus.API.Controllers;
using APlus.API.Data;
using APlus.Application.Interface;
using APlus.DTO.ReadDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace APlus.Test
{
    public class InventoryControllerTest
    {
        private InventoryController _controller;

        public InventoryControllerTest(IInventoryItemService inventory, UserManager<APlusUser> userManager, IHttpContextAccessor httpContext)
        {
            _controller = new InventoryController(inventory, userManager, httpContext);
        }

        [Fact]
        public void GetAllTest()
        {
            //Arrange


            //Act
            var result = _controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<InventoryItemDTO>>(list.Value);

            //var listItems = list.Value as List<InventoryItemDTO>;

            //Assert.Equal(5, )
        }
    }
}
