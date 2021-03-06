﻿using EmployeesApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace EmployeesApiIntegrationTests
{
    public class PlacingAnOrder : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public PlacingAnOrder(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task PlacingAnOrderBeforeCutoffShipsToday()
        {
            //GIVEN
            //it is before the cutoff
            _factory.SystemTimeToUse = new DateTime(1969, 4, 20, 11, 59, 59);
            //WHEN
            //I place an order
            var response = _client.PostAsJsonAsync("/orders", new OrderToSend());
            //THEN
            //it ships the same day
            var content = await response.Content.ReadAsAsync<OrderConfirmation>();
            Assert.Equal(21, content.estimatedShipDate.Date.Day);
        }

        [Fact]
        public async Task PlacingAnOrderAfterCutoffShipsTomorrow()
        {
            //GIVEN
            //it is before the cutoff
            _factory.SystemTimeToUse = new DateTime(1969, 4, 20, 12, 00, 00);
            //WHEN
            //I place an order
            var response = _client.PostAsJsonAsync("/orders", new OrderToSend());
            //THEN
            //it ships the same day
            var content = await response.Content.ReadAsAsync<OrderConfirmation>();
            Assert.Equal(21, content.estimatedShipDate.Date.Day);
        }

        public class OrderToSend
        {

        }

        public class OrderConfirmation
        {
            public DateTime estimatedShipDate { get; set; }
        }
    }
}
