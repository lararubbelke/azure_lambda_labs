namespace ProductCatalogSampleData
{
    using System;
    using PartsUnlimited.Models;

    public class Program
    {
       static string documentDbName = "PartsUnlimited";
        static string documentDbEndpoint = "https://lararcode.documents.azure.com:443/";
        static string documentDbAuthKey = "qFwyHrOwZbvAwQR4TbyLm4Z0MI4XRxVMxFuxmRNvuWeCZZIE4A4y42NGLBNaoqZimhp4u2eOLGAbKTmfjEM34Q==";
        static string documentDbCollection = "Products";

        static void Main(string[] args)
        {
            //string documentDbEndpoint = args[0];
            //string documentDbAuthKey = args[1];

            Console.WriteLine("Initializing DocumentDB");

            DocumentDBTestData.InitializePartsUnlimitedDatabaseAsync(documentDbName, documentDbEndpoint, documentDbAuthKey, documentDbCollection).Wait();

            Console.WriteLine("DocumentDB data is ready");
            Console.WriteLine("Press Any key to exit...");
            Console.ReadLine();
        }        
    }
}
