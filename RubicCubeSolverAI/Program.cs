using System;

namespace RubicCubeSolverAI
{
    class Program
    {
        static void Main(string[] args)
        {           

            var rubicsCube = new Shuffler().GetShuffledCube(1);

            Console.WriteLine(rubicsCube);

            Console.ReadLine();
        }
    }
}