using Microsoft.AspNetCore.Mvc.Testing;
using StorageManagement.API;
using StorageManagement.API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace StorageManagment.Tests
{
    public class IntegrationTest
    {
        protected readonly HttpClient client;
        protected IntegrationTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            client = factory.CreateClient();
        }
    }
}
