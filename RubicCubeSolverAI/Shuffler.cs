using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubicCubeSolverAI
{
    public class Shuffler
    {
        public RubicsCube GetShuffledCube(int steps)
        {
            var cube = new RubicsCube();

            var rotates = new List<Action>
            {
                cube.RotateBackSideAroundYLeftToTop,
                cube.RotateBackSideAroundYRightToTop,
                cube.RotateBottomSideAroundZLeftToFront,
                cube.RotateBottomSideAroundZRightToFront,
                cube.RotateFrontSideAroundYLeftToTop,
                cube.RotateFrontSideAroundYRightToTop,
                cube.RotateLeftSideAroundXBackToTop,
                cube.RotateLeftSideAroundXFrontToTop,
                cube.RotateRightSideAroundXBackToTop,
                cube.RotateRightSideAroundXFrontToTop,
                cube.RotateTopSideAroundZLeftToFront,
                cube.RotateTopSideAroundZRightToFront
            };

            var random = new Random();

            for (var stepCount = 0; stepCount < steps; ++stepCount)
            {
                rotates[random.Next(rotates.Count)].Invoke();
            }

            return cube;
        }
    }
}
