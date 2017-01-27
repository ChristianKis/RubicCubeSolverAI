using System;

namespace RubicCubeSolverAI
{
    class Program
    {
        static void Main(string[] args)
        {           

            var rubicsCube = new Shuffler().GetShuffledCube(2);

            Console.WriteLine("Shuffled Cube:");
            Console.WriteLine(rubicsCube);
            Console.ReadLine();
            Console.Clear();

            var solver = new Solver(rubicsCube);

            solver.Solve();

            Console.WriteLine("Solved With:");
            foreach (var action in solver.ActionNames)
            {
                Console.WriteLine(action);
            }

            Console.ReadLine();
        }
    }
}