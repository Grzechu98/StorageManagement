using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StorageManagment.Tests
{
    public class ManagmentControllerTests : IntegrationTest
    {
        [Fact]
        public async Task PlaceProduct_Test_ValidData()
        {
            // Arrange
            var request = new {
                Url = "api/Managment/PlaceProduct/1",
                Body = new
                {
                    warehouseId = 1
                }
            };

            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task AssignContractorRack_Test_ValidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Managment/ContractorRack",
                Body = new
                {
                    NIP = "0113456789",
                    warehouseId = 1
                }
            };

            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PlaceContractorProduct_Test_ValidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Managment/PlaceContractorProduct",
                Body = new
                {
                    Id = 1,
                    NIP = "0123456789",
                    warehouseId = 1
                }
            };

            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));


            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task AssignContractorRack_Test_InvalidData()
        {
            // Arrange
            var request = new
            {
                Url = "api/Managment/ContractorRack",
                Body = new
                {
                    NIP = "111111111",
                    warehouseId = 1
                }
            };

            // Act
            var response = await client.PostAsync(request.Url, JsonHelper.TransformToJson(request.Body));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


    }
}
