using System;
using System.Collections.Generic;
using System.Linq;

namespace KolkoKrzyzyk
{
    class Player
    {
        static int input, change;
        static String checkInput, changeInput;
        static int[] playerXY;
        public static List<int> playerMove = new List<int>();
        public static void player()
        {
            Console.WriteLine("Wybierz pole z zakresu 1 - 9");
            String temp = Console.ReadLine();
            if (int.TryParse(temp, out input))
            {
                if (Enumerable.Range(1,9).Contains(input))
                {
                    playerXY = parseInput(input);
                    if (WinConditions.isEmpty(playerXY[0], playerXY[1]))
                    {
                        KolkoKrzyzyk.field[playerXY[0], playerXY[1]] = 'X';
                        KolkoKrzyzyk.playerTurn = false;
                        playerMove.Add(input);
                    }
                    else
                    {
                        Console.WriteLine("Wybierz dostepne puste pole");
                        player();
                    }
                    if (playerMove.Count == 3)
                    {
                        WinConditions.finalMove++;
                    }
                }
                else 
                {
                    Console.WriteLine("Cyfra spoza zakresu"); 
                }
            }
            else
            {
                Console.WriteLine("Niepoprawny znak");
                System.Threading.Thread.Sleep(1000);
                player();
            }
        }


        public static void playerFinal()
        {
            Boolean notDone = true;
            List<int> playerDisplayMove = new List<int>();
            foreach (int x in playerMove)
            {
                playerDisplayMove.Add(x);
            }
            Console.WriteLine($"Wybierz jeden ze znakow:({string.Join("|", playerDisplayMove)}) do przesuniecia");
            checkInput = Console.ReadLine();
            if (int.TryParse(checkInput, out input))
            {
                if ((playerMove.Contains(input)))
                {
                    Console.WriteLine("Wybierz pozycje w pionie lub poziomie na ktora chcesz przesunac znak");
                    while (notDone)
                    {
                        changeInput = Console.ReadLine();
                        if (int.TryParse(changeInput, out change))
                        {
                            if (Enumerable.Range(1, 9).Contains(change))
                            {
                                int[] temp_change = parseInput(change);
                                int[] temp = parseInput(input);
                                if (isOrthogonal(temp, temp_change))
                                {
                                    if (WinConditions.isEmpty(temp_change[0], temp_change[1]))
                                    {
                                        KolkoKrzyzyk.field[temp_change[0], temp_change[1]] = 'X';
                                        KolkoKrzyzyk.playerTurn = false;
                                        KolkoKrzyzyk.field[temp[0], temp[1]] = (char)(input + '0');
                                        notDone = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Pozycja nie jest pusta");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Pozycja nie jest w pionie lub poziomie w stosunku do pozycji poczatkowej");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Cyfra spoza zakresu");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wpisz poprawna pozycje");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Wprowadz jedna z podanych cyfr: ({string.Join("|", playerDisplayMove)})");
                    System.Threading.Thread.Sleep(1000);
                    playerFinal();
                }
            }
            else
            {
                Console.WriteLine($"Wprowadz jedna z cyfr z pozycja znaku: ({string.Join("|", playerDisplayMove)})");
                System.Threading.Thread.Sleep(1000);
                playerFinal();
            }
        }

        static int[] parseInput(int input)
        {
            int[] arr = new int[2];
            switch (input)
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
                default: Console.WriteLine("Cyfra spoza zakresu"); break;
            }
            return arr;
        }

        static Boolean isOrthogonal(int[] input, int[] change)
        {
            if (input[0] == change[0] || input[1] == change[1])
            {
                return true;
            }
            return false;
        }
    }
}
