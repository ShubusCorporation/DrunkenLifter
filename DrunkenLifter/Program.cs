using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace DrunkenLifter
{
    class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        const int MIN_LENGTH_STAIR = 10;
        const int SW_SHOWMAXIMIZED = 3;

        static void Game()
        {
            int sy = Console.WindowHeight;
            int sx = Console.WindowWidth;

            Platform pl = new Platform(sx / 4, sy - sy / 4, sx / 8, (char)0x2588);
            Stairs st = new Stairs(sx / 2, (sy / 4) < MIN_LENGTH_STAIR ? MIN_LENGTH_STAIR : sy / 4);
            Lifter mn = new Lifter(pl, st);
            pl.addObject(mn);
            bool alive = true;

            while (alive && mn.Escaped == false)
            {
                Thread.Sleep(99);
                Console.CursorVisible = false;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo kInf = Console.ReadKey(true);

                    switch (kInf.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            pl.goLeft();
                            break;

                        case ConsoleKey.RightArrow:
                            pl.goRight();
                            break;

                        case ConsoleKey.UpArrow:
                            if (pl.Y > st.length)
                            {
                                pl.goUp();
                            }
                            break;

                        case ConsoleKey.DownArrow:
                            if (mn.Y > pl.Y)
                                pl.moveY(pl.getMaxPlatformY() - pl.Y);
                            else
                                pl.goDown();
                            break;

                        case ConsoleKey.F11:
                        case ConsoleKey.Enter:
                            Console.CursorVisible = false;
                            break;

                        default:
                            break;
                    }
                    while (Console.KeyAvailable) { Console.ReadKey(true); }
                }
                alive = mn.moveLifter(pl);
            }
            writeGameResult(alive);
        }

        static void writeGameResult(bool win)
        {
            if (win)
            {
                Console.SetCursorPosition(6, 10);
                Console.WriteLine("Пьяный Лифтер\r\nБлагодарит Вас за спасение!\r\n");
                Console.SetCursorPosition(6, 15);
                Console.WriteLine("Drunken Lifter\r\nThanks You for salvation and promises \r\ndo not drink Vodka any more.\r\n");
            }
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Alas, Drunken Lifter is dead.\r\nIt's because he drank too much vodka...\r\n");
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Drunken Lifter";
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(6, 0);
            Console.WriteLine("\t\tDrunken Lifter game.\r\n");
            Console.WriteLine("\tYou should help Drunken Lifter to get salvation.");
            Console.WriteLine("\tUse arrow keys to move the platform.\r\n\tYou can drop the platform in case of falling.\r\n");
 
            while (true)
            {
                Console.WriteLine("\tResize the window to the size you like to play with.");
                Console.WriteLine("\tPress F11 or Alt + Enter to maximize the window.");
                Console.WriteLine("\tThen press any key to continue...");
                while (Console.KeyAvailable == false) { }

                Console.CursorVisible = false;
                Console.Clear();
                Console.ReadKey(true);

                if (Console.WindowHeight <= MIN_LENGTH_STAIR)
                {
                    Console.WriteLine("\tError : Incorrect window size");
                    continue;
                }
                Game();
                
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                Console.WriteLine("Press Esc for quit");
                Console.WriteLine("Press any key for new game");
                ConsoleKeyInfo kInf = Console.ReadKey(true);

                if (kInf.Key == ConsoleKey.Escape)
                {
                    break;
                }
                Console.Clear();
            }
        }
    }
}