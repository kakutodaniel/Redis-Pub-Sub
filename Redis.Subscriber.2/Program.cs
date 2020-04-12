﻿using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Redis.Subscriber._2
{
    class Program
    {
        static void Main(string[] args)
        {

            var configuration = ConfigurationOptions.Parse("localhost:6379");

            var connection = ConnectionMultiplexer.Connect(configuration);

            var subscriber = connection.GetSubscriber();

            const string channelName = "test-channel";

            var db = connection.GetDatabase();

            var all = db.ListRange("key1");
            Random rnd = new Random();

            foreach (var item in all)
            {
               
                var rndValue = rnd.Next(1000, 3000);
                Console.WriteLine($"random: {rndValue}");

                Thread.Sleep(rndValue);

                var t = db.ListRemove("key1", item);
                var v = System.Text.Encoding.Default.GetString((byte[])item.Box());

                if (t == 0)
                {
                    Console.WriteLine($"{v} already removed");
                }
                else
                {
                    Console.WriteLine($"removed item {v}");
                }
            }

            //while (true)
            //{
            //    var pop = db.ListLeftPop("key1");

            //    if (pop.HasValue)
            //    {

            //        Console.WriteLine(System.Text.Encoding.Default.GetString((byte[])pop.Box()));

            //        Console.WriteLine($"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")}<Subscriber.1 - Normal - {channel}><{message}>.");
            //    }
            //    else
            //    {
            //        break;
            //    }

            //    Thread.Sleep(4000);
            //}

            subscriber.Subscribe(channelName, (channel, message) =>
            {
                var pop = db.ListLeftPop("key1");

                if (pop.HasValue)
                {
                    Console.WriteLine(System.Text.Encoding.Default.GetString((byte[])pop.Box()));
                    //Console.WriteLine($"[Subscriber.2] - {message}");
                }

                //Thread.Sleep(5000);
                //Console.WriteLine($"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fffffff")}<Subscriber.2 - Normal - {channel}><{message}>.");
            });

            Thread.Sleep(Timeout.Infinite);

        }
    }
}
