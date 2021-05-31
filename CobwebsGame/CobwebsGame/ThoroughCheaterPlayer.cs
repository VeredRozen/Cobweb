using System;
using System.Collections.Generic;
using System.Text;

namespace CobwebsGame
{
    class ThoroughCheaterPlayer : IObserver, IObservable
    {
        List<IObserver> players;
        List<int> numToRand;
        int num, guessed;
        int chosenNumber;

        public ThoroughCheaterPlayer(int chosenNumber)
        {
            this.chosenNumber = chosenNumber;
            players = new List<IObserver>();
            numToRand = new List<int>();
            for (int i = 41; i <= 141; i++)
            {
                numToRand.Add(i);
            }
            num = 41;
        }
        public void Update(int num)
        {
            numToRand.Remove(num);
        }

        public void Guess()
        {
            if (numToRand.Count > num)
            {
                int guessed = numToRand[num];
                numToRand.Remove(guessed);
                Console.WriteLine("Thorough Cheater Player: {0}", num);
                Notify();
                num++;
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
            return this.guessed;
        }
    }
}
