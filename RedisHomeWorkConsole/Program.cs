using Azure.Storage.Queues;
using RedisHomeWorkConsole.Types;
using System.Text.Json;
using System.Threading;

string jsonFilePath = @"../../../appsettings.json";
string jsonString = File.ReadAllText(jsonFilePath);

AzureQueueType azureQueue = JsonSerializer.Deserialize<AzureQueueType>(jsonString);

QueueClient queueClient = new QueueClient(azureQueue.ConnectionString, azureQueue.QueueName);
await queueClient.CreateIfNotExistsAsync();

while (true)
{
    Console.Write("Filim Adi Daxil Edin:");
    string filimName = Console.ReadLine();
    Console.Clear();

    if (!string.IsNullOrWhiteSpace(filimName))
    {
        try
        {
            Console.WriteLine($"Filim: {filimName} <- Elave edildi");

            await queueClient.SendMessageAsync(filimName);
            Console.WriteLine("Message Getdi ok.");

            Thread.Sleep(1400);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata oluştu: {ex.Message}");
            Thread.Sleep(1400);
        }
    }
    else
    {
        Console.WriteLine("Filim Adi Bos Ola Bilmez!!!!");
        Thread.Sleep(1400);
    }

    Console.Clear();
}
