using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StorageManagment.Tests
{
    public class ProductControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetProductById_Test_ValidData()
        {
            // Arrange
            var request = "api/Products/1";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task GetProductsByName_Test_ValidData()
        {
            // Arrange
            var request = "api/Products/ByName/testowy";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductById_Test_InvalidData()
        {
            // Arrange
            var request = "api/Products/31313131";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
      
    }
}
