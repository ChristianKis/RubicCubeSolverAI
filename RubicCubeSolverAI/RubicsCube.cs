using System;
using System.Text;
namespace RubicCubeSolverAI
{
    public class RubicsCube
    {
        private const int CubeSize = 3;

        /// <summary>
        /// 0, 0, 0 is the front bottom left cube    
        /// x, y, z in order           
        /// </summary>
        private readonly Cube[,,] _cubes = new Cube[CubeSize, CubeSize, CubeSize];
        public RubicsCube()
        {
            Initialize3X3SolvedCube();
        }

        private void Initialize3X3SolvedCube()
        {
            // Front side bottom line
            _cubes[0, 0, 0] = new Cube(bottom: Color.Yellow, left: Color.Red, front: Color.Blue);
            _cubes[1, 0, 0] = new Cube(bottom: Color.Yellow, front: Color.Blue);
            _cubes[2, 0, 0] = new Cube(bottom: Color.Yellow, front: Color.Blue, right: Color.Orange);

            // Front side middle line
            _cubes[0, 0, 1] = new Cube(left: Color.Red, front: Color.Blue);
            _cubes[1, 0, 1] = new Cube(front: Color.Blue);
            _cubes[2, 0, 1] = new Cube(front: Color.Blue, right: Color.Orange);

            // Front side top line
            _cubes[0, 0, 2] = new Cube(left: Color.Red, front: Color.Blue, top: Color.White);
            _cubes[1, 0, 2] = new Cube(front: Color.Blue, top: Color.White);
            _cubes[2, 0, 2] = new Cube(front: Color.Blue, right: Color.Orange, top: Color.White);

            // Back side bottom line
            _cubes[0, 2, 0] = new Cube(bottom: Color.Yellow, left: Color.Red, back: Color.Green);
            _cubes[1, 2, 0] = new Cube(bottom: Color.Yellow, back: Color.Green);
            _cubes[2, 2, 0] = new Cube(bottom: Color.Yellow, back: Color.Green, right: Color.Orange);

            // Back side middle line
            _cubes[0, 2, 1] = new Cube(left: Color.Red, back: Color.Green);
            _cubes[1, 2, 1] = new Cube(back: Color.Green);
            _cubes[2, 2, 1] = new Cube(back: Color.Green, right: Color.Orange);

            // Back side top line
            _cubes[0, 2, 2] = new Cube(left: Color.Red, back: Color.Green, top: Color.White);
            _cubes[1, 2, 2] = new Cube(back: Color.Green, top: Color.White);
            _cubes[2, 2, 2] = new Cube(back: Color.Green, right: Color.Orange, top: Color.White);

            // Middle slice bottom line
            _cubes[0, 1, 0] = new Cube(bottom: Color.Yellow, left: Color.Red);
            _cubes[1, 1, 0] = new Cube(bottom: Color.Yellow);
            _cubes[2, 1, 0] = new Cube(bottom: Color.Yellow, right: Color.Orange);

            // Middle slice middle line
            _cubes[0, 1, 1] = new Cube(left: Color.Red);
            _cubes[1, 1, 1] = new Cube();
            _cubes[2, 1, 1] = new Cube(right: Color.Orange);

            // Middle slice top line
            _cubes[0, 1, 2] = new Cube(left: Color.Red, top: Color.White);
            _cubes[1, 1, 2] = new Cube(top: Color.White);
            _cubes[2, 1, 2] = new Cube(right: Color.Orange, top: Color.White);
        }

        public void RotateFrontSideAroundYLeftToTop()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[x, 0, z].RotateAroundYLeftToTop();
                }
            }

            var previousCube = _cubes[0, 0, 0];
            _cubes[0, 0, 0] = _cubes[CubeSize - 1, 0, 0];
            _cubes[CubeSize - 1, 0, 0] = _cubes[CubeSize - 1, 0, CubeSize - 1];
            _cubes[CubeSize - 1, 0, CubeSize - 1] = _cubes[0, 0, CubeSize - 1];
            _cubes[0, 0, CubeSize - 1] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[nonCornerIndex, 0, 0];
                _cubes[nonCornerIndex, 0, 0] = _cubes[CubeSize - 1, 0, nonCornerIndex];
                _cubes[CubeSize - 1, 0, nonCornerIndex] = _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1];
                _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1] = _cubes[0, 0, CubeSize - 1 - nonCornerIndex];
                _cubes[0, 0, CubeSize - 1 - nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }
        public void RotateFrontSideAroundYRightToTop()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[x, 0, z].RotateAroundYRightToTop();
                }
            }

            var previousCube = _cubes[0, 0, 0];
            _cubes[0, 0, 0] = _cubes[0, 0, CubeSize - 1];
            _cubes[0, 0, CubeSize - 1] = _cubes[CubeSize - 1, 0, CubeSize - 1];
            _cubes[CubeSize - 1, 0, CubeSize - 1] = _cubes[CubeSize - 1, 0, 0];
            _cubes[CubeSize - 1, 0, 0] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[nonCornerIndex, 0, 0];
                _cubes[nonCornerIndex, 0, 0] = _cubes[0, 0, CubeSize - 1 - nonCornerIndex];
                _cubes[0, 0, CubeSize - 1 - nonCornerIndex] = _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1];
                _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1] = _cubes[CubeSize - 1, 0, nonCornerIndex];
                _cubes[CubeSize - 1, 0, nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }

        public void RotateBackSideAroundYLeftToTop()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[x, CubeSize - 1, z].RotateAroundYLeftToTop();
                }
            }

            var previousCube = _cubes[0, CubeSize - 1, 0];
            _cubes[0, CubeSize - 1, 0] = _cubes[CubeSize - 1, CubeSize - 1, 0];
            _cubes[CubeSize - 1, CubeSize - 1, 0] = _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1];
            _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1] = _cubes[0, CubeSize - 1, CubeSize - 1];
            _cubes[0, CubeSize - 1, CubeSize - 1] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[nonCornerIndex, CubeSize - 1, 0];
                _cubes[nonCornerIndex, CubeSize - 1, 0] = _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex];
                _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex] = _cubes[CubeSize - 1 - nonCornerIndex, CubeSize - 1, CubeSize - 1];
                _cubes[CubeSize - 1 - nonCornerIndex, CubeSize - 1, CubeSize - 1] = _cubes[0, CubeSize - 1, CubeSize - 1 - nonCornerIndex];
                _cubes[0, CubeSize - 1, CubeSize - 1 - nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }
        public void RotateBackSideAroundYRightToTop()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[x, CubeSize - 1, z].RotateAroundYRightToTop();
                }
            }

            var previousCube = _cubes[0, CubeSize - 1, 0];
            _cubes[0, CubeSize - 1, 0] = _cubes[0, CubeSize - 1, CubeSize - 1];
            _cubes[0, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1];
            _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, 0];
            _cubes[CubeSize - 1, CubeSize - 1, 0] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[nonCornerIndex, CubeSize - 1, 0];
                _cubes[nonCornerIndex, CubeSize - 1, 0] = _cubes[0, CubeSize - 1, CubeSize - 1 - nonCornerIndex];
                _cubes[0, CubeSize - 1, CubeSize - 1 - nonCornerIndex] = _cubes[CubeSize - 1 - nonCornerIndex, CubeSize - 1, CubeSize - 1];
                _cubes[CubeSize - 1 - nonCornerIndex, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex];
                _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }

        public void RotateRightSideAroundXFrontToTop()
        {
            for (var y = 0; y < CubeSize; y++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[CubeSize - 1, y, z].RotateAroundXFrontToTop();
                }
            }

            var previousCube = _cubes[CubeSize - 1, 0, 0];
            _cubes[CubeSize - 1, 0, 0] = _cubes[CubeSize - 1, CubeSize - 1, 0];
            _cubes[CubeSize - 1, CubeSize - 1, 0] = _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1];
            _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, 0, CubeSize - 1];
            _cubes[CubeSize - 1, 0, CubeSize - 1] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[CubeSize - 1, nonCornerIndex, 0];
                _cubes[CubeSize - 1, nonCornerIndex, 0] = _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex];
                _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex] = _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1];
                _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1] = _cubes[CubeSize - 1, 0, CubeSize - 1 - nonCornerIndex];
                _cubes[CubeSize - 1, 0, CubeSize - 1 - nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }
        public void RotateRightSideAroundXBackToTop()
        {
            for (var y = 0; y < CubeSize; y++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[CubeSize - 1, y, z].RotateAroundXBackToTop();
                }
            }

            var previousCube = _cubes[CubeSize - 1, 0, 0];
            _cubes[CubeSize - 1, 0, 0] = _cubes[CubeSize - 1, 0, CubeSize - 1];
            _cubes[CubeSize - 1, 0, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1];
            _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, 0];
            _cubes[CubeSize - 1, CubeSize - 1, 0] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[CubeSize - 1, nonCornerIndex, 0];
                _cubes[CubeSize - 1, nonCornerIndex, 0] = _cubes[CubeSize - 1, 0, CubeSize - 1 - nonCornerIndex];
                _cubes[CubeSize - 1, 0, CubeSize - 1 - nonCornerIndex] = _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1];
                _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex];
                _cubes[CubeSize - 1, CubeSize - 1, nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }

        public void RotateLeftSideAroundXFrontToTop()
        {
            for (var y = 0; y < CubeSize; y++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[0, y, z].RotateAroundXFrontToTop();
                }
            }

            var previousCube = _cubes[0, 0, 0];
            _cubes[0, 0, 0] = _cubes[0, CubeSize - 1, 0];
            _cubes[0, CubeSize - 1, 0] = _cubes[0, CubeSize - 1, CubeSize - 1];
            _cubes[0, CubeSize - 1, CubeSize - 1] = _cubes[0, 0, CubeSize - 1];
            _cubes[0, 0, CubeSize - 1] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[0, nonCornerIndex, 0];
                _cubes[0, nonCornerIndex, 0] = _cubes[0, CubeSize - 1, nonCornerIndex];
                _cubes[0, CubeSize - 1, nonCornerIndex] = _cubes[0, CubeSize - 1 - nonCornerIndex, CubeSize - 1];
                _cubes[0, CubeSize - 1 - nonCornerIndex, CubeSize - 1] = _cubes[0, 0, CubeSize - 1 - nonCornerIndex];
                _cubes[0, 0, CubeSize - 1 - nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }
        public void RotateLeftSideAroundXBackToTop()
        {
            for (var y = 0; y < CubeSize; y++)
            {
                for (var z = 0; z < CubeSize; z++)
                {
                    _cubes[0, y, z].RotateAroundXBackToTop();
                }
            }

            var previousCube = _cubes[0, 0, 0];
            _cubes[0, 0, 0] = _cubes[0, 0, CubeSize - 1];
            _cubes[0, 0, CubeSize - 1] = _cubes[0, CubeSize - 1, CubeSize - 1];
            _cubes[0, CubeSize - 1, CubeSize - 1] = _cubes[0, CubeSize - 1, 0];
            _cubes[0, CubeSize - 1, 0] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[0, nonCornerIndex, 0];
                _cubes[0, nonCornerIndex, 0] = _cubes[0, 0, CubeSize - 1 - nonCornerIndex];
                _cubes[0, 0, CubeSize - 1 - nonCornerIndex] = _cubes[0, CubeSize - 1 - nonCornerIndex, CubeSize - 1];
                _cubes[0, CubeSize - 1 - nonCornerIndex, CubeSize - 1] = _cubes[0, CubeSize - 1, nonCornerIndex];
                _cubes[0, CubeSize - 1, nonCornerIndex] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }

        public void RotateTopSideAroundZLeftToFront()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var y = 0; y < CubeSize; y++)
                {
                    _cubes[x, y, CubeSize - 1].RotateAroundZLeftToFront();
                }
            }

            var previousCube = _cubes[0, 0, CubeSize - 1];
            _cubes[0, 0, CubeSize - 1] = _cubes[0, CubeSize - 1, CubeSize - 1];
            _cubes[0, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1];
            _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, 0, CubeSize - 1];
            _cubes[CubeSize - 1, 0, CubeSize - 1] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[0, nonCornerIndex, CubeSize - 1];
                _cubes[0, nonCornerIndex, CubeSize - 1] = _cubes[nonCornerIndex, CubeSize - 1, CubeSize - 1];
                _cubes[nonCornerIndex, CubeSize - 1, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1];
                _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1] = _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1];
                _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }
        public void RotateTopSideAroundZRightToFront()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var y = 0; y < CubeSize; y++)
                {
                    _cubes[x, y, CubeSize - 1].RotateAroundZRightToFront();
                }
            }

            var previousCube = _cubes[0, 0, CubeSize - 1];
            _cubes[0, 0, CubeSize - 1] = _cubes[CubeSize - 1, 0, CubeSize - 1];
            _cubes[CubeSize - 1, 0, CubeSize - 1] = _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1];
            _cubes[CubeSize - 1, CubeSize - 1, CubeSize - 1] = _cubes[0, CubeSize - 1, CubeSize - 1];
            _cubes[0, CubeSize - 1, CubeSize - 1] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[0, nonCornerIndex, CubeSize - 1];
                _cubes[0, nonCornerIndex, CubeSize - 1] = _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1];
                _cubes[CubeSize - 1 - nonCornerIndex, 0, CubeSize - 1] =
                    _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1];
                _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, CubeSize - 1] =
                    _cubes[nonCornerIndex, CubeSize - 1, CubeSize - 1];
                _cubes[nonCornerIndex, CubeSize - 1, CubeSize - 1] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }

        public void RotateBottomSideAroundZLeftToFront()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var y = 0; y < CubeSize; y++)
                {
                    _cubes[x, y, 0].RotateAroundZLeftToFront();
                }
            }

            var previousCube = _cubes[0, 0, 0];
            _cubes[0, 0, 0] = _cubes[0, CubeSize - 1, 0];
            _cubes[0, CubeSize - 1, 0] = _cubes[CubeSize - 1, CubeSize - 1, 0];
            _cubes[CubeSize - 1, CubeSize - 1, 0] = _cubes[CubeSize - 1, 0, 0];
            _cubes[CubeSize - 1, 0, 0] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[0, nonCornerIndex, 0];
                _cubes[0, nonCornerIndex, 0] = _cubes[nonCornerIndex, CubeSize - 1, 0];
                _cubes[nonCornerIndex, CubeSize - 1, 0] = _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, 0];
                _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, 0] = _cubes[CubeSize - 1 - nonCornerIndex, 0, 0];
                _cubes[CubeSize - 1 - nonCornerIndex, 0, 0] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }
        public void RotateBottomSideAroundZRightToFront()
        {
            for (var x = 0; x < CubeSize; x++)
            {
                for (var y = 0; y < CubeSize; y++)
                {
                    _cubes[x, y, 0].RotateAroundZRightToFront();
                }
            }

            var previousCube = _cubes[0, 0, 0];
            _cubes[0, 0, 0] = _cubes[CubeSize - 1, 0, 0];
            _cubes[CubeSize - 1, 0, 0] = _cubes[CubeSize - 1, CubeSize - 1, 0];
            _cubes[CubeSize - 1, CubeSize - 1, 0] = _cubes[0, CubeSize - 1, 0];
            _cubes[0, CubeSize - 1, 0] = previousCube;

            Action<int> repositionNonCornerCubes = delegate (int nonCornerIndex)
            {
                previousCube = _cubes[0, nonCornerIndex, 0];
                _cubes[0, nonCornerIndex, 0] = _cubes[CubeSize - 1 - nonCornerIndex, 0, 0];
                _cubes[CubeSize - 1 - nonCornerIndex, 0, 0] = _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, 0];
                _cubes[CubeSize - 1, CubeSize - 1 - nonCornerIndex, 0] = _cubes[nonCornerIndex, CubeSize - 1, 0];
                _cubes[nonCornerIndex, CubeSize - 1, 0] = previousCube;
            };

            for (var nonCornerIndex = 1; nonCornerIndex < CubeSize - 1; nonCornerIndex++)
            {
                repositionNonCornerCubes(nonCornerIndex);
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("Top side" + Environment.NewLine);
            builder.Append("-------------------------------" + Environment.NewLine);
            for (var y = CubeSize - 1; y >= 0; y--)
            {
                for (var x = 0; x < CubeSize; x++)
                {
                    builder.Append($"{_cubes[x, y, CubeSize - 1].Top} ");
                }

                builder.Append(Environment.NewLine);
            }

            builder.Append(Environment.NewLine);

            builder.Append("Front side" + Environment.NewLine);
            builder.Append("-------------------------------" + Environment.NewLine);
            for (var z = CubeSize - 1; z >= 0; z--)
            {
                for (var x = 0; x < CubeSize; x++)
                {
                    builder.Append($"{_cubes[x, 0, z].Front} ");
                }

                builder.Append(Environment.NewLine);
            }

            builder.Append(Environment.NewLine);

            builder.Append("Bottom side" + Environment.NewLine);
            builder.Append("-------------------------------" + Environment.NewLine);
            for (var y = 0; y < CubeSize; y++)
            {
                for (var x = 0; x < CubeSize; x++)
                {
                    builder.Append($"{_cubes[x, y, 0].Bottom} ");
                }

                builder.Append(Environment.NewLine);
            }

            builder.Append(Environment.NewLine);

            builder.Append("Back side" + Environment.NewLine);
            builder.Append("-------------------------------" + Environment.NewLine);
            for (var z = CubeSize - 1; z >= 0; z--)
            {
                for (var x = 0; x < CubeSize; x++)
                {
                    builder.Append($"{_cubes[x, CubeSize - 1, z].Back} ");
                }

                builder.Append(Environment.NewLine);
            }

            builder.Append(Environment.NewLine);

            builder.Append("Left side" + Environment.NewLine);
            builder.Append("-------------------------------" + Environment.NewLine);
            for (var z = CubeSize - 1; z >= 0; z--)
            {
                for (var y = CubeSize - 1; y >= 0; y--)
                {
                    builder.Append($"{_cubes[0, y, z].Left} ");
                }

                builder.Append(Environment.NewLine);
            }

            builder.Append(Environment.NewLine);

            builder.Append("Right side" + Environment.NewLine);
            builder.Append("-------------------------------" + Environment.NewLine);
            for (var z = CubeSize - 1; z >= 0; z--)
            {
                for (var y = 0; y < CubeSize; y++)
                {
                    builder.Append($"{_cubes[CubeSize - 1, y, z].Right} ");
                }

                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}