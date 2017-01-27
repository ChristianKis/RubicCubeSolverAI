using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RubicCubeSolverAI
{
    public class Solver
    {
        private RubicsCube _cube;
        private readonly List<Rotation> _rotations = new List<Rotation>();

        public IList<string> ActionNames { get;  } = new List<string>();

        public Solver(RubicsCube cube)
        {
            _cube = cube;

            _rotations.Add(new Rotation(cube.RotateBottomSideAroundZRightToFront, cube.RotateBottomSideAroundZLeftToFront));
            _rotations.Add(new Rotation(cube.RotateBottomSideAroundZLeftToFront, cube.RotateBottomSideAroundZRightToFront));

            _rotations.Add(new Rotation(cube.RotateTopSideAroundZRightToFront, cube.RotateTopSideAroundZLeftToFront));
            _rotations.Add(new Rotation(cube.RotateTopSideAroundZLeftToFront, cube.RotateTopSideAroundZRightToFront));

            _rotations.Add(new Rotation(cube.RotateLeftSideAroundXBackToTop, cube.RotateLeftSideAroundXFrontToTop));
            _rotations.Add(new Rotation(cube.RotateLeftSideAroundXFrontToTop, cube.RotateLeftSideAroundXBackToTop));

            _rotations.Add(new Rotation(cube.RotateRightSideAroundXBackToTop, cube.RotateRightSideAroundXFrontToTop));
            _rotations.Add(new Rotation(cube.RotateRightSideAroundXFrontToTop, cube.RotateRightSideAroundXBackToTop));

            _rotations.Add(new Rotation(cube.RotateFrontSideAroundYRightToTop, cube.RotateFrontSideAroundYLeftToTop));
            _rotations.Add(new Rotation(cube.RotateFrontSideAroundYLeftToTop, cube.RotateFrontSideAroundYRightToTop));

            _rotations.Add(new Rotation(cube.RotateBackSideAroundYRightToTop, cube.RotateBackSideAroundYLeftToTop));
            _rotations.Add(new Rotation(cube.RotateBackSideAroundYLeftToTop, cube.RotateBackSideAroundYRightToTop));
        }

        public void Solve()
        {
            var cubeDestability = GetMissmatch();

            if(cubeDestability == 0)
                return;

            var missmatches = new List<int>();
            foreach (var rotation in _rotations)
            {
                rotation.Invoke();
                missmatches.Add(GetMissmatch());
                rotation.Undo();
            }

            var minIndex = missmatches.IndexOf(missmatches.Min());

            _rotations[minIndex].Invoke();
            ActionNames.Add(_rotations[minIndex]._action.GetMethodInfo().Name);

            Solve();
        }

        private int GetMissmatch()
        {
            var sum = 0;
            sum += GetMissmatch(_cube.TopSide); 
            sum += GetMissmatch(_cube.BottomSide);
            sum += GetMissmatch(_cube.LeftSide);
            sum += GetMissmatch(_cube.RightSide);
            sum += GetMissmatch(_cube.FrontSide);
            sum += GetMissmatch(_cube.BackSide); 

            return sum;
        }

        private int GetMissmatch(Color[,] side)
        {
            var sideColor = side[1, 1];
            var sideMissmatch = 0;
            for(var i = 0; i < RubicsCube.CubeSize; ++i)
                for (var j = 0; j < RubicsCube.CubeSize; ++j)
                {
                    if (side[i, j] != sideColor)
                        sideMissmatch++;
                }

            return sideMissmatch;
        }

        private class Rotation
        {
            public readonly Action _action;
            private readonly Action _undoAction;

            public Rotation(Action action, Action undoAction)
            {
                _action = action;
                _undoAction = undoAction;
            }

            public void Invoke()
            {
                _action.Invoke();
            }

            public void Undo()
            {
                _undoAction.Invoke();
            }

        }

    }
}
