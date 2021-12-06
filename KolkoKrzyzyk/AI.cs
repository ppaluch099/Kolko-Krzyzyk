using System;
using System.Collections.Generic;

namespace KolkoKrzyzyk
{
    class AI
    {
        static int[,] convert = new int[,] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };
        static List<int> corners = new List<int>() { 1, 3, 7, 9 };
        static List<int> sides = new List<int>() { 2, 4, 6, 8 };
        static Random rnd = new Random();
        static int COM, position;
        static Boolean moved = false;
        static List<int> movesCOM = new List<int>();

        public static void computer()
        {
            COM = calculateMove();
            int[] temp = parseChoice(COM);
            if (WinConditions.isEmpty(temp[0], temp[1]))
            {
                KolkoKrzyzyk.field[temp[0], temp[1]] = 'O';
                KolkoKrzyzyk.playerTurn = true;
                movesCOM.Add(COM);
            }
            else
            {
                computer();
            }
            if (movesCOM.Count == 3)
            {
                WinConditions.finalMove++;
            }
        }

        public static void computerFinal()
        {
            char[,] fieldCopy = (char[,])KolkoKrzyzyk.field.Clone();
            for (int i = 0; i < movesCOM.Count; i++)
            {
                int[] temp = parseChoice(movesCOM[i]);
                int temp2 = calculateFinalMove(fieldCopy, temp, 'O');
                if (temp2 != 0)
                {
                    KolkoKrzyzyk.field[temp[0], temp[1]] = (char)(movesCOM[i] + '0');
                    temp = parseChoice(temp2);
                    KolkoKrzyzyk.field[temp[0], temp[1]] = 'O';
                    moved = true;
                    break;
                }
            }
            if(!moved)
            {
                for (int i = 0; i < movesCOM.Count; i++)
                {
                    int[] temp = parseChoice(movesCOM[i]);
                    int temp2 = calculateFinalMove(fieldCopy, temp, 'X');
                    if (temp2 != 0)
                    {
                        KolkoKrzyzyk.field[temp[0], temp[1]] = (char)(movesCOM[i] + '0');
                        temp = parseChoice(temp2);
                        KolkoKrzyzyk.field[temp[0], temp[1]] = 'O';
                        moved = true;
                        break;
                    }
                }
            }
            if (!moved)
            {
                for (int i = 0; i < 3; i++)
                {
                    int empty = findEmptyOrthagonal(movesCOM[i]);
                    if (empty != movesCOM[i])
                    {
                        int[] temp = parseChoice(movesCOM[i]);
                        KolkoKrzyzyk.field[temp[0], temp[1]] = (char)(movesCOM[i] + '0');
                        temp = parseChoice(empty);
                        KolkoKrzyzyk.field[temp[0], temp[1]] = 'O';
                        moved = true;
                        break;
                    }
                }
            }

        }

        static int calculateFinalMove(char[,] fieldCopy, int[] choice, char mark)
        {
            for(int i = 0; i < 3; i++)
            {
                if (choice[0] != i && WinConditions.isEmpty(i, choice[1])) {
                    fieldCopy[choice[0], choice[1]] = (char)(convert[choice[0], choice[1]] + '0');
                    fieldCopy[i, choice[1]] = mark;
                    if (copyWin(fieldCopy, mark))
                    {
                        return convert[i, choice[1]];
                    }
                    else
                    {
                        fieldCopy = (char[,])KolkoKrzyzyk.field.Clone();
                    }
                }
                else if (choice[1] != i && WinConditions.isEmpty(choice[0], i))
                {
                    fieldCopy[choice[0], choice[1]] = (char)(convert[choice[0], choice[1]] + '0');
                    fieldCopy[choice[0], i] = mark;
                    if (copyWin(fieldCopy, mark))
                    {
                        return convert[choice[0], i];
                    }
                    else
                    {
                        fieldCopy = (char[,])KolkoKrzyzyk.field.Clone();
                    }
                }
                fieldCopy = (char[,])KolkoKrzyzyk.field.Clone();
            }
            return 0;
        }

        static int calculateMove()
        {
            if (Player.playerMove.Count == 2 || movesCOM.Count == 2)
            {
                int[] temp = possibleWinOrDraw();
                if (temp[2] == 0)
                {
                    position = convert[temp[0], temp[1]];
                    return findEmptyOrthagonal(position);

                }
                else if (temp[2] == 1)
                {
                    position = convert[temp[0], temp[1]];
                    return position;
                }
            }
            if (WinConditions.isEmpty(1, 1))
            {
                position = 5;
                return position;
            }
            if ((movesCOM.Contains(1) || Player.playerMove.Contains(1)) && corners.Contains(1))
            {
                corners.Remove(1);
            }
            if ((movesCOM.Contains(3) || Player.playerMove.Contains(3)) && corners.Contains(3))
            {
                corners.Remove(3);
            }
            if ((movesCOM.Contains(7) || Player.playerMove.Contains(7)) && corners.Contains(7))
            {
                corners.Remove(7);
            }
            if ((movesCOM.Contains(9) || Player.playerMove.Contains(9)) && corners.Contains(9))
            {
                corners.Remove(9);
            }
            if ((movesCOM.Contains(2) || Player.playerMove.Contains(2)) && sides.Contains(2))
            {
                sides.Remove(2);
            }
            if ((movesCOM.Contains(4) || Player.playerMove.Contains(4)) && sides.Contains(4))
            {
                sides.Remove(4);
            }
            if ((movesCOM.Contains(6) || Player.playerMove.Contains(6)) && sides.Contains(6))
            {
                sides.Remove(6);
            }
            if ((movesCOM.Contains(8) || Player.playerMove.Contains(8)) && sides.Contains(8))
            {
                sides.Remove(8);
            }
            if (WinConditions.isEmpty(0, 0) || WinConditions.isEmpty(2, 0) || WinConditions.isEmpty(0, 2) || WinConditions.isEmpty(2, 2))
            {
                while (corners.Count > 0)
                {
                    position = rnd.Next(corners.Count);
                    int[] possibleMove = parseChoice(corners[position]);
                    if (WinConditions.isEmpty(possibleMove[0], possibleMove[1]))
                    {
                        return corners[position];
                    }
                    else
                    {
                        corners.RemoveAt(position);
                    }
                }
            }
            if (WinConditions.isEmpty(1, 0) || WinConditions.isEmpty(0, 1) || WinConditions.isEmpty(1, 2) || WinConditions.isEmpty(2, 0))
            {
                while (sides.Count > 0)
                {
                    position = rnd.Next(sides.Count);
                    int[] possibleMove = parseChoice(sides[position]);
                    if (WinConditions.isEmpty(possibleMove[0], possibleMove[1]))
                    {
                        return sides[position];
                    }
                    else
                    {
                        sides.RemoveAt(position);
                    }
                }
            }
            return position;
        }

        static Boolean copyWin(char[,] fieldCopy, char mark)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (fieldCopy[i,j] != mark)
                    {
                        break;
                    }
                    if (j == 2)
                    {
                        return true;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (fieldCopy[j, i] != mark)
                    {
                        break;
                    }
                    if (j == 2)
                    {
                        return true;
                    }
                }
            }
            for(int i = 0; i < 3; i++)
            {
                if (fieldCopy[i, i] != mark)
                {
                    break;
                }
                if(i == 2)
                {
                    return true;
                }
            }
            int k = 2;
            for(int i = 0; i < 3; i++)
            {
                if (fieldCopy[i,k] != mark)
                {
                    break;
                }
                if (i == 2)
                {
                    return true;
                }
                k--;
            }
            return false;
        }

        static int[] parseChoice(int COM)
        {
            int[] arr = new int[2];
            switch (COM)
            {
                case 1:
                    arr[0] = 0;
                    arr[1] = 0;
                    break;
                case 2:
                    arr[0] = 1;
                    arr[1] = 0;
                    break;
                case 3:
                    arr[0] = 2;
                    arr[1] = 0;
                    break;
                case 4:
                    arr[0] = 0;
                    arr[1] = 1;
                    break;
                case 5:
                    arr[0] = 1;
                    arr[1] = 1;
                    break;
                case 6:
                    arr[0] = 2;
                    arr[1] = 1;
                    break;
                case 7:
                    arr[0] = 0;
                    arr[1] = 2;
                    break;
                case 8:
                    arr[0] = 1;
                    arr[1] = 2;
                    break;
                case 9:
                    arr[0] = 2;
                    arr[1] = 2;
                    break;
                default: break;
            }
            return arr;
        }

        static int[] possibleWinOrDraw()
        {
            int[] arr = new int[3] { 0, 0, -1 };
            for (int i = 0; i < 3; i++)
            {
                Boolean two = false;
                if (KolkoKrzyzyk.field[1, i] == KolkoKrzyzyk.field[0, i] || KolkoKrzyzyk.field[1, i] == KolkoKrzyzyk.field[2, i] || KolkoKrzyzyk.field[2, i] == KolkoKrzyzyk.field[0, i])
                {
                    two = true;
                }
                if (two)
                {
                    arr[1] = i;
                    if (WinConditions.isEmpty(1, i))
                    {
                        arr[0] = 1;
                        if (KolkoKrzyzyk.field[0, i] == 'O')
                        {
                            arr[2] = 0;
                        }
                        else
                        {
                            arr[2] = 1;
                        }
                    }
                    else if (WinConditions.isEmpty(0, i))
                    {
                        arr[0] = 0;
                        if (KolkoKrzyzyk.field[1, i] == 'O')
                        {
                            arr[2] = 0;
                        }
                        else
                        {
                            arr[2] = 1;
                        }
                    }
                    else if (WinConditions.isEmpty(2, i))
                    {
                        arr[0] = 2;
                        if (KolkoKrzyzyk.field[1, i] == 'O')
                        {
                            arr[2] = 0;
                        }
                        else
                        {
                            arr[2] = 1;
                        }
                    }
                    return arr;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Boolean two = false;
                if (KolkoKrzyzyk.field[i, 1] == KolkoKrzyzyk.field[i, 0] || KolkoKrzyzyk.field[i, 1] == KolkoKrzyzyk.field[i, 2] || KolkoKrzyzyk.field[i, 2] == KolkoKrzyzyk.field[i, 0])
                {
                    two = true;
                }
                if (two)
                {
                    arr[0] = i;
                    if (WinConditions.isEmpty(i, 1))
                    {
                        arr[1] = 1;
                        if (KolkoKrzyzyk.field[i, 0] == 'O')
                        {
                            arr[2] = 0;
                        }
                        else
                        {
                            arr[2] = 1;
                        }
                    }
                    else if (WinConditions.isEmpty(i, 0))
                    {
                        arr[1] = 0;
                        if (KolkoKrzyzyk.field[i, 1] == 'O')
                        {
                            arr[2] = 0;
                        }
                        else
                        {
                            arr[2] = 1;
                        }
                    }
                    else if (WinConditions.isEmpty(i, 2))
                    {
                        arr[1] = 2;
                        if (KolkoKrzyzyk.field[i, 1] == 'O')
                        {
                            arr[2] = 0;
                        }
                        else
                        {
                            arr[2] = 1;
                        }
                    }
                    return arr;
                }
            }

            Boolean twoDiagonal = false;
            if (KolkoKrzyzyk.field[0, 0] == KolkoKrzyzyk.field[1, 1] || KolkoKrzyzyk.field[1, 1] == KolkoKrzyzyk.field[2, 2] || KolkoKrzyzyk.field[0, 0] == KolkoKrzyzyk.field[2, 2])
            {
                twoDiagonal = true;
            }
            if (twoDiagonal)
            {
                if (WinConditions.isEmpty(0, 0))
                {
                    arr[0] = 0;
                    arr[1] = 0;
                    if (KolkoKrzyzyk.field[1, 1] == 'O')
                    {
                        arr[2] = 0;
                    }
                    else
                    {
                        arr[2] = 1;
                    }
                }
                else if (WinConditions.isEmpty(1, 1))
                {
                    arr[0] = 1;
                    arr[1] = 1;
                    if (KolkoKrzyzyk.field[0, 0] == 'O')
                    {
                        arr[2] = 0;
                    }
                    else
                    {
                        arr[2] = 1;
                    }
                }
                else if (WinConditions.isEmpty(2, 2))
                {
                    arr[0] = 2;
                    arr[1] = 2;
                    if (KolkoKrzyzyk.field[1, 1] == 'O')
                    {
                        arr[2] = 0;
                    }
                    else
                    {
                        arr[2] = 1;
                    }
                }
            }
            Boolean twoAntiDiagonal = false;
            if (KolkoKrzyzyk.field[0, 2] == KolkoKrzyzyk.field[1, 1] || KolkoKrzyzyk.field[1, 1] == KolkoKrzyzyk.field[2, 0] || KolkoKrzyzyk.field[0, 2] == KolkoKrzyzyk.field[2, 0])
            {
                twoAntiDiagonal = true;
            }
            if (twoAntiDiagonal)
            {
                if (WinConditions.isEmpty(0, 2))
                {
                    arr[0] = 0;
                    arr[1] = 2;
                    if (KolkoKrzyzyk.field[1, 1] == 'O')
                    {
                        arr[2] = 0;
                    }
                    else
                    {
                        arr[2] = 1;
                    }
                }
                else if (WinConditions.isEmpty(1, 1))
                {
                    arr[0] = 1;
                    arr[1] = 1;
                    if (KolkoKrzyzyk.field[2, 0] == 'O')
                    {
                        arr[2] = 0;
                    }
                    else
                    {
                        arr[2] = 1;
                    }
                }
                else if (WinConditions.isEmpty(2, 0))
                {
                    arr[0] = 2;
                    arr[1] = 0;
                    if (KolkoKrzyzyk.field[0, 2] == 'O')
                    {
                        arr[2] = 0;
                    }
                    else
                    {
                        arr[2] = 1;
                    }
                }
            }
            return arr;
        }

        static int findEmptyOrthagonal(int position)
        {
            int[] temp = parseChoice(position);
            for (int i = 0; i < 3; i++)
            {
                if (WinConditions.isEmpty(temp[0], i) && i != temp[1])
                {
                    return convert[temp[0], i];
                }

                else if (WinConditions.isEmpty(i, temp[1]) && i != temp[0])
                {
                    return convert[i, temp[1]];
                }
            }

            return position;
        }

    }
}