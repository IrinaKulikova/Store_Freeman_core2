using Moq;
using Store.Controllers;
using Store.Models;
using Store.Repositories.Interfaces;
using Store.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Store.Tests
{
    public class ToyControllerTests
    {
        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnCurrentPage()
        {
            var result = GetViewModel();
            var paging = result.PagingInfo;

            Assert.Equal(2, paging.CurrentPage);
        }

        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnItemsPerPage()
        {
            var result = GetViewModel();
            var paging = result.PagingInfo;

            Assert.Equal(2, paging.ItemsPerPage);
        }


        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnTotalItems()
        {
            var result = GetViewModel();
            var paging = result.PagingInfo;

            Assert.Equal(5, paging.TotalItems);
        }


        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnTotalPages()
        {
            var result = GetViewModel();
            var paging = result.PagingInfo;

            Assert.Equal(3, paging.TotalPages);
        }

        private ToysListViewModel GetViewModel()
        {
            var mockToyRepository = new Mock<IToyRepository>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();

            mockToyRepository.Setup(m => m.GetAll()).Returns((new List<Toy>()
            {
                new Toy{ ToyId = 1, Name = "T1"},
                new Toy{ ToyId = 2,  Name= "T2"},
                new Toy{ ToyId = 3,  Name= "T3"},
                new Toy{ ToyId = 4,  Name= "T4"},
                new Toy{ ToyId = 5,  Name= "T5"}
            }).AsQueryable());

            var controller = new ToyController(mockToyRepository.Object,
                                               mockCategoryRepository.Object);
            controller.PageSize = 2;

            var result = controller.List(null, 2);
            var listViewModel = result.Result.ViewData.Model as ToysListViewModel;

            return listViewModel;
        }

        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnToyName()
        {
            var result = GetViewModel();

            var toys = result.Toys.ToArray();

            Assert.Equal("T3", toys[0].Name);
        }


        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnListLength()
        {
            var result = GetViewModel();

            var toys = result.Toys.ToArray();

            Assert.Equal(2, toys.Length);
        }

        [Fact]
        public async void ToyController_WithCategoryAndPage_ShouldReturnArrayToys()
        {
            var result = await GetToysArray();

            Assert.Equal(3, result.Length);
        }

        [Fact]
        public async void ToyController_WithCategoryAndPage_ShouldReturnFirstItemToyWithCategory()
        {
            var result = await GetToysArray();

            Assert.True(result[0].Name == "T1" && result[0].Category.Name == "Test1");
        }

        [Fact]
        public async void ToyController_WithCategoryAndPage_ShouldReturnSecondItemToyWithCategory()
        {
            var result = await GetToysArray();

            Assert.True(result[1].Name == "T3" && result[0].Category.Name == "Test1");
        }

        [Fact]
        public async void ToyController_WithCategoryAndPage_ShouldReturnThirdItemToyWithCategory()
        {
            Toy[] result = await GetToysArray();

            Assert.True(result[2].Name == "T5" && result[0].Category.Name == "Test1");
        }

        private async Task<Toy[]> GetToysArray()
        {
            var mock = new Mock<IToyRepository>();
            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var category1 = new Category() { Name = "Test1" };
            var category2 = new Category() { Name = "Test2" };

            mockCategoryRepository.Setup(c => c.FindByName(category1.Name))
                    .ReturnsAsync(category1);

            var toys = new Toy[] {
                new Toy{ ToyId = 1, Name = "T1", Category = category1},
                new Toy{ ToyId = 2,  Name= "T2", Category = category2},
                new Toy{ ToyId = 3,  Name= "T3", Category = category1},
                new Toy{ ToyId = 4,  Name= "T4", Category = category2},
                new Toy{ ToyId = 5,  Name= "T5", Category = category1}
            };

            category1.Toys = toys.Where(t => t.Category == category1).ToList();

            mock.Setup(m => m.GetAll()).Returns(toys.AsQueryable());

            var controller = new ToyController(mock.Object, mockCategoryRepository.Object);
            controller.PageSize = 3;

            var model = await controller.List("Test1", 1);

            var result = (model.ViewData.Model as ToysListViewModel).Toys.ToArray();
            return result;
        }
    }
}
