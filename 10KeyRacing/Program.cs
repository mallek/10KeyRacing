using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace _10KeyRacing
{
    class Program
    {

        class Player
        {
            public int PlayerId { get; set; }
            public int Score { get; set; }
            public TimeSpan TotalTime { get; set; }
        }

        static void Main(string[] args)
        {
            var Players = new List<Player>();
            int PlayerCount = 0;
            int RaceLength = 0;

            Random rand = new Random();

            Console.WriteLine($" _  ___    _  __            ____                      _   _____         _   ");
            Console.WriteLine($"/ |/ _ \\  | |/ /___ _   _  / ___| _ __   ___  ___  __| | |_   _|__  ___| |_ ");
            Console.WriteLine($"| | | | | | ' // _ \\ | | | \\___ \\| '_ \\ / _ \\/ _ \\/ _` |   | |/ _ \\/ __| __|");
            Console.WriteLine($"| | |_| | | . \\  __/ |_| |  ___) | |_) |  __/  __/ (_| |   | |  __/\\__ \\ |_ ");
            Console.WriteLine($"|_|\\___/  |_|\\_\\___|\\__, | |____/| .__/ \\___|\\___|\\__,_|   |_|\\___||___/\\__|");
            Console.WriteLine($"                    |___/        |_|                                        ");
            Console.WriteLine();

            while (PlayerCount <= 0)
            {
                Console.Write("How Many Players? ");
                int.TryParse(Console.ReadLine(), out PlayerCount);
            }

            for (int i = 0; i < PlayerCount; i++)
            {
                Players.Add(new Player() { PlayerId = i + 1, Score = 0 });
            }


            while (RaceLength <= 0)
            {
                Console.Write("How Many Questions? ");
                int.TryParse(Console.ReadLine(), out RaceLength);
            }

            foreach (Player player in Players)
            {
                Console.Clear();
                Console.WriteLine($"Player {player.PlayerId} press any key when ready...");
                Console.ReadKey();
                Console.WriteLine($"Ready...");
                Thread.Sleep(500);
                Console.WriteLine($"Set....");
                Thread.Sleep(500);
                Console.WriteLine($"GO!");
                Thread.Sleep(500);

                for (int q = 0; q < RaceLength; q++)
                {

                    var sw = Stopwatch.StartNew();
                    var thisQuestion = rand.Next();
                    Console.WriteLine(thisQuestion);
                    int answer;
                    int.TryParse(Console.ReadLine(), out answer);
                    sw.Stop();
                    player.TotalTime += sw.Elapsed;
                    if (answer == thisQuestion)
                    {
                        player.Score += 1;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine($" _  ___    _  __            ____                      _   _____         _   ");
            Console.WriteLine($"/ |/ _ \\  | |/ /___ _   _  / ___| _ __   ___  ___  __| | |_   _|__  ___| |_ ");
            Console.WriteLine($"| | | | | | ' // _ \\ | | | \\___ \\| '_ \\ / _ \\/ _ \\/ _` |   | |/ _ \\/ __| __|");
            Console.WriteLine($"| | |_| | | . \\  __/ |_| |  ___) | |_) |  __/  __/ (_| |   | |  __/\\__ \\ |_ ");
            Console.WriteLine($"|_|\\___/  |_|\\_\\___|\\__, | |____/| .__/ \\___|\\___|\\__,_|   |_|\\___||___/\\__|");
            Console.WriteLine($"                    |___/        |_|                                        ");
            Console.WriteLine();


            foreach (Player player in Players)
            {
                Console.WriteLine($"Player{player.PlayerId} Score: {player.Score}/{RaceLength} Accuracy: {(player.Score / (float)RaceLength) * 100}% Time: {player.TotalTime}");
            }

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}
