using Microsoft.AspNetCore.Mvc;
using Moq;
using Store.Controllers;
using Store.Models;
using Store.Repositories.Interfaces;
using Xunit;

namespace Store.Tests
{

    public class OrderControllerTests
    {
        [Fact]
        public async void OrderController_WithEmptyOrder_ShouldNotBeValid()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();
            var orderController = new OrderController(mock.Object, cart);

            var result = await orderController.Checkout(order) as ViewResult;

            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async void OrderController_WithEmptyOrder_ShouldNotSaveOrder()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            var order = new Order();
            var orderController = new OrderController(mock.Object, cart);

            var result = await orderController.Checkout(order) as ViewResult;

            mock.Verify(m => m.SaveOrUpdate(It.IsAny<Order>()), Times.Never);
        }


        [Fact]
        public async void OrderController_WithValidOrder_SholdSaveOrder()
        {
            var mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Toy(), 1);
            var orderController = new OrderController(mock.Object, cart);

            var result = await orderController
                   .Checkout(new Order()) as RedirectToActionResult;

            mock.Verify(m => m.SaveOrUpdate(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
