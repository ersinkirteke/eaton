using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using kafka.eaton.common.domain.models;
using kafka.eaton.common.infrastructure.wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace kafka.eaton.producer.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {
        private readonly ProducerConfig _producerConfig;
        private readonly IConfiguration _configuration;

        public ProducerController(ProducerConfig producerConfig, IConfiguration configuration)
        {
            _producerConfig = producerConfig;
            _configuration = configuration;
        }

        /// <summary>
        /// produce a telemetry data
        /// </summary>
        /// <param name="telemetry"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SendTelemetry([FromBody]TelemetryDto telemetry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Serialize 
            string serializedOrder = JsonConvert.SerializeObject(telemetry);

            Console.WriteLine("========");
            Console.WriteLine("Info: ProducerController => Post => Recieved a new telemetry data:");
            Console.WriteLine(serializedOrder);
            Console.WriteLine("=========");

            var producer = new ProducerWrapper(this._producerConfig, _configuration.GetValue<string>("topic"));
            await producer.WriteMessage(serializedOrder);

            return Created("Producer", "Device telemetry is in progress");
        }
    }
}
