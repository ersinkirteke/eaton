using AutoMapper;
using Confluent.Kafka;
using kafka.eaton.common.infrastructure.dataaccess;
using kafka.eaton.consumer.api.profiles;
using kafka.eaton.consumer.api.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace kafka.eaton.consumer.test
{
    public class ProcessTelemetryServiceTest
    {

        private IConfiguration _configuration;
        private ConsumerConfig _consumerConfig;
        private ProducerConfig _producerConfig;
        private static IMapper _mapper;
        private IOptions<DataAccessOptions> _options;

        public ProcessTelemetryServiceTest()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

            _configuration = configuration;
            _consumerConfig = configuration.GetSection("consumer").Get<ConsumerConfig>();
            _producerConfig = configuration.GetSection("producer").Get<ProducerConfig>();
            _options = Options.Create(configuration.Get<DataAccessOptions>());

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new TelemetryProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task Execute_Telemetry_Process_Async()
        {
            //ARRANGE
            TelemetryDataAccess telemetryDataAccess = new TelemetryDataAccess(_options);
            var service = new ProcessTelemetryService(_consumerConfig, _producerConfig, telemetryDataAccess, _mapper, _configuration);

            //ACT
            var cancellationToken = new CancellationTokenSource(5000);
            var serviceTask = Task.Run(async () => await service.StartAsync(cancellationToken.Token));
            await Task.WhenAny(serviceTask, Task.Delay(Timeout.Infinite, cancellationToken.Token));

            //ASSERT
            Assert.NotEqual(TaskStatus.Faulted, serviceTask.Status);
            Assert.True(cancellationToken.IsCancellationRequested);
            await service.StopAsync(cancellationToken.Token);
        }
    }
}
