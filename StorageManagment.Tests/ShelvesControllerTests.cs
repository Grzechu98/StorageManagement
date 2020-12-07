using FluentAssertions;
using Newtonsoft.Json;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StorageManagment.Tests
{
    public class ShelvesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task PostShelf_Test_ValidData()
        {

            // Arrange
            var request = new
            {
                Url = "api/Shelves",
                Body = new
                {
                    ShelfNumber = "4post",
                    Quantity = 1,
                    RackId = 2
                }
            };
            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task PutShelf_Test_ValidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Shelves/14",
                Body = new
                {
                    Id = 14,
                    ShelfNumber = "4",
                    Quantity = 1,
                    RackId = 1
                }
            };
            // Act
            var response = await client.PutAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task DeleteShelf_Test_ValidData()
        {
            // Arrange
            var Postrequest = new
            {
                Url = "api/Shelves",
                Body = new
                {
                    ShelfNumber = "3del",
                    Quantity = 1,
                    RackId = 1
                }
            };
            // Act
            var postresponse = await client.PostAsync(Postrequest.Url, JsonHelper.TransformToJson(Postrequest.Body));
            var PostResponseJson = await postresponse.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<ShelfModel>(PostResponseJson);

            var delResponse = await client.DeleteAsync(string.Format("api/Shelves/" + responseObj.Id));
            // Assert
            postresponse.EnsureSuccessStatusCode();
            delResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetShelf_Test_ValidData()
        {
            // Arrange
            var request = "/api/Shelves/14";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PostShelf_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Shelves",
                Body = new
                {
                    ShelfNumber = "4",
                    Quantity = 1,
                    RackId = "abc"
                }
            };
            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task PutShelf_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Shelves/1",
                Body = new
                {
                    Id = 5,
                    ShelfNumber = "4",
                    Quantity = 1,
                    RackId = 1
                }
            };
            // Act
            var response = await client.PutAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task DeleteShelf_Test_InvalidData()
        {
            // Arrange
   
            // Act
            var delResponse = await client.DeleteAsync(string.Format("api/Shelves/3333"));
            // Assert
            
            delResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetShelf_Test_InvalidData()
        {
            // Arrange
            var request = "/api/Shelves/1999992";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
