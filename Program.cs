using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LogParser
{
    class Program
    {

        public class LogModel
        {
            public Guid Id;
            public string Message;
            public DateTime Date;
        }

        static void Main(string[] args)
        {
            var filePath = args[0];
            var fileContents = File.ReadAllText(filePath);
            var allLogs = JsonConvert.DeserializeObject<List<LogModel>>(fileContents);

            Console.WriteLine("File Loaded");

            while (true)
            {
                Console.WriteLine("Select operation");
                Console.WriteLine("1. DateRange");
                Console.WriteLine("2. Contains Filter");
                Console.WriteLine("3. Output");
                var selected = Console.ReadKey().Key;


                if (selected == ConsoleKey.D1 || selected == ConsoleKey.D2 || selected == ConsoleKey.D3)
                {
                    switch (selected)
                    {
                        case ConsoleKey.D1:
                            Console.Clear();
                            Console.WriteLine("From: ");
                            var from = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("To: ");
                            var to = DateTime.Parse(Console.ReadLine());
                            allLogs = allLogs.Where(w => w.Date >= from && w.Date <= to).ToList();
                            break;
                        case ConsoleKey.D2:
                            Console.Clear();
                            Console.WriteLine("Text: ");
                            var text = Console.ReadLine();
                            allLogs = allLogs.Where(x => x.Message.Contains(text)).ToList();
                            break;
                        case ConsoleKey.D3:
                            foreach (var log in allLogs)
                            {
                                var resultText = log.Date + ":" + log.Message;
                                Console.WriteLine(resultText);
                            }

                            Console.WriteLine("Press any key to exit");
                            Console.ReadKey();
                            return;
                    }

                    Console.Clear();
                    Console.WriteLine("Done");

                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }


        }
    }
}
