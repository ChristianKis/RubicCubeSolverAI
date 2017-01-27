using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RubicCubeSolverAI
{
    [TestClass]
    public class CubeTest
    {
        [TestMethod]
        public void RotateAroundYLeftToTop()
        {
            var cube = new Cube(Color.White, Color.Yellow, Color.Blue, Color.Green, Color.Red, Color.Orange);

            cube.RotateAroundYLeftToTop();

            Assert.AreEqual(cube.Top, Color.Red);
            Assert.AreEqual(cube.Right, Color.White);
            Assert.AreEqual(cube.Bottom, Color.Orange);
            Assert.AreEqual(cube.Left, Color.Yellow);
            Assert.AreEqual(cube.Front, Color.Blue);
            Assert.AreEqual(cube.Back, Color.Green);
        }

        [TestMethod]
        public void RefTypeCheck()
        {
            var firstCube = new Cube();
            var secondCube = new Cube();

            var list = new List<Cube> { firstCube, secondCube };

            var test = list[0];
            list[0] = secondCube;
            //Assert.AreEqual(firstCube.GetHashCode(), secondCube.GetHashCode());
            Assert.AreEqual(test.GetHashCode(), list[0].GetHashCode());



        }
    }
}
