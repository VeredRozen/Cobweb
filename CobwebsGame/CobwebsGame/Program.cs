using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CobwebsGame
{
    class Program
    {
        const int maxTotalParticipants = 8;
        const int minTotalParticipants = 2;
        const int chosenNumber = 100;

        private static Dictionary<PlayersEnum, List<IPlayer>> allPlayers;


        private static void UpdateObservablePlayers(Dictionary<PlayersEnum, List<IPlayer>> allPlayers, IObserver o)
        {
            if (allPlayers.ContainsKey(PlayersEnum.Random))
            {
                UpdateDictionary(PlayersEnum.Random, o, allPlayers);
            }
            if (allPlayers.ContainsKey(PlayersEnum.Memory))
            {
                UpdateDictionary(PlayersEnum.Memory, o, allPlayers);
            }
            if (allPlayers.ContainsKey(PlayersEnum.Thorough))
            {
                UpdateDictionary(PlayersEnum.Thorough, o, allPlayers);
            }
            if (allPlayers.ContainsKey(PlayersEnum.ThoroughCheater))
            {
                UpdateDictionary(PlayersEnum.Thorough, o, allPlayers);
            }
        }

        private static void UpdateDictionary(PlayersEnum pe, IObserver o, Dictionary<PlayersEnum, List<IPlayer>> allPlayers)
        {
            allPlayers.TryGetValue(pe, out List<IPlayer> observables);
            foreach (var p in observables)
            {
                p.Add(o);
            }
            allPlayers[pe] = observables;
        }
        public static Dictionary<PlayersEnum, List<IPlayer>> getAllPlayers(string playersInput, int chosenNumber)
        {
            Dictionary<PlayersEnum, List<IPlayer>> allPlayers = new Dictionary<PlayersEnum, List<IPlayer>>();
            for (int i = 0; i < playersInput.Length; i++)
            {
                int participantType = int.Parse(playersInput[i].ToString());
                switch ((PlayersEnum)participantType)
                {
                    case PlayersEnum.Random:
                        if (allPlayers.ContainsKey(PlayersEnum.Random))
                        {
                            allPlayers[PlayersEnum.Random].Add(new RandomPlayer(chosenNumber));
                        }
                        else
                        {
                            allPlayers.Add(PlayersEnum.Random, new List<IPlayer>() { new RandomPlayer(chosenNumber) });
                        }
                        break;
                    case PlayersEnum.Memory:
                        if (allPlayers.ContainsKey(PlayersEnum.Memory))
                        {
                            allPlayers[PlayersEnum.Memory].Add(new MemoryPlayer(chosenNumber));
                        }
                        else
                        {
                            allPlayers.Add(PlayersEnum.Memory, new List<IPlayer>() { new MemoryPlayer(chosenNumber) });
                        }
                        break;
                    case PlayersEnum.Cheater:
                        CheaterPlayer cp = new CheaterPlayer(chosenNumber);
                        UpdateObservablePlayers(allPlayers, cp);
                        if (allPlayers.ContainsKey(PlayersEnum.Cheater)){
                            allPlayers[PlayersEnum.Cheater].Add(cp);
                        }
                        else
                        {
                            allPlayers.Add(PlayersEnum.Cheater, new List<IPlayer>() { new CheaterPlayer(chosenNumber) });
                        }

                        break;
                    case PlayersEnum.Thorough:
                        if (allPlayers.ContainsKey(PlayersEnum.Thorough))
                        {
                            allPlayers[PlayersEnum.Thorough].Add(new ThoroughPlayer(chosenNumber));
                        }
                        else
                        {
                            allPlayers.Add(PlayersEnum.Thorough, new List<IPlayer>() { new ThoroughPlayer(chosenNumber) });
                        }
                        break;
                    case PlayersEnum.ThoroughCheater:
                        ThoroughCheaterPlayer tcp = new ThoroughCheaterPlayer(chosenNumber);
                        UpdateObservablePlayers(allPlayers, tcp);
                        allPlayers[PlayersEnum.ThoroughCheater].Add(tcp);
                        break;
                }
            }
            return allPlayers;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Cobware's game! You can pick 2-8 players for each game.");

            Console.WriteLine("How many player do you want?");
            Console.WriteLine($@"Please enter a string that includes your choice by the following roles:<br>
                1- for Random player,\n 
                2- for Memory player, \n
                3- for Cheater player, \n
                4- for Thorough player, \n
                5- for Thorough cheater player\n
                For example, the input: 23211
                will provide: 2 memory players (because the number 2 uppears 2 times)\n
                1 cheater player (because the number 3 uppears 1 time)\n
                and 2 random players (because the number 1 uppears 2 times)");

            string playersInput = Console.ReadLine();

            if (playersInput.Length > maxTotalParticipants || playersInput.Length < minTotalParticipants)
            {
                Console.WriteLine("You only allowed to pick 2-8 players");
            }
            else
            {
                allPlayers = getAllPlayers(playersInput, chosenNumber);
                int count = 0;
                while (count < 100)
                {
                    foreach (var playersGroup in allPlayers)
                    {
                        foreach (var player in playersGroup.Value)
                        {
                            Thread Thread = new Thread(() => player.Guess());
                            Thread.Start();
                            if (player.getNum() == chosenNumber)
                            {
                                bool lockWasTaken = false;
                                var temp = new Object();
                                try
                                {
                                    Monitor.Enter(temp, ref lockWasTaken);
                                    Console.WriteLine(playersGroup.Key + " player won!!!!!");
                                    Environment.Exit(Environment.ExitCode);
                                }
                                finally
                                {
                                    if (lockWasTaken)
                                    {
                                        Monitor.Exit(temp);
                                    }
                                }

                                }
                            };
                               
                        }
                    }
                    count++;
                }
        }
    }
    
}
