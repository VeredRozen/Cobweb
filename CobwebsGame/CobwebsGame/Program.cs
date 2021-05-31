using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CobwebsGame
{
    class Program
    {
        const int maxTotalParticipants = 8;
        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Cobware's game! You can pick 2-8 players for each game.");

            Console.WriteLine("How many player do you want?");
            Console.WriteLine($@"Please write here a string that includes your choice by the roles:<br>
                1- for Random player,\n 
                2- for Memory player, \n
                3- for Cheater player, \n
                4- for Thorough player, \n
                5- for Thorough cheater player\n
                For example, the input: 23211
                will say: 2 memory players (because the number 2 uppears 2 times)\n
                1 cheater player (because the number 3 uppears 1 time)\n
                and 2 random players (because the number 1 uppears 2 times)");

            int totalParticipants = 0;
            string playersInput = Console.ReadLine();

            if (playersInput.Length > maxTotalParticipants)
            {
                Console.WriteLine("You only allowed to pick 2-8 players");
            }
            else
            {
                ThreadPool players = new ThreadPool();
                for (int i = 0; i < playersInput.Length; i++)
                {
                    int participantType = int.Parse(playersInput[i].ToString());
                    switch ((PlayersEnum)participantType)
                    {
                        case PlayersEnum.Random:
                            RandomPlayer randomPlayer = new RandomPlayer(100);
                            break;
                        case PlayersEnum.Memory:
                            MemoryPlayer memoryPlayer = new MemoryPlayer(100);
                            break;
                        case PlayersEnum.Cheater:
                            CheaterPlayer cheaterPlayer = new CheaterPlayer(100);
                            break;
                        case PlayersEnum.Thorough:
                            ThoroughPlayer thoroughPlayer = new ThoroughPlayer(100);
                            break;
                        case PlayersEnum.ThoroughCheater:
                            ThoroughCheaterPlayer thoroughCheaterPlayer = new ThoroughCheaterPlayer(100);
                            break;
                    }

                }
            }
            
            /*
            int chosenNumber = 100;
            //Observables:
            RandomPlayer rp = new RandomPlayer(chosenNumber);
            MemoryPlayer mp = new MemoryPlayer(chosenNumber);
            ThoroughPlayer tp = new ThoroughPlayer(chosenNumber);
            Thread randomThread = new Thread(() => rp.Guess());
            Thread memoryThread = new Thread(() => mp.Guess());
            Thread ThoroughThread = new Thread(() => tp.Guess());

            //Observaes:
            CheaterPlayer cp = new CheaterPlayer(chosenNumber);
            ThoroughCheaterPlayer tcp = new ThoroughCheaterPlayer(chosenNumber);
            Thread cheaterThread = new Thread(() => cp.Guess());
            Thread thoroughCheaterThread = new Thread(() => tcp.Guess());


            int randomTimer = 0, memoryTimer = 0, thoroughtTimer = 0;
            int cheaterTimer = 0, thoroughtCeaterTimer = 0;
            rp.Add(cp);
            rp.Add(tcp);
            mp.Add(cp);
            mp.Add(tcp);
            tp.Add(cp);
            tp.Add(tcp);


            List<int> l = new List<int>();
            int numr = 0, numm = 0, numc = 0, numt = 0, numtcp = 0;
            int chosed = 100;
            int count = 0;
            while ((numr != chosed &&numc != chosed && numm!=chosed && numt != chosed && numtcp != chosed) && count < 100)
            {
                Console.WriteLine("Round: {0}", count);
                randomThread.Start();
                memoryThread.Start();
                ThoroughThread.Start();
                cheaterThread.Start();
                thoroughCheaterThread.Start();


                Console.WriteLine(" ");

                count++;
            }*/
        }
    }
    
}
