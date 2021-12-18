using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrunkenLifter
{
    class Platform : IPhysicalObject
    {
        private int x;
        private  int y;
        public int length;

        private char block;
        private string platform;
        private string hidePlatform;
        private string hidePlatformX0;

        private List<IPhysicalObject> plObjects = new List<IPhysicalObject>();

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public void addObject(IPhysicalObject obj)
        {
            plObjects.Add(obj);
        }

        public Platform(int ax, int ay, int alength, char ablock)
        {
            x = ax;
            y = ay;
            length = alength;
            block = ablock;
            platform = new string(block, length);
            hidePlatform = new string(' ', length + 1);
            hidePlatformX0 = new string(' ', length);
            draw();
        }

        public int getMaxPlatformY()
        {
            return Console.WindowHeight - 3;
        }

        private bool isObjectOnPlatform(IPhysicalObject lf)
        {
            return (lf.Y <= y && lf.Y >= y - 2) && (lf.X >= x) && (lf.X < x + length);
        }

        public void goUp(char no = '\0')
        {
            moveY(-1);
        }

        public bool goDown()
        {
            moveY(1);
            return true;
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
                hide();
                y += dy;

                if (dy > 1) // Platfor has been dropped
                {
                    foreach (IPhysicalObject lf in this.plObjects)
                    {
                        if (isObjectOnPlatform(lf) && lf.Y == y)
                        {
                            lf.goDown();
                        }
                    }
                }
                draw();
            }
        }

        public void draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(platform);
        }

        public void hide()
        {
            var hide = x == 0 ? hidePlatformX0 : hidePlatform;
            var dx = x == 0 ? 0 : 1;
            Console.SetCursorPosition(x - dx, y);
            Console.Write(hide);
        }

        public void goLeft()
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
                    if (lf.Y == y)
                    {
                        lf.goUp(block);
                    }
                    else lf.goLeft();
                }
            }
        }

        public void goRight()
        {
            if (x + length == Console.WindowWidth)
                return;

            var cursorDx = x == 0 ? 0 : 1;
            var clearBlock = x == 0 ? " " : "  ";

            Console.SetCursorPosition(x - cursorDx, y);
            Console.Write(clearBlock);

            Console.SetCursorPosition(x + length, y);
            Console.Write(block);
            x++;

            foreach (IPhysicalObject lf in this.plObjects)
            {
                if (isObjectOnPlatform(lf))
                {
                    if (lf.Y == y)
                    {
                        lf.goUp(block);
                    }
                    else lf.goRight();
                }
            }
        }
    }
}
