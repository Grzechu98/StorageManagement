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
    public class StorageRacksControllerTests : IntegrationTest
    {
        [Fact]
        public async Task PostRack_Test_ValidData()
        {

            // Arrange
            var request = new
            {
                Url = "api/StorageRacks",
                Body = new
                {
                    RackNumber = "D1",
                    IsTaken = false,
                    WarehouseId = 1
                }
            };
            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task PutRack_Test_ValidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/StorageRacks/1",
                Body = new
                {
                    Id = 1,
                    RackNumber = "A1",
                    IsTaken = false,
                    WarehouseId = 1
                }
            };
            // Act
            var response = await client.PutAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task DeleteStorageRack_Test_ValidData()
        {
            // Arrange
            var Postrequest = new
            {
                Url = "api/StorageRacks",
                Body = new
                {
                    RackNumber = "A1Deltest",
                    IsTaken = false,
                    WarehouseId = 1
                }
            };
            // Act
            var postresponse = await client.PostAsync(Postrequest.Url, JsonHelper.TransformToJson(Postrequest.Body));
            var PostResponseJson = await postresponse.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<StorageRackModel>(PostResponseJson);

            var delResponse = await client.DeleteAsync(string.Format("api/StorageRacks/" + responseObj.Id));
            // Assert
            postresponse.EnsureSuccessStatusCode();
            delResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetStorageRack_Test_ValidData()
        {
            // Arrange
            var request = "/api/StorageRacks/1";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PostStorageRack_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/StorageRacks",
                Body = new
                {
                    Id = 1,
                    RackNumber = "A1",
                    IsTaken = "badrequest",
                    WarehouseId = 1
                }
            };
            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task PutStorageRack_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/StorageRacks/1",
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
        public async Task DeleteStorageRack_Test_InvalidData()
        {
            // Arrange
            var Postrequest = new
            {
                Url = "api/StorageRacks",
                Body = new
                {
                    RackNumber = "A2121Deltest",
                    IsTaken = true,
                    WarehouseId = 1
                }
            };
            // Act
            var postresponse = await client.PostAsync(Postrequest.Url, JsonHelper.TransformToJson(Postrequest.Body));
            var PostResponseJson = await postresponse.Content.ReadAsStringAsync();
            var responseObj = JsonConvert.DeserializeObject<StorageRackModel>(PostResponseJson);

            var delResponse = await client.DeleteAsync(string.Format("api/StorageRacks/" + responseObj.Id + 1999992));
            // Assert
            postresponse.EnsureSuccessStatusCode();
            delResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetStorageRack_Test_InvalidData()
        {
            // Arrange
            var request = "/api/StorageRacks/1999992";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
