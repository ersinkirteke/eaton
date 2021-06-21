using Confluent.Kafka;
using System;
using System.Threading;

namespace kafka.eaton.common.infrastructure.wrappers
{
    /// <summary>
    /// Wrapper for kafka consumer 
    /// </summary>
    public class ConsumerWrapper
    {
        private string _topicName;
        private ConsumerConfig _consumerConfig;
        private IConsumer<string, string> _consumer;
        private static readonly Random rand = new Random();

        public ConsumerWrapper(ConsumerConfig config, string topicName)
        {
            this._topicName = topicName;
            this._consumerConfig = config;
            this._consumer = new ConsumerBuilder<string, string>(this._consumerConfig).Build();
            this._consumer.Subscribe(topicName);
        }

        /// <summary>
        /// Read messages from kafka topic
        /// </summary>
        /// <returns></returns>
        public string ReadMessage()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;
            var cancelled = false;

            _consumer.Subscribe(this._topicName);

            while (!cancelled)
            {
                var consumeResult = _consumer.Consume(token);
                Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' from topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}");
                _consumer.Close();
                return consumeResult.Message.Value;
            }

            return "";
        }
    }
}
