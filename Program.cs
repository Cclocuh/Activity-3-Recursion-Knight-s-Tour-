using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTour
{
    class Program
    {
        static int BoardSize = 8;
        static int attemptedMoves = 0;

        /* xMove[] and yMove[] define next move of knight.
            xMove[] is for next value of x coordinate
            yMove[] is for next value of y coordinate */

        static int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };

        // boardGrid is an 8x8 array that contains -1 for an unvisited square or a move number between a and 63.
        static int[,] boardGird = new int[BoardSize, BoardSize];

        // Driver Code 

        public static void Main()
        {
            solveKT();
            Console.ReadLine();
        }

        static void solveKT()
        {
            /* Initialization of solution matrix. value of -1 means "not visited yet" */
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                    boardGird[x, y] = -1;

                int startX = 0;
                int startY = 4;

                // set starting position for knight 
                boardGird[startX, startY] = 0;
                
                // count the total number of guesses
                attemptedMoves = 0;

                /* explore all tours using solveKTUtil() */
                if (!solveKTUtil(startX, startY, 1))
                {
                    Console.WriteLine("Solution does not exist for {0} {1}", startX, startY);
                }
                else
                {
                    printSolution(boardGird);
                    Console.Out.WriteLine("Total attempted moves {0}", attemptedMoves);
                }
            }

            /* A recursive utility function to solve knight tour problem */
            bool solveKTUtil(int x,  int y, int moveCount) 
            {
                attemptedMoves++;
                if (attemptedMoves % 1000000 == 0)
                    Console.Out.WriteLine("Attempts: {0}", attemptedMoves);

                int k, next_x, next_y;

                // check to see if we have reached a solution. 64 = moveCount 
                if (moveCount == BoardSize * BoardSize)
                    return true;

                /* Try all next moves from the current coordinate x, y */
                for (k = 0; k < 8; k++)
                {
                    next_x = x + xMove[k];
                    next_y = y + yMove[k];
                    if (isSquareSafe(next_y, next_x))
                    {
                        boardGird[next_x, next_y] = moveCount;
                        if (solveKTUtil(next_x, next_y, moveCount + 1))
                            return true;
                        else
                            // backtracking
                            boardGird[next_x, next_y] = -1;
                    }
                }
                return false;
            }

            /*A utility function to check if i, j are valid indexes for N*N chessboard */
            bool isSquareSafe(int x, int y)
            {
                return (x >= 0 && x < BoardSize &&
                        y >= 0 && y < BoardSize &&
                        boardGird[x, y] == -1);
            }

            void printSolution(int[,] solution)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    for (int y = 0; y < BoardSize; y++)
                        Console.Write(solution[x, y] + " ");

                    Console.WriteLine();
                }
            }

            int CountVisitedNeighbors (int x, int y)
            {
                int count = 0;
                for (int i = 0; i < 8; i++)
                {
                    int next_x = x + xMove[i];
                    int next_y = y + yMove[i];
                    if (isSquareSafe(next_x, next_y))
                    {
                        for (int  j = 0; j < 8; j++)
                        {
                            int neighborX = next_x + xMove[j];
                            int neighborY = next_y + yMove[j];
                            if (isSquareSafe(neighborX, neighborY))
                                count++;
                        }
                    }
                }
                return count;

            }
        }
    }
}
