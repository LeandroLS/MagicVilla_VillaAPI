using System;

namespace MagicVilla_VillaAPI.Data.Logging;

public class Logging : ILogging
{
    public void log(string message, string type)
    {
        if (type == "error")
        {
            Console.WriteLine("ERROR - " + message);
        }
        Console.WriteLine(message);
    }
}
