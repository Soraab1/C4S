using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Declare and initialize 10 satellites with RNGs
        string[] satellites = new string[10];
        for (int i = 0; i < satellites.Length; i++)
        {
            satellites[i] = "TEST24" + GenerateNumerologyStringWithSum(30);
        }

        // Declare VirtualSatellite and assign its RNG
        string VirtualSatellite = "TEST24" + GenerateNumerologyStringWithSum(30);
        string countryLocation = RandomizeCountryLocation();

        // Display the satellites
        for (int i = 0; i < satellites.Length; i++)
        {
            Console.WriteLine($"Sat{i + 1}: {satellites[i]}");
        }
        Console.WriteLine($"VirtualSatellite: {VirtualSatellite}, Location: {countryLocation}");

        // Validate the RNG for VirtualSatellite
        Console.WriteLine("\nEnter the RNG of the Virtual Satellite to proceed:");
        string userInput = Console.ReadLine();

        if (userInput == VirtualSatellite)
        {
            Console.WriteLine("Access Granted!\n");

            // Show menu options
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Send a message to satellites.");
            Console.WriteLine("2. Receive a message from a satellite.");
            Console.Write("Enter your choice (1 or 2): ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Proceed with sending a message
                string GroundTerminal1 = "", GroundTerminal2 = "", GroundTerminal3 = "", GroundTerminal4 = "", GroundTerminal5 = "";
                Console.WriteLine("\nEnter a message to send through the Ground Terminals:");
                string userMessage = Console.ReadLine();

                List<Tuple<int, char>> scrambledMessage = ScrambleMessage(userMessage, ref GroundTerminal1, ref GroundTerminal2, ref GroundTerminal3, ref GroundTerminal4, ref GroundTerminal5);

                Console.WriteLine("\nScrambled message across Ground Terminals:");
                Console.WriteLine($"GroundTerminal1: {GroundTerminal1}");
                Console.WriteLine($"GroundTerminal2: {GroundTerminal2}");
                Console.WriteLine($"GroundTerminal3: {GroundTerminal3}");
                Console.WriteLine($"GroundTerminal4: {GroundTerminal4}");
                Console.WriteLine($"GroundTerminal5: {GroundTerminal5}");

                string unscrambledMessage = UnscrambleMessage(scrambledMessage);
                Console.WriteLine("\nUnscrambled message at VirtualSatellite:");
                Console.WriteLine(unscrambledMessage);
            }
            else if (choice == "2")
            {
                // Receive a message from a satellite
                Console.WriteLine("\nChoose a satellite to receive a message from (1-10):");
                int satelliteIndex;
                while (!int.TryParse(Console.ReadLine(), out satelliteIndex) || satelliteIndex < 1 || satelliteIndex > 10)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 10:");
                }

                Console.WriteLine($"Receiving message from Sat{satelliteIndex}:");
                Console.WriteLine("\"Test 1 2 3, this is a message from Cybersecurity for Space\"");
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting program.");
            }
        }
        else
        {
            Console.WriteLine("Access Denied! Incorrect RNG.");
        }
    }

    static string GenerateNumerologyStringWithSum(int targetSum)
    {
        Dictionary<char, int> letterNumerology = new Dictionary<char, int>
        {
            { 'A', 1 }, { 'I', 1 }, { 'J', 1 }, { 'Q', 1 }, { 'Y', 1 },
            { 'B', 2 }, { 'K', 2 }, { 'R', 2 },
            { 'C', 3 }, { 'G', 3 }, { 'L', 3 }, { 'S', 3 },
            { 'D', 4 }, { 'M', 4 }, { 'T', 4 },
            { 'E', 5 }, { 'H', 5 }, { 'N', 5 }, { 'X', 5 },
            { 'U', 6 }, { 'V', 6 }, { 'W', 6 },
            { 'O', 7 }, { 'Z', 7 },
            { 'F', 8 }, { 'P', 8 }
        };

        Dictionary<int, int> numberNumerology = new Dictionary<int, int>();
        for (int i = 1; i <= 80; i++)
        {
            int numerologyValue = (i - 1) / 10 + 1;
            numberNumerology[i] = numerologyValue;
        }

        StringBuilder result = new StringBuilder();
        Random random = new Random();
        int currentSum = 0;

        while (currentSum < targetSum)
        {
            bool chooseNumber = random.Next(2) == 0;

            if (chooseNumber)
            {
                int randomNumber = random.Next(1, 81);
                int numberValue = numberNumerology[randomNumber];

                if (currentSum + numberValue <= targetSum)
                {
                    result.Append(randomNumber);
                    currentSum += numberValue;
                }
            }
            else
            {
                char randomLetter = letterNumerology.Keys.ToArray()[random.Next(letterNumerology.Count)];
                int letterValue = letterNumerology[randomLetter];

                if (currentSum + letterValue <= targetSum)
                {
                    result.Append(randomLetter);
                    currentSum += letterValue;
                }
            }
        }

        return result.ToString();
    }

    static string RandomizeCountryLocation()
    {
        string[] countries = new string[]
        {
            "United States", "India", "China", "Russia", "Brazil",
            "Australia", "Canada", "Germany", "France", "United Kingdom",
            "Japan", "South Africa", "Italy", "Mexico", "Spain",
            "Turkey", "Indonesia", "South Korea", "Argentina", "Saudi Arabia"
        };

        Random random = new Random();
        return countries[random.Next(countries.Length)];
    }

    static List<Tuple<int, char>> ScrambleMessage(string message, ref string GroundTerminal1, ref string GroundTerminal2, ref string GroundTerminal3, ref string GroundTerminal4, ref string GroundTerminal5)
    {
        Random random = new Random();
        StringBuilder[] terminals = { new StringBuilder(), new StringBuilder(), new StringBuilder(), new StringBuilder(), new StringBuilder() };

        List<Tuple<int, char>> scrambledMessage = new List<Tuple<int, char>>();

        for (int i = 0; i < message.Length; i++)
        {
            int terminalIndex = random.Next(5);
            terminals[terminalIndex].Append(message[i]);
            scrambledMessage.Add(new Tuple<int, char>(terminalIndex, message[i]));
        }

        GroundTerminal1 = terminals[0].ToString();
        GroundTerminal2 = terminals[1].ToString();
        GroundTerminal3 = terminals[2].ToString();
        GroundTerminal4 = terminals[3].ToString();
        GroundTerminal5 = terminals[4].ToString();

        return scrambledMessage;
    }

    static string UnscrambleMessage(List<Tuple<int, char>> scrambledMessage)
    {
        StringBuilder unscrambledMessage = new StringBuilder(scrambledMessage.Count);
        foreach (var pair in scrambledMessage)
        {
            unscrambledMessage.Append(pair.Item2);
        }

        return unscrambledMessage.ToString();
    }
}
