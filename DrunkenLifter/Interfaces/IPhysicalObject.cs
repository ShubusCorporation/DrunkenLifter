using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrunkenLifter
{
    interface IPhysicalObject
    {
        void goLeft();
        void goRight();
        void goUp(char ch);
        bool goDown();
        int X();
        int Y();
    }
}
