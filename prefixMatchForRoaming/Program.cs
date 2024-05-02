using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List of prefixes
        List<string> prefixes = new List<string>
        {
            "88071", "88081", "88091", "88051", "8801",
            "017", "018", "019", "016", "015", "013",
            "880711", "880811", "880911", "880611", "880511",
            "0096", "96", "096", "88096"
        };

        // Sample originatingCallingNumber
        string originatingCallingNumber = "11171234567";

        Match(originatingCallingNumber, prefixes);

    }

    public static void Match(string number, List<string> prefixes)
    {
        foreach (string prefix in prefixes)
        {
            if (number.StartsWith(prefix))
            {
                Console.WriteLine("Matched");
                Console.ReadLine(); // If the number doesn't start with any prefix, return true
            }
            else
            {
                Console.WriteLine("Not Matched");
                Console.ReadLine();
            }
        }
         // If any prefix matches, return false
    }
}