using System.Linq;
using Store.Models;
using Xunit;

namespace Store.Tests
{

    public class CartTests
    {
        [Fact]
        public void Cart_WithToy_ShouldReturnArrayWithToy()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1" };
            var toy2 = new Toy { ToyId = 2, Name = "T2" };

            var results = CreateCart(toy1, toy2);

            Assert.Equal(2, results.Length);
        }

        [Fact]
        public void Cart_WithToy_ShouldReturnArrayWithFirstToy()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1" };
            var toy2 = new Toy { ToyId = 2, Name = "T2" };

            var results = CreateCart(toy1, toy2);

            Assert.Equal(toy1, results[0].Toy);
        }

        [Fact]
        public void Cart_WithToy_ShouldReturnArrayWithSecondToy()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1" };
            var toy2 = new Toy { ToyId = 2, Name = "T2" };

            var results = CreateCart(toy1, toy2);

            Assert.Equal(toy2, results[1].Toy);
        }

        private static CartLine[] CreateCart(Toy toy1, Toy toy2,
                 int count1 = 1, int count2 = 1)
        {
            var cart = new Cart();

            cart.AddItem(toy1, count1);
            cart.AddItem(toy2, count2);
            var results = cart.Lines.ToArray();

            return results;
        }

        [Fact]
        public void Cart_WithToysQuantity_ShouldSetQuantity()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1" };
            var toy2 = new Toy { ToyId = 2, Name = "T2" };

            var cart = CreateCart(toy1, toy2, 1, 6);

            var results = cart.ToList()
                .OrderBy(c => c.Toy.ToyId).ToArray();

            Assert.Equal(1, results[0].Quantity);
            Assert.Equal(6, results[1].Quantity);
        }

        [Fact]
        public void Cart_WithId_ShouldRemoveCartLine()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1" };
            var toy2 = new Toy { ToyId = 2, Name = "T2" };

            var cart = new Cart();

            cart.AddItem(toy1, 1);
            cart.AddItem(toy2, 1);

            cart.RemoveLine(toy1.ToyId);

            Assert.Empty(cart.Lines.Where(c => c.Toy.ToyId == toy1.ToyId));
        }


        [Fact]
        public void Cart_WithId_ShouldRemoveCartLineAndHaveLessSize()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1" };
            var toy2 = new Toy { ToyId = 2, Name = "T2" };

            var cart = new Cart();

            cart.AddItem(toy1, 1);
            cart.AddItem(toy2, 1);

            cart.RemoveLine(toy1.ToyId);

            Assert.Equal(1, cart.Lines.Count());
        }

        [Fact]
        public void Cart_WithoutArguments_ShouldReturnTotalPrice()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1", Price = 2.5M };
            var toy2 = new Toy { ToyId = 2, Name = "T2", Price = 6M };

            var cart = new Cart();

            cart.AddItem(toy1, 1);
            cart.AddItem(toy2, 1);

            var result = cart.ComputeTotalValue();

            Assert.Equal(8.5M, result);
        }

        [Fact]
        public void Cart_WithoutArguments_ShouldClearCartLines()
        {
            var toy1 = new Toy { ToyId = 1, Name = "T1", Price = 2.5M };
            var toy2 = new Toy { ToyId = 2, Name = "T2", Price = 6M };

            var cart = new Cart();

            cart.AddItem(toy1, 1);
            cart.AddItem(toy2, 1);

            cart.Clear();

            // Assert
            Assert.Empty(cart.Lines);
        }
    }
}
