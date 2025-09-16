using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
            int stackSize = 5;
            int[] input = { -1, -1 };
            int moveCount = 0;

            startingPoint(towers, stackSize, input, moveCount);
        }
        static void startingPoint(Stack<int>[] towers, int stackSize, int[] input, int moveCount)
        {
            initialtower(towers, stackSize);
            while (true)
            {

                Printer(towers, moveCount);
                if (towers[0].Count == 0 && towers[1].Count == 0)
                    break;

                input = userInputs(input);
                if (moveCheck(towers, input))
                {
                    towers[input[1]].Push(towers[input[0]].Pop());
                    moveCount++;
                }
            }
            winMes(stackSize, moveCount);
        }
        static void initialtower(Stack<int>[] towers, int stackSize)
        {
            for (int x = 0; x < towers.Length; x++)
                towers[x] = new Stack<int>();

            for (int x = 0; x < stackSize; x++)
                towers[0].Push(stackSize - x);
        }
        static int[] userInputs(int[] input)
        {
            bool isNum = true;
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

            return input;

        }
        static bool moveCheck(Stack<int>[] towers, int[] input)
        {
            bool move = true;
            if (towers[input[0]].Count <= 0)
                move = false;
            else if (towers[input[1]].Count > 0)
            {
                if (towers[input[0]].Peek() > towers[input[1]].Peek())
                    move = false;
            }

            return move;
        }
        static void Printer(Stack<int>[] towers, int moveCount)
        {
            Console.Clear();
            Console.WriteLine($"\tMove Count: {moveCount}\n");
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta };
            string thing = "|";
            int stackSize = 5;
            int[][] displayTowers = new int[3][];
            Stack<int> buffer = new Stack<int>();

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
        }
        static void winMes(int stackSize, int moveCount)
        {
            int perfectMoveCount = (int)Math.Pow(2, stackSize) - 1;
            Console.WriteLine($"\tCONGRATULATIONS! You finished the game in {moveCount} moves!");
            Console.WriteLine($"\tThe perfect move count should be {perfectMoveCount}. . .");
            Console.WriteLine($"\tYour score is {100 - ((perfectMoveCount - moveCount) / perfectMoveCount)}%");
        }
    }
}
