using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using Confluent.Kafka;
using AutoMapper;
using kafka.eaton.common.infrastructure.dataaccess;
using kafka.eaton.common.infrastructure.wrappers;
using kafka.eaton.common.domain.entities;
using kafka.eaton.common.domain.models;
using Microsoft.Extensions.Configuration;

namespace kafka.eaton.consumer.api.services
{
    /// <summary>
    /// Background worker for telemetry processing
    /// </summary>
    public class ProcessTelemetryService : BackgroundService
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly ProducerConfig _producerConfig;
        private readonly TelemetryDataAccess _telemetryDataAccess;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ProcessTelemetryService(
            ConsumerConfig consumerConfig, 
            ProducerConfig producerConfig, 
            TelemetryDataAccess telemetryDataAccess, 
            IMapper mapper,
            IConfiguration configuration
            )
        {
            _telemetryDataAccess = telemetryDataAccess;
            _consumerConfig = consumerConfig;
            _producerConfig = producerConfig;
            _mapper = mapper;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("TelemetryProcessing Service Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerHelper = new ConsumerWrapper(_consumerConfig, _configuration.GetValue<string>("topic1"));

                try
                {
                    string telemetryRequest = consumerHelper.ReadMessage();
                    //Deserilaize 
                    TelemetryDto telemetryDto = JsonConvert.DeserializeObject<TelemetryDto>(telemetryRequest);

                    Telemetry telemetry = _mapper.Map<Telemetry>(telemetryDto);
                    //Process Telemetry
                    //Create a Id with both devicename and epoch time
                    DateTime time = DateTime.Now;
                    long unixTime = ((DateTimeOffset)time).ToUnixTimeSeconds();
                    telemetry.Id=telemetry.DeviceName+" "+ unixTime;
                    await _telemetryDataAccess.Insert(telemetry);
                    Console.WriteLine($"Info: TelemetryHandler => Processing the telemetry for {telemetry.DeviceName}");

                    //Write to TelemetryProcessed Queue
                    var producerWrapper = new ProducerWrapper(_producerConfig, _configuration.GetValue<string>("topic2"));
                    await producerWrapper.WriteMessage(JsonConvert.SerializeObject(telemetry));
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Info: " + ex.Message);
                    throw new Exception("somethgins wrong happen");
                }
            }
        }
    }
}
