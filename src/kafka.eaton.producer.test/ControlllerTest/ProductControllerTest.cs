using Confluent.Kafka;
using kafka.eaton.common.domain.models;
using kafka.eaton.producer.api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace kafka.eaton.producer.test
{
    public class ProductControllerTest
    {
        private IConfiguration _configuration;
        private ProducerConfig _producerConfig;

        public ProductControllerTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            _configuration = configuration;
            _producerConfig = configuration.GetSection("producer").Get<ProducerConfig>();
        }

        [Fact]
        public async Task Send_Telemetry_Succeded()
        {
            // Arrange
            var controller = new ProducerController(_producerConfig,_configuration);
            TelemetryDto telemetryDto = new TelemetryDto() { 
                DeviceName = "eatonups125", 
                Temperature = 34, 
                TimeStamp = new DateTimeOffset(), 
                Longitude = 24.1313, 
                Latitude = 35.4343 
            };

            // Act
            var result = await controller.SendTelemetry(telemetryDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal("Device telemetry is in progress", createdResult.Value);
        }
    }
}
