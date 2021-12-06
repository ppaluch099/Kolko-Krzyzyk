using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace KolkoKrzyzyk
{
    class WinConditions
    {
        public static Boolean playerWin = false, computerWin = false;
        public static int finalMove = 0;

        public static Boolean isEmpty(int x, int y)
        {
            if (KolkoKrzyzyk.field[x,y] == 'X' || KolkoKrzyzyk.field[x,y] == 'O')
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void checkWin(char mark)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(KolkoKrzyzyk.field[j,i] != mark)
                    {
                        break;
                    }
                    if(j == 2)
                    {
                        if (mark == 'X')
                        {
                            playerWin = true;
                        }
                        else
                        {
                            computerWin = true;
                        }
                    }
                }
            }

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (KolkoKrzyzyk.field[i, j] != mark)
                    {
                        break;
                    }
                    if (j == 2)
                    {
                        if (mark == 'X')
                        {
                            playerWin = true;
                        }
                        else
                        {
                            computerWin = true;
                        }
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if(KolkoKrzyzyk.field[i,i] != mark)
                {
                    break;
                }
                if (i == 2)
                {
                    if (mark == 'X')
                    {
                        playerWin = true;
                    }
                    else
                    {
                        computerWin = true;
                    }
                }
            }
            int k = 2;
            for (int i = 0; i < 3; i++)
            {
                if(KolkoKrzyzyk.field[i, k] != mark)
                {
                    break;
                }
                if (i == 2)
                {
                    if (mark == 'X')
                    {
                        playerWin = true;
                    }
                    else
                    {
                        computerWin = true;
                    }
                }
                k--;

            }
        }

        public static void isFinished()
        {
            if ((playerWin && computerWin) || (playerWin == false && computerWin == false))
            {
                KolkoKrzyzyk.drawBoard();
                Console.WriteLine("\nRemis");
            }
            else if (playerWin && computerWin == false)
            {
                KolkoKrzyzyk.drawBoard();
                Console.WriteLine("\nWygrales");
            }
            else if (playerWin == false && computerWin)
            {
                KolkoKrzyzyk.drawBoard();
                Console.WriteLine("\nPrzegrales");
            }
        }
    }
}
