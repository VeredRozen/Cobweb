using System;
using System.Collections.Generic;
using System.Text;

namespace CobwebsGame
{
    interface IPlayer
    {
        public void Guess();
        public void Add(IObserver o);
        public int getNum();
    }
}
