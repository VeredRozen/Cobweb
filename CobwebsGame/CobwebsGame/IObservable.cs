using System;
using System.Collections.Generic;
using System.Text;

namespace CobwebsGame
{
    interface IObservable
    {
        public void Add(IObserver o);
        public void Remove(IObserver o);
        public void Notify();
        public void Guess();
    }
}
