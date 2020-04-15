using StackExchange.Redis;
using System;
using System.Threading;

namespace Redis.Subscriber._2
{
    public class car
    {
        public string Nome { get; set; }
    }

    class Program
    {
        static Timer timer;

        static void Main(string[] args)
        {
            var configuration = ConfigurationOptions.Parse("localhost:6379");

            var connection = ConnectionMultiplexer.Connect(configuration);

            var subscriber = connection.GetSubscriber();

            const string channelName = "test-channel";

            var db = connection.GetDatabase();

            var all = db.ListRange("key1");
            Random rnd = new Random();

            timer = new Timer(Run, new car { Nome = "bmw" }, 0, 2000);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            //timer.Dispose();

            //foreach (var item in all)
            //{

            //    var rndValue = rnd.Next(1000, 3000);
            //    Console.WriteLine($"random: {rndValue}");

            //    Thread.Sleep(rndValue);

            //    var t = db.ListRemove("key1", item);
            //    var v = System.Text.Encoding.Default.GetString((byte[])item.Box());

            //    if (t == 0)
            //    {
            //        Console.WriteLine($"{v} already removed");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"removed item {v}");
            //    }
            //}

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

            //subscriber.Subscribe(channelName, (channel, message) =>
            //{
            //    try
            //    {
            //        var pop = db.ListLeftPop("key1");

            //        if (pop.HasValue)
            //        {
            //            var content = System.Text.Encoding.Default.GetString((byte[])pop.Box());
            //            db.ListRightPush("key2", content);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //        Console.WriteLine(ex.Message);
            //    }

            //});

            Console.ReadLine();

           

            //Thread.Sleep(Timeout.Infinite);

        }

        public static void OnProcessExit(object sender, EventArgs args)
        {
            timer.Dispose();
        }

        public static void Run(object state)
        {
            var ob = (car)state;
            Console.WriteLine(ob.Nome);
        }
    }
}
