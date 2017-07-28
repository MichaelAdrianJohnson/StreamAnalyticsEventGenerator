using System;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using System.Threading;

namespace StreamAnalyticsEventGenerator
{
    class Program
    {
        private static EventHubClient eventHubClient;
        private const string connectionString = "";//Insert Eventhub connection string, non demo code should use config file
        private const string eventHubName = ""; //Insert event hub name

        static void Main(string[] args)
        {
            bool inLoop = true;
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

            double temp = 40.0;
            double max = 50.0;
            double min = 30.0;
            Random random = new Random();
            int randomNumber;
            while (inLoop)
            {
                randomNumber = random.Next(0, 100);
                if (randomNumber > 50 && temp < max)
                    temp = temp + 0.5;
                else if (randomNumber < 50 && temp > min)
                    temp = temp - 0.5;
                else if (randomNumber == 50)
                    temp = 60.0;

                var message = "{\"temp\":" + temp.ToString() + "}"; //Over simplistic means to construct a JSON object 

                eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));

                Console.WriteLine(temp);

                Thread.Sleep(200);

            }

        }
    }
}
