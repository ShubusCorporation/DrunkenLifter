using System;

namespace DrunkenLifter
{
    class Lifter : IPhysicalObject
    {
        int x;
        int y;
        char man = (char)2;
        ConsoleColor mnColor = ConsoleColor.Yellow;
        string left = "";
        string rigth = "";
        const int maxRandom = 100;
        Random rnd = new Random();
        bool escaped = false;
        bool win = false;
        Stairs st;

        public bool Escaped { get { return this.escaped; } }
        public int X() { return this.x; }
        public int Y() { return this.y; }

        public Lifter(Platform pl, Stairs st)
        {
            left = man + " ";
            rigth = " " + man;
            this.st = st; 

            y = pl.y - 1;
            x = pl.x + pl.length / 2;
            draw();
        }

        public bool checkWin()
        {
            return (x == st.x && y <= st.length - 1);
        }

        public bool moveLifter(Platform pl)
        {
            if (!win)
            {
                win = checkWin();
            }
            if (win)
            {
                if (this.Escaped == false)
                {
                    goUp(st.block);
                }
                return true;
            }
            if (y < pl.y - 1 || y > pl.y)
            {
                bool ret = goDown();

                if (ret == false || y != pl.y - 1)
                {
                    return ret;
                }
            }
            if (x >= pl.x && x < pl.x + pl.length)
            {
                int dx = rnd.Next(1, maxRandom);

                if (dx < maxRandom / 2 + 1)
                {
                    this.goLeft();
                }
                else
                {
                    this.goRight();
                }
            }
            else
            {
                return goDown();
            }
            return true;
        }

        public void goUp(char ch)
        {
            if (y == 1)
            {
                this.escaped = true;
                this.mnColor = ConsoleColor.Green;                
            }
            clear(ch);
            y--;            
            draw();
            win = checkWin();
        }

        public bool goDown()
        {
            if (y >= Console.WindowHeight - 1)
            {
                this.mnColor = ConsoleColor.Red;
                this.draw();
                return false;
            }
            else
            {
                clear(' ');
                y++;
                draw();
            }
            return true;
        }

        public void goLeft()
        {
            if (win || x <= 0) return;
            Console.SetCursorPosition(--x, y);
            Console.ForegroundColor = mnColor;
            Console.Write(left);
            Console.ForegroundColor = ConsoleColor.White;
            win = checkWin();
        }

        public void goRight()
        {
            if (win || x >= Console.WindowWidth - 1) return;
            Console.SetCursorPosition(x++, y);
            Console.ForegroundColor = mnColor;
            Console.Write(rigth);
            Console.ForegroundColor = ConsoleColor.White;
            win = checkWin();
        }

        void clear(char ch)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(ch);
        }

        void draw()
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = mnColor;
            Console.Write(man);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
