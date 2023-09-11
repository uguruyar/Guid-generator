string formattedDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
string sanitizedDateTime = new string(formattedDateTime.Where(char.IsDigit).ToArray());
string csvFilePath = "guid_records-" + sanitizedDateTime + ".csv";

Task[] tasks = new Task[Environment.ProcessorCount];

for (int i = 0; i < tasks.Length; i++)
{
    tasks[i] = Task.Factory.StartNew(() =>
    {
        using (StreamWriter writer = new StreamWriter(csvFilePath, append: true))
        {
            for (int j = 0; j < 25000; j++)
            {
                Guid guid = Guid.NewGuid();
                writer.WriteLine(guid.ToString());
            }
        }
    });
}

Task.WaitAll(tasks);

Console.WriteLine("Created 100.000 guid records successfully.");