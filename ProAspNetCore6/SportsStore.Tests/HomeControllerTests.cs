using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Can_Use_Repository()
    {
        //Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

        mock.Setup(m => m.Products).Returns( (
                new Product[]
                {
                    new Product{ ProductID = 1, Name = "P1"},
                    new Product{ ProductID = 2, Name = "P2"}
                }
            ).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);

        //Act
        ProductsListViewModel viewModel = 
            (controller.Index(null) as ViewResult)?.ViewData.Model as ProductsListViewModel;

        IEnumerable<Product> products = viewModel.Products;

        //Assert
        Product[] prodArray = products?.ToArray() ?? Array.Empty<Product>();

        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);


    } 
    [Fact]
    public void Can_Paginate()
    {
        //Adding Pagination - Schritt 02

        //Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((
            new Product[]
            {
                new Product{ ProductID = 1, Name = "P1"},
                new Product{ ProductID = 2, Name = "P2"},
                new Product{ ProductID = 3, Name = "P3"},
                new Product{ ProductID = 4, Name = "P4"},
                new Product{ ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);
        controller.PageSize = 3;

        //Act
        ProductsListViewModel model = 
            (controller.Index(null, 2) as ViewResult).Model as ProductsListViewModel;

        // Assert
        Product[] prodArray = model.Products.ToArray() ?? Array.Empty<Product>();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }

    // ...ensure that the controller sends the correct pagination data to the view
    [Fact]
    public void Can_Send_Pagination_View_Model()
    { 
        //Arrange
        Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
        mockRepo.Setup(mockRepo => mockRepo.Products).Returns((new Product[]
        {
            new Product { ProductID = 1, Name = "P1"},
            new Product { ProductID = 2, Name = "P2"},
            new Product { ProductID = 3, Name = "P3"},
            new Product { ProductID = 4, Name = "P4"},
            new Product { ProductID = 5, Name = "P5"}
        }).AsQueryable<Product>()) ;

        // Arrange
        HomeController homeController = new HomeController(mockRepo.Object) { PageSize = 3 };

        //Act
        ProductsListViewModel viewModel = 
            (homeController.Index(null, 2) as ViewResult)?.ViewData?.Model as ProductsListViewModel ?? new();

        //Assert 
        PagingInfo pageInfo = viewModel.PagingInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);

    
    }

    [Fact]
    public void Can_Filter_Products()
    { 
        // Arrange
        // Create the mock repository
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
            new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
            new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
            new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
            new Product { ProductID = 5, Name = "P5", Category = "Cat3" }
        }).AsQueryable<Product>());

        // Arrange - create a controller and make the page size 3 items
        HomeController controller = new HomeController(mock.Object);
        controller.PageSize = 3;

        //Act
        ProductsListViewModel model = (controller.Index("Cat2", 1) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new();
        Product[] result = model?.Products.ToArray();

        //Assert
        Assert.Equal(2, result.Length);
        Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
        Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");

    
    }
    
}