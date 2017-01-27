using System;

namespace RubicCubeSolverAI
{
    class Program
    {
        static void Main(string[] args)
        {
            var rubicsCube = new RubicsCube();

            //rubicsCube.RotateBackSideAroundYLeftToTop();            
            //rubicsCube.RotateFrontSideAroundYRightToTop();
            //rubicsCube.RotateTopSideAroundZLeftToFront();
            rubicsCube.RotateBottomSideAroundZRightToFront();
            

            Console.WriteLine(rubicsCube);

            Console.ReadLine();
        }
    }
}