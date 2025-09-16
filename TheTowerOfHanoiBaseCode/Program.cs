using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TheTowerOfHanoiBaseCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int>[] towers = new Stack<int>[3];
            Stack<int> buffer = new Stack<int>();
            int[][] displayTowers = new int[3][];
            int stackSize = 5;
            string thing = "|";
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta};

            int[] input = { -1, -1 };
            bool isNum = true;
            bool move = true;
            int moveCount = 0;
            int perfectMoveCount = (int)Math.Pow(2,stackSize) - 1;

            for (int x = 0; x < towers.Length; x++)
                towers[x] = new Stack<int>();

            for (int x = 0; x < stackSize; x++)
                towers[0].Push(stackSize - x);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\tMove Count: {moveCount}\n");
                for (int x = 0; x < towers.Length; x++)
                {
                    displayTowers[x] = new int[stackSize + 2];
                    for (int y = 0; y < displayTowers[x].Length; y++)
                        displayTowers[x][y] = -1;
                }

                for (int x = 0; x < towers.Length; x++)
                {
                    buffer = new Stack<int>();
                    while (towers[x].Count > 0)
                    {
                        displayTowers[x][displayTowers[x].Length - towers[x].Count] = towers[x].Peek();
                        buffer.Push(towers[x].Pop());
                    }

                    while (buffer.Count > 0)
                        towers[x].Push(buffer.Pop());
                }

                for (int x = 0; x < displayTowers[0].Length; x++)
                {
                    for (int y = 0; y < displayTowers.Length; y++)
                    {
                        thing = "|";
                        Console.ResetColor();
                        if (displayTowers[y][x] != -1)
                        {
                            Console.ForegroundColor = colors[displayTowers[y][x] % colors.Length];
                            thing = displayTowers[y][x] + "";
                        }

                        Console.Write($"\t-{thing}-");
                    }
                    Console.WriteLine();
                }

                Console.ResetColor();
                Console.WriteLine("\t---\t---\t---");

                if (towers[0].Count == 0 && towers[1].Count == 0)
                    break;

                Console.WriteLine("\n");
                do
                {
                    Console.Write("\tFrom what tower would you like to move the disk from? [0/1/2]\t");
                    isNum = int.TryParse(Console.ReadLine(), out input[0]);
                    if (isNum && (input[0] > 2 || input[0] < 0))
                        isNum = false;
                } while (!isNum);

                do
                {
                    Console.Write("\tFrom what tower would you like to move the disk to? [0/1/2]\t");
                    isNum = int.TryParse(Console.ReadLine(), out input[1]);
                    if (isNum && (input[1] > 2 || input[1] < 0))
                        isNum = false;
                    if (isNum && input[0] == input[1])
                        isNum = false;
                } while (!isNum);

                Console.WriteLine($"\tAttempting to move a disk from Tower {input[0]} to Tower {input[1]}");
                move = true;
                if (towers[input[0]].Count <= 0)
                {
                    move = false;
                    Console.WriteLine("\tThere is nothing to move from that tower...");
                }
                else if (towers[input[1]].Count > 0)
                {
                    if (towers[input[0]].Peek() > towers[input[1]].Peek())
                    {
                        move = false;
                        Console.WriteLine("\tYou are unable to place a larger disk on top of a smaller one...");
                    }
                }

                if (move)
                {
                    towers[input[1]].Push(towers[input[0]].Pop());
                    moveCount++;
                }
            }

            Console.WriteLine($"\tCONGRATULATIONS! You finished the game in {moveCount} moves!");
            Console.WriteLine($"\tThe perfect move count should be {perfectMoveCount}. . .");
            Console.WriteLine($"\tYour score is {100 - ((perfectMoveCount - moveCount)/perfectMoveCount)}%");

        }
    }
}
