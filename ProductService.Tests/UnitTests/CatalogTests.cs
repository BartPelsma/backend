using Microsoft.EntityFrameworkCore;
using ProductService.Controllers;
using ProductService.DBContexts;
using ProductService.Models.DTO;
using ProductService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Flurl.Http.Testing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;



namespace ProductService.Tests.UnitTests
{
    public class CatalogTests
    {
        private ProductController _controller;
        private ProductServiceDatabaseContext _context;
        public CatalogTests()
        {
            Initialize();
        }

        public bool Initialize()
        {
            var options = new DbContextOptionsBuilder<ProductServiceDatabaseContext>().UseInMemoryDatabase(databaseName: "InMemoryProductDb").Options;

            _context = new ProductServiceDatabaseContext(options);
            _controller = new ProductController(_context, Options.Create(new AppConfig() { ApiGatewayBaseUrl = "http://fake-url.com" }));
            SeedProductInMemoryDatabaseWithData(_context);

            return false;
        }

        [Fact]
        private async Task GetCatalogEntries_ShouldReturnCatalogpageEmpty()
        {
            Initialize();
            var imgblobs = new List<ImageBlobModel>();

            string serializedObject = JsonConvert.SerializeObject(imgblobs);
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(serializedObject);
                var result = await _controller.GetCatalogEntries(0, 0,"-","-");

                var okObj = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<CatalogPage>(okObj.Value);
                Assert.Empty(model.CatalogItems);
            }
        }

        [Fact]
        private async Task GetCatalogEntries_FilterOutAllButChat()
        {
            Initialize();
            var imgblobs = new List<ImageBlobModel>();

            string serializedObject = JsonConvert.SerializeObject(imgblobs);
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(serializedObject);
                var result = await _controller.GetCatalogEntries(0, 5, "-","Chat");
                var okObj = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<CatalogPage>(okObj.Value);

                CatalogItem testItem = new CatalogItem();

                foreach(var item in model.CatalogItems)
                {
                    var category = new Category { Name = item.CategoryName };
                    testItem.CatalogNumber = 1;
                    testItem.Category = category;
                }

                Assert.Equal("Chat", testItem.Category.Name);
                
            }
        }

        [Fact]
        private async Task GetCatalogEntries_FilterOutAllButBBB()
        {
            Initialize();
            var imgblobs = new List<ImageBlobModel>();

            string serializedObject = JsonConvert.SerializeObject(imgblobs);
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(serializedObject);
                var result = await _controller.GetCatalogEntries(0, 5, "BBB", "-");
                var okObj = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<CatalogPage>(okObj.Value);

                string name = "ERROR";

                foreach (var item in model.CatalogItems)
                {
                    foreach(var product in item.CatalogItems)
                    {
                        name = product.Name;
                    }
                }

                Assert.Equal("BBB", name);

            }
        }

        [Fact]
        private async Task GetCatalogEntries_ShouldReturnCatalogpage()
        {
            Initialize();
            var imgblobs = new List<ImageBlobModel>();

            string serializedObject = JsonConvert.SerializeObject(imgblobs);
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(serializedObject);
                var result = await _controller.GetCatalogEntries(0, 5, "-", "-");

                var okObj = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<CatalogPage>(okObj.Value);

                Assert.Equal(5, model.CatalogItems.Count);
            }
        }

        [Fact]
        private async Task GetCatalogEntries_ShouldReturnCatalogpageNumber3()
        {
            Initialize();
            var imgblobs = new List<ImageBlobModel>();

            string serializedObject = JsonConvert.SerializeObject(imgblobs);
            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(serializedObject);
                var result = await _controller.GetCatalogEntries(3, 2, "-", "-");

                var okObj = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<CatalogPage>(okObj.Value);

                Assert.Single(model.CatalogItems);
            }
        }

        [Fact]
        private async Task GetCatalogEntries_ShouldReturnCatalogProductsFiltertBySearch()
        {
            Initialize();
            var imgblobs = new List<ImageBlobModel>();

            string serializedObject = JsonConvert.SerializeObject(imgblobs);

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(serializedObject);
                var result = await _controller.GetCatalogEntries(0, 50, "C","-");

                var okObj = Assert.IsType<OkObjectResult>(result);
                var model = Assert.IsAssignableFrom<CatalogPage>(okObj.Value);

                Assert.Equal(1, model.CatalogItems.Count());
                Assert.Equal(1, model.CatalogItems[0].CatalogItems.Count);
                Assert.Equal("CCC", model.CatalogItems[0].CatalogItems[0].Name);
                Assert.Single(model.CatalogItems);
            }
        }

        private void SeedProductInMemoryDatabaseWithData(ProductServiceDatabaseContext context)
        {
            var category = new Category { Name = "Apha" };
            var category2 = new Category { Name = "Beta" };
            var category3 = new Category { Name = "Chat" };
            var category4 = new Category { Name = "Toine" };
            var category5 = new Category { Name = "Berny" };
            _context.Categories.Add(category);

            var data = new List<Product>
                {
                    new Product { Id= 1, Name = "BBB", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category },
                    new Product { Id= 2, Name = "ZZZ", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category2 },
                    new Product { Id= 3, Name = "AAA", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category3 },
                    new Product { Id= 4, Name = "CCC", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category4 },
                    new Product { Id= 5, Name = "DDD", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category5 },
                    new Product { Id= 6, Name = "FFF", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category2 },
                    new Product { Id= 7, Name = "GGG", Description = "", ProductState = ProductState.AVAILABLE, RequiresApproval = true, Category = category },
                };
            if (!context.Products.Any())
            {
                context.Products.AddRange(data);
            }
            context.SaveChanges();
        }
    }
}
