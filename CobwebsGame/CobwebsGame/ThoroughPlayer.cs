using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CobwebsGame
{
    class ThoroughPlayer : IObservable
    {
        List<IObserver> players;
        int num;
        int chosenNumber;

        public ThoroughPlayer(int chosenNumber)
        {
            this.chosenNumber = chosenNumber;
            this.players = new List<IObserver>();
            num = 41;
        }

        public void Add(IObserver o)
        {
            this.players.Add(o);
        }

        public void Guess()
        {
            num++;
            Console.WriteLine("Thorough Player: {0}", num);
            Notify();
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
