using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Store.Controllers;
using Store.Models;
using Xunit;
using Store.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Store.Tests
{

    public class AdminControllerTests
    {

        [Fact]
        public void AdminController_WithoutArguments_ShouldListToys()
        {

            var mockToyRepository = new Mock<IToyRepository>();

            mockToyRepository.Setup(m => m.GetAll()).Returns(new Toy[] {
                new Toy {ToyId = 1, Name = "T1"},
                new Toy {ToyId = 2, Name = "T2"},
                new Toy {ToyId = 3, Name = "T3"},
            }.AsQueryable());

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var controller = new AdminController(mockToyRepository.Object,
                    mockCategoryRepository.Object);

            var result = GetViewModel<IEnumerable<Toy>>(controller.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("T1", result[0].Name);
            Assert.Equal("T2", result[1].Name);
            Assert.Equal("T3", result[2].Name);
        }




        [Fact]
        public async void AdminController_WithIdToy_ShouldDeleteFromDB()
        {
            var mockToyRepository = new Mock<IToyRepository>();

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var stubCategory = new Category { CategoryId = 1, Name = "C1" };
            var stubToy = new Toy
            {
                ToyId = 1,
                Name = "T1",
                Category = stubCategory,
                CartLines = new List<CartLine>()
            };

            mockToyRepository.Setup(r => r.DeleteById(stubToy.ToyId))
                    .ReturnsAsync(stubToy);

            mockCategoryRepository.Setup(r => r.Categories()).Returns(
                new Category[] { stubCategory }.AsQueryable());

            mockToyRepository.Setup(m => m.GetAll()).Returns(
                new Toy[] { stubToy }.AsQueryable());

            mockToyRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(stubToy);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new AdminController(mockToyRepository.Object,
                        mockCategoryRepository.Object);

            controller.TempData = tempData;

            await controller.Delete(stubToy.ToyId);

            mockToyRepository.Verify(m => m.DeleteById(stubToy.ToyId),
                    Times.Once);
        }


        [Fact]
        public async void AdminController_WithIdToy_ShouldNotDeleteFromDB()
        {
            var mockToyRepository = new Mock<IToyRepository>();

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var stubCategory = new Category { CategoryId = 1, Name = "C1" };
            var stubToy = new Toy
            {
                ToyId = 1,
                Name = "T1",
                Category = stubCategory,
                CartLines = new List<CartLine>()
                {
                    new CartLine()
                }
            };

            mockToyRepository.Setup(r => r.DeleteById(stubToy.ToyId))
                    .ReturnsAsync(stubToy);

            mockCategoryRepository.Setup(r => r.Categories()).Returns(
                new Category[] { stubCategory }.AsQueryable());

            mockToyRepository.Setup(m => m.GetAll()).Returns(
                new Toy[] { stubToy }.AsQueryable());

            mockToyRepository.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(stubToy);

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

            var controller = new AdminController(mockToyRepository.Object,
                        mockCategoryRepository.Object);

            controller.TempData = tempData;

            await controller.Delete(stubToy.ToyId);

            mockToyRepository.Verify(m => m.DeleteById(stubToy.ToyId),
                Times.Never);
        }


        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
