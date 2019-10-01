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
        public void ToyController_WithMockRepositoryPage3_ShouldReturnPaginationInfo()
        {
            ToysListViewModel result = GetViewModel();

            PagingInfo paging = result.PagingInfo;
            Assert.Equal(2, paging.CurrentPage);
            Assert.Equal(2, paging.ItemsPerPage);
            Assert.Equal(5, paging.TotalItems);
            Assert.Equal(3, paging.TotalPages);
        }

        private ToysListViewModel GetViewModel()
        {
            Mock<IToyRepository> mockToyRepository = new Mock<IToyRepository>();
            mockToyRepository.Setup(m => m.Toys()).Returns((new List<Toy>()
            {
                new Toy{ ToyId = 1, Name = "T1"},
                new Toy{ ToyId = 2,  Name= "T2"},
                new Toy{ ToyId = 3,  Name= "T3"},
                new Toy{ ToyId = 4,  Name= "T4"},
                new Toy{ ToyId = 5,  Name= "T5"}
            }).AsQueryable());

            var controller = new ToyController(mockToyRepository.Object);
            controller.PageSize = 2;

            var result = controller.List(0, 2);
            var listViewModel = result.ViewData.Model as ToysListViewModel;
            return listViewModel;
        }

        [Fact]
        public void ToyController_WithMockRepositoryPage3_ShouldReturnListToys()
        {
            ToysListViewModel result = GetViewModel();

            Toy[] toys = result.Toys.ToArray();
            Assert.Equal("T3", toys[0].Name);
            Assert.Equal("T4", toys[1].Name);
            Assert.Equal(2, toys.Length);
        }
    }
}
