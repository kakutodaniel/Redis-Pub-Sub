using StackExchange.Redis;
using System;

namespace Redis.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = ConfigurationOptions.Parse("localhost:6379");

            var connection = ConnectionMultiplexer.Connect(configuration);

            var publisher = connection.GetSubscriber();

            const string channelName = "test-channel";

            var db = connection.GetDatabase();

            for (int i = 0; i < 50; i++)
            {
               db.ListRightPush(new RedisKey("key1"), new RedisValue($"value-{i}"));

                //publisher.Publish(channelName, $"Message: {channelName} - value-{i}", CommandFlags.FireAndForget);
                publisher.Publish(channelName, "", CommandFlags.FireAndForget);
            }

            //publisher.Publish("test-l", $"Publish a message to pattern channel: test-l");

        }
    }
}
