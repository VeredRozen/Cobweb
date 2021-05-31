using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CobwebsGame
{
    class RandomPlayer : IObservable
    {
        List<IObserver> players;
        Random rand;
        int num;
        int chosenNumber;
        public RandomPlayer(int chosenNumber)
        {
            this.players = new List<IObserver>();
            this.chosenNumber = chosenNumber;
            rand = new Random();
        }

        public void Add(IObserver o)
        {
            this.players.Add(o);
        }

        public void Guess()
        {
            num = rand.Next(40, 140);
            Notify();
            Console.WriteLine("Random Player: {0}", num);
            int delta = Math.Abs(chosenNumber - num);
            Thread.Sleep(delta);
        }

        public void Notify()
        {
            foreach (var o in players)
            {
                o.Update(num);
            }
        }

        public void Remove(IObserver o)
        {
            this.players.Remove(o);
        }

        public int getNum()
        {
            return this.num;
        }
    }
}
