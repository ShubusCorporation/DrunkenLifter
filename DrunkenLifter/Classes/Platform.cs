using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrunkenLifter
{
    class Platform
    {
        public int x;
        public int y;
        public int length;
        public char block = (char)0x2588; //'█';
        List<IPhysicalObject> plObjects = new List<IPhysicalObject>();

        public void AddObject(IPhysicalObject obj)
        {
            plObjects.Add(obj);
        }

        public Platform(int ax, int ay, int al)
        {
            x = ax;
            y = ay;
            length = al;
            draw(block);
        }

        public int getMaxPlatformY()
        {
            return Console.WindowHeight - 3;
        }

        private bool isObjectOnPlatform(IPhysicalObject lf)
        {
            return (lf.Y() <= y && lf.Y() >= y - 2) && (lf.X() >= x) && (lf.X() < x + length);
        }

        public void moveY(int dy)
        {
            if (dy == 0) return;

            if (y + dy > 0 && y + dy <= getMaxPlatformY())
            {
                foreach (IPhysicalObject lf in this.plObjects)
                {
                    if (isObjectOnPlatform(lf))
                    {
                        if (dy < 0)
                            lf.goUp(this.block);
                        if (dy > 0)
                            lf.goDown();
                    }
                }
                draw(' ');
                y += dy;

                if (dy > 1) // Platfor has been dropped
                {
                    foreach (IPhysicalObject lf in this.plObjects)
                    {
                        if (isObjectOnPlatform(lf) && lf.Y() == y)
                        {
                            lf.goDown();
                        }
                    }
                }
                draw(block);
            }
        }

        public void draw(char ch)
        {
            for (int i = x; i < x + length; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write(ch);
            }
        }

        public void moveLeft()
        {
            if (x == 0) return;

            x--;
            Console.SetCursorPosition(x, y);
            Console.Write(block);
            Console.SetCursorPosition(x + length, y);
            Console.Write(' ');

            foreach (IPhysicalObject lf in this.plObjects)
            {
                if (isObjectOnPlatform(lf))
                {
                    if (lf.Y() == y)
                    {
                        lf.goUp(block);
                    }
                    else lf.goLeft();
                }
            }
        }

        public void moveRigth()
        {
            if (x + length + 1 > Console.WindowWidth) return;

            Console.SetCursorPosition(x, y);
            Console.Write(' ');
            Console.SetCursorPosition(x + length, y);
            Console.Write(block);
            x++;

            foreach (IPhysicalObject lf in this.plObjects)
            {
                if (isObjectOnPlatform(lf))
                {
                    if (lf.Y() == y)
                    {
                        lf.goUp(block);
                    }
                    else lf.goRight();
                }
            }
        }
    }
}
