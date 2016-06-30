namespace PartsUnlimitedDataGen
{
    using Microsoft.Azure.Devices.Client;
    using System;
    using System.Threading;
    using System.Text;
    using Microsoft.ServiceBus.Messaging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using PartsUnlimited.Models;
    using System.Linq;
    using System.Collections.Generic;


    public class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "lararuk.azure-devices.net";
        static string deviceKey = "NhumXOEuOaawD+8Bpy0jcHwfup9kXzJCX+z2ASq8208=";

//        static string connectionString = "HostName=lararuk.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=8SlHU6gjGCO/vJBaWoSfhstBuNTkq5XgEV0N842Nkm0=";
//        static string iotHubD2cEndpoint = "messages/events";
 //       static EventHubClient eventHubClient;

        //static string eventHubName = "iothub-ehub-lararuk-48714-fa69a028e0";
        //static string eventHubConnectionString = "Endpoint=sb://ihsuprodbyres034dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=8SlHU6gjGCO/vJBaWoSfhstBuNTkq5XgEV0N842Nkm0=";
        //static EventHubClient eventHubClient = null;


        static void Main(string[] args)
        {

            
            try
            {
                Console.WriteLine("Simulated device\n");
                deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey));

//                eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString, eventHubName);

                // Randomly create instances of the store actions, such as add view remove and checkout a product, 
                // convert it into a JSON string and sends to the Event Hub.
                GenerateRandomEvents();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured while creating Event Hubs client: " + e.ToString());
            }
            finally
            {
                Console.WriteLine("DataGen has stopped");
            }
        }

        private static void GenerateRandomEvents()
        {
            var categories = SampleData.GetCategories().ToList();
            var products = SampleData.GetProducts(categories).ToList();
            var eventTypes = new List<string>() { "add", "view", "checkout", "remove" };
            var random = new Random();

            while (true)
            {
                var randomProduct = products[random.Next(products.Count)];
                var randomEventType = eventTypes[random.Next(eventTypes.Count)];

                var eventMessage = new EventMessage
                {
                    Type = randomEventType,
                    ProductId = randomProduct.ProductId,
                    Title = randomProduct.Title,
                    Category = randomProduct.Category.Name
                };

                SendingRandomMessages(eventMessage);
                Thread.Sleep(200);
            }
        }

        private static async void SendingRandomMessages(EventMessage eventMessage)
        {
            try
            {
                var messagestring = JsonConvert.SerializeObject(eventMessage, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() } );

                //EventData data = new EventData(Encoding.UTF8.GetBytes(message));
                var message = new Message(Encoding.ASCII.GetBytes(messagestring));

                await deviceClient.SendEventAsync(message);

                //eventHubClient.Send(data);
                Console.WriteLine("Sent message: {0}.", message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(DateTime.Now.ToString() + " > Exception: " + exception.ToString());
            }
        }

       }
    }
