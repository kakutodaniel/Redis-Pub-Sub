﻿using StackExchange.Redis;

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

            var key = new RedisKey("key1");

            for (int i = 0; i < 50; i++)
            {
                var vl = new RedisValue($"pub-value-{i}");
                db.ListRightPush(key, vl);
                publisher.Publish(channelName, vl, CommandFlags.FireAndForget);
            }
        }
    }
}
