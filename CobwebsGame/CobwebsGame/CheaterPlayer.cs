using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CobwebsGame
{
    class CheaterPlayer : IObserver, IObservable
    {
        List<IObserver> players;
        List<int> numToRand;
        Random rand;
        int num;
        int chosenNumber;

        public CheaterPlayer(int chosenNumber)
        {
            players = new List<IObserver>();
            this.chosenNumber = chosenNumber;
            numToRand = new List<int>();
            rand = new Random();
            for (int i = 41; i <= 141; i++)
            {
                numToRand.Add(i);
            }
        }
        public void Update(int num)
        {
            numToRand.Remove(num);
        }

        public void Guess()
        {
            if (numToRand.Count > 0)
            {
                int index = rand.Next(0, numToRand.Count);
                num = numToRand[index];
                numToRand.Remove(num);
                Console.WriteLine("Cheater Player: {0}", num);
                Notify();
                int delta = Math.Abs(chosenNumber - num);
                Thread.Sleep(delta);
            }
            else
            {
                Console.WriteLine("Run out the numbers");
            }
        }

        public void Add(IObserver o)
        {
            players.Add(o);
        }

        public void Remove(IObserver o)
        {
            players.Remove(o);
        }

        public void Notify()
        {
            foreach (var o in players)
            {
                o.Update(num);
            }
        }
        public int getNum()
        {
            return this.num;
        }

        
    }
}
