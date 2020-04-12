using StackExchange.Redis;
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

            while (true)
            {
                var pop = db.ListLeftPop("key1");

                if (pop.HasValue)
                {

                    Console.WriteLine(System.Text.Encoding.Default.GetString((byte[])pop.Box()));

                    //Console.WriteLine($"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")}<Subscriber.1 - Normal - {channel}><{message}>.");
                }
                else
                {
                    break;
                }

                Thread.Sleep(4000);
            }

            //Task.Run(() =>
            //{
            //    //var lst = db.ListGetByIndex("key1", 0);

            //    var pop = db.ListLeftPop("key1");

            //    if (pop.HasValue)
            //    {

            //        Console.WriteLine(System.Text.Encoding.Default.GetString((byte[])pop.Box()));

            //        //Console.WriteLine($"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")}<Subscriber.1 - Normal - {channel}><{message}>.");
            //    }

            //});


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
