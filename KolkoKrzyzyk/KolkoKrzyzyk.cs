using System;

namespace KolkoKrzyzyk
{
    class KolkoKrzyzyk
    {
        public static char[,] field = new char[,] { { '1', '4', '7' }, { '2', '5', '8' }, { '3', '6', '9' } };
        public static Boolean playerTurn = true;
        public static void drawBoard() {
            Console.Clear();
            Console.WriteLine($"   {field[0,2]}  |  {field[1,2]}  |  {field[2,2]}   ");
            Console.WriteLine("-------------------");
            Console.WriteLine($"   {field[0,1]}  |  {field[1,1]}  |  {field[2,1]}   ");
            Console.WriteLine("-------------------");
            Console.WriteLine($"   {field[0,0]}  |  {field[1,0]}  |  {field[2,0]}   ");
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            if(rnd.Next(2) != 1)
            {
                playerTurn = false;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            if (playerTurn)
            {
                playerStart();
            }
            else
            {
                AISTART();
            }
            WinConditions.checkWin('X');
            WinConditions.checkWin('O');
            WinConditions.isFinished();
        }

        static void playerStart()
        {
            while (WinConditions.finalMove < 2)
            {
                drawBoard();
                if (playerTurn)
                {
                    Player.player();
                }
                else
                {
                    AI.computer();
                    System.Threading.Thread.Sleep(500);
                }
            }
            drawBoard();
            Player.playerFinal();
            drawBoard();
            AI.computerFinal();
            System.Threading.Thread.Sleep(500);
            drawBoard();
        }

        static void AISTART()
        {
            while (WinConditions.finalMove < 2)
            {
                drawBoard();
                if (!playerTurn)
                {
                    AI.computer();
                }
                else
                {
                    Player.player();
                    System.Threading.Thread.Sleep(500);
                }
            }
            drawBoard();
            AI.computerFinal();
            System.Threading.Thread.Sleep(500);
            drawBoard();
            Player.playerFinal();
            drawBoard();
        }
    }
}
