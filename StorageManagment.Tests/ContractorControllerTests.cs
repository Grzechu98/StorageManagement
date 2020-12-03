using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StorageManagment.Tests
{
    public class ContractorControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetContractors_Test_ValidData()
        {
            // Arrange
            var request = "api/Contractor";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetContractorTotalStock_Test_ValidData()
        {
            // Arrange
            var request = "api/Contractor/0123456789/TotalStock";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetContractorWarehouseStock_Test_ValidData()
        {
            // Arrange
            var request = "api/Contractor/0123456789/WarehouseStock/1";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetContractorFreeStock_Test_ValidData()
        {
            // Arrange
            var request = "api/Contractor/0123456789/FreeStorageSpaceInWarehouse/1";

            // Act
            var response = await client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
