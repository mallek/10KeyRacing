using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace _10KeyRacing
{
    class Program
    {
        static void Main(string[] args)
        {
            var Players = new List<Player>();
            int PlayerCount = 0;
            int RaceLength = 0;

            Random rand = new Random();

            WriteBanner();

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
                    player.TotalKeys += thisQuestion.ToString().Length;
                    Console.WriteLine(thisQuestion);
                    int.TryParse(Console.ReadLine(), out var answer);
                    sw.Stop();
                    if (answer == thisQuestion)
                    {
                        player.Score += 1;
                    }
                    player.TotalTime += sw.Elapsed;
                }
            }

            Console.Clear();
            WriteBanner();


            foreach (Player player in Players)
            {
                WriteScoreCard(player, RaceLength, Players.IndexOf(player) + 1);
                //Console.WriteLine($"Player{player.PlayerId} Score: {player.Score}/{RaceLength} Accuracy: {(player.Score / (float)RaceLength) * 100}% kph:{player.KeysPerHour:N0} Time: {player.TotalTime}");
            }

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }


        internal class Player
        {
            public int PlayerId { get; set; }
            public int Score { get; set; }
            public TimeSpan TotalTime { get; set; }
            public int TotalKeys { get; set; }

            public long KeysPerHour
            {
                get
                {
                    var ticksPerHour = TimeSpan.FromHours(1).Ticks;
                    var keysPerHour = ticksPerHour / this.TotalTime.Ticks * this.TotalKeys;
                    return keysPerHour;
                }
            }

            public string CalculateScore(long raceLength)
            {
                return $" {(this.Score / raceLength):P1}";
            }

        }

        private static void WriteScoreCard(Player player, int raceLength, int index)
        {
            Console.WriteLine($"|==================================================================================================|");
            Console.WriteLine($"|                                   Player {index.ToString().PadLeft(2)}                                                      |");
            Console.WriteLine($"|Player Score: {player.CalculateScore(raceLength).PadLeft(12)}                                                                        |");
            Console.WriteLine($"|Accuracy: {(player.Score / raceLength).ToString("P1").PadLeft(16)}                                                                        |");
            Console.WriteLine($"|KPH: {player.KeysPerHour.ToString("N0").PadLeft(21)}                                                                        |");
            Console.WriteLine($"|Time: {player.TotalTime.ToString().PadLeft(20)}                                                                        |");
            Console.WriteLine($"|==================================================================================================|");
        }


        private static void WriteBanner()
        {
            Console.WriteLine($" _  ___    _  __            ____                      _   _____         _   ");
            Console.WriteLine($"/ |/ _ \\  | |/ /___ _   _  / ___| _ __   ___  ___  __| | |_   _|__  ___| |_ ");
            Console.WriteLine($"| | | | | | ' // _ \\ | | | \\___ \\| '_ \\ / _ \\/ _ \\/ _` |   | |/ _ \\/ __| __|");
            Console.WriteLine($"| | |_| | | . \\  __/ |_| |  ___) | |_) |  __/  __/ (_| |   | |  __/\\__ \\ |_ ");
            Console.WriteLine($"|_|\\___/  |_|\\_\\___|\\__, | |____/| .__/ \\___|\\___|\\__,_|   |_|\\___||___/\\__|");
            Console.WriteLine($"                    |___/        |_|                                        ");
            Console.WriteLine();
        }
    }
}
