using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DrunkenLifter
{
    class Stairs
    {
        public int x;
        public int length;
        public char block = '#';

        public Stairs(int ax, int al)
        {
            x = ax;
            length = al;
            draw();
        }

        void draw()
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(block);
            }
        }
    }
}