using Confluent.Kafka;
using kafka.eaton.common.domain.models;
using kafka.eaton.producer.api.Controllers;
using kafka.eaton.producer.api.settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace kafka.eaton.producer.test
{
    public class ProductControllerTest
    {
        private ProducerConfig _producerConfig;

        public ProductControllerTest()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            _producerConfig = configuration.GetSection("producer").Get<ProducerConfig>();
        }

        [Fact]
        public async Task Send_Telemetry_Succeded()
        {
            // Arrange
            var producerSettingsMock = Mock.Of<IProducerSettings>(m =>
                       m.AnySetting == "test" &&
                       m.Topic == "predictpulse");
            // If provider.GetService(typeof(IValidator<User>)) gets called, 
            // IValidator<User> mock will be returned
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(provider => provider.GetService(typeof(IProducerSettings)))
                .Returns(producerSettingsMock);

            // Mock the HttpContext to return a mockable 
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(context => context.RequestServices)
                .Returns(serviceProviderMock.Object);

            var controller = new ProducerController(_producerConfig);
            var actionExecutingContext = HttpContextUtils.MockedActionExecutingContext(httpContextMock.Object, controller);
            controller.ControllerContext = new ControllerContext(actionExecutingContext);

            TelemetryDto telemetryDto = new TelemetryDto()
            {
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

    public class HttpContextUtils
    {
        public static ActionExecutingContext MockedActionExecutingContext(
            HttpContext context,
            IList<IFilterMetadata> filters,
            IDictionary<string, object> actionArguments,
            object controller
        )
        {
            var actionContext = new ActionContext() { HttpContext = context, RouteData = new RouteData(), ActionDescriptor = new ControllerActionDescriptor() };

            return new ActionExecutingContext(actionContext, filters, actionArguments, controller);
        }
        public static ActionExecutingContext MockedActionExecutingContext(
            HttpContext context,
            object controller
        )
        {
            return MockedActionExecutingContext(context, new List<IFilterMetadata>(), new Dictionary<string, object>(), controller);
        }
    }
}
