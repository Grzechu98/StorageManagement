using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StorageManagement.API.Controllers;
using System.Net.Http;
using System.Net;
using FluentAssertions;
using StorageManagement.API.Models;
using Xunit;
using Newtonsoft.Json;

namespace StorageManagment.Tests
{

    public class WarehouseControllerTests : IntegrationTest
    {
        [Fact]
        public async Task PostWarehouse_Test_ValidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Warehouses",
                Body = new
                {
                    Name = "Magazyn Test",
                    City = "test",
                    Street = "test",
                    UnitNumber = "33",
                    PostCode = "35-221"
                }
            };
            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task PutWarehouse_Test_ValidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Warehouses/1",
                Body = new
                {
                    Id = 1,
                    Name = "Magazyn 1",
                    City = "Rzeszów",
                    Street = "Eugeniusza Kwiatkowskiego",
                    UnitNumber = "45",
                    PostCode = "35-311"
                }
            };
            // Act
            var response = await client.PutAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task DeleteWarehouse_Test_ValidData()
        {
            // Arrange
            var Postrequest = new
            {
                Url = "api/Warehouses",
                Body = new
                {
                    Name = "test",
                    City = "testCity",
                    Street = "test",
                    UnitNumber = "33",
                    PostCode = "35-221"
                }
            };
            // Act
            var postresponse = await client.PostAsync(Postrequest.Url, JsonHelper.TransformToJson(Postrequest.Body));
            var PostResponseJson = await postresponse.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<WarehouseModel>(PostResponseJson);

            var delResponse = await client.DeleteAsync(string.Format("api/Warehouses/"+ responseObj.Id));
            // Assert
            postresponse.EnsureSuccessStatusCode();
            delResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetWarehouses_Test()
        {
            // Arrange
            var request = "/api/Warehouses";
            // Act
            var response = await client.GetAsync(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
        }

        [Fact]
        public async Task GetWarehouse_Test_ValidData()
        {
            // Arrange
            var request = "/api/Warehouses/1";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetWarehouseStockStatus_Test_ValidData()
        {
            // Arrange
            var request = "api/Warehouses/1/stockstatus";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PostWarehouse_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Warehouses",
                Body = new
                {
                    Name = "test",
                }
            };
            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task PutWarehouse_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Warehouses/1",
                Body = new
                {
                    Id = 5,
                    Name = "testPut",
                    City = "testCity",
                    Street = "test",
                    UnitNumber = "33",
                    PostCode = "35-221"
                }
            };
            // Act
            var response = await client.PutAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task DeleteWarehouse_Test_InvalidData()
        {
            // Arrange
            var Postrequest = new
            {
                Url = "api/Warehouses",
                Body = new
                {
                    Name = "test",
                    City = "testCity",
                    Street = "test",
                    UnitNumber = "33",
                    PostCode = "35-221"
                }
            };
            // Act
            var postresponse = await client.PostAsync(Postrequest.Url, JsonHelper.TransformToJson(Postrequest.Body));
            var PostResponseJson = await postresponse.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<WarehouseModel>(PostResponseJson);

            var delResponse = await client.DeleteAsync(string.Format("api/Warehouses/" + responseObj.Id+1999992));
            // Assert
            postresponse.EnsureSuccessStatusCode();
            delResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWarehouse_Test_InvalidData()
        {
            // Arrange
            var request = "/api/Warehouses/1999992";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
