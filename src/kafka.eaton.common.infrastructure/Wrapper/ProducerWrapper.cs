using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace kafka.eaton.common.infrastructure.wrappers
{
    /// <summary>
    /// Wrapper for kafka producer
    /// </summary>
    public class ProducerWrapper
    {
        private string _topicName;
        private IProducer<string, string> _producer;
        private ProducerConfig _config;
        private static readonly Random rand = new Random();

        public ProducerWrapper(ProducerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._config = config;
            this._producer = new ProducerBuilder<string, string>(this._config).Build();
        }

        /// <summary>
        /// Write message to kafka topic
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WriteMessage(string message)
        {
            var result = await this._producer.ProduceAsync(this._topicName, new Message<string, string> 
            { 
                Key = rand.Next(5).ToString(), 
                Value = message 
            });
            Console.WriteLine($"KAFKA => Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
            return;
        }
    }
}
