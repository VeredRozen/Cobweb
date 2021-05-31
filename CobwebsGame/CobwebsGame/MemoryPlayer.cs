using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CobwebsGame
{
    class MemoryPlayer : IObservable, IPlayer
    {
        Random rand;
        List<int> numToRand;
        int num;
        List<IObserver> players;
        int chosenNumber;
        public MemoryPlayer(int chosenNumber)
        {
            players = new List<IObserver>();
            this.chosenNumber = chosenNumber;
            rand = new Random();
            numToRand = new List<int>();
            for (int i=41; i<=141; i++)
            {
                numToRand.Add(i);
            }
        }

        public void Add(IObserver o)
        {
            players.Add(o);
        }

        public void Guess()
        {
            int index = rand.Next(0, numToRand.Count);
            num = numToRand[index];
            numToRand.Remove(num);
            Console.WriteLine("Memory Player: {0}", num);
            Notify();
            int delta = Math.Abs(chosenNumber - num);
            Thread.Sleep(delta);
        }

        public void Notify()
        {
            foreach (var p in players)
            {
                p.Update(num);
            }
        }

        public void Remove(IObserver o)
        {
            players.Remove(o);
        }

        public int getNum()
        {
            return this.num;
        }
    }
}
