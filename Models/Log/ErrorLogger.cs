using System;
using System.IO;

public class ErrorLogger
{
    private readonly string logFilePath;

    public ErrorLogger(string filePath)
    {
        logFilePath = filePath;
    }

    public void LogError(string errorMessage)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"Error at {DateTime.Now}: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing to the log file: " + ex.Message);
        }
    }
}

