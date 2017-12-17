<<<<<<< HEAD
using System;
using Xunit;
using T4TCase.Controllers;
using T4TCase.Data;
using T4TCase.Model;
=======
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using T4TCase.Controllers;
using T4TCase.Data;
using Xunit;
>>>>>>> FlorianDev

namespace XUnitTestT4TCase
{
    public class UnitTest1
    {
<<<<<<< HEAD
        
        [Fact]
        public void Test1()
        {
            //var controller = new OrderController();
=======
        [Fact]
        public void Values_Get_All()
        {
            //set options for dbcontext
            var dbOption = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=T4TCase.mdf;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            //set controller
            var controller = new OrderController(new DatabaseContext(dbOption));

            //set fake authentication
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "florian")
            }, "someAuthTypeName"))
                }
            }; ;

            //get result from controller
            var result =  controller.Order();

            //check if the return type is a viewresult
            Assert.IsType<ViewResult>(result);
>>>>>>> FlorianDev
        }
    }
}
