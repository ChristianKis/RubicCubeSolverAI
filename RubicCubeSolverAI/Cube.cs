namespace RubicCubeSolverAI
{
    /// <summary>
    /// These colors are represented from the users point
    /// Left means clockwise first tile from the front tile
    /// Right means counter-clockwise first tile from the front tile
    /// </summary>
    public class Cube
    {
        /// <summary>
        /// X and Y axis, top side
        /// </summary>
        public Color Top { get; private set; }
        /// <summary>
        /// X and Y axis, bottom side
        /// </summary>
        public Color Bottom { get; private set; }
        /// <summary>
        /// X and Z axis, front side
        /// </summary>
        public Color Front { get; private set; }
        /// <summary>
        /// X and Z axis, back side
        /// </summary>
        public Color Back { get; private set; }
        /// <summary>
        /// Y and Z axis, left side
        /// </summary>
        public Color Left { get; private set; }
        /// <summary>
        /// Y and Z axis, right side
        /// </summary>
        public Color Right { get; private set; }

        public Cube(Color top = Color.Blank,
            Color bottom = Color.Blank,
            Color front = Color.Blank,
            Color back = Color.Blank,
            Color left = Color.Blank,
            Color right = Color.Blank)
        {
            Top = top;
            Bottom = bottom;
            Front = front;
            Back = back;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Left and Right stays the same        
        /// </summary>
        public void RotateAroundXFrontToTop()
        {
            var previousColor = Top;

            Top = Front;
            Front = Bottom;
            Bottom = Back;
            Back = previousColor;
        }

        /// <summary>
        /// Left and Right stays the same        
        /// </summary>
        public void RotateAroundXBackToTop()
        {
            var previousColor = Top;

            Top = Back;
            Back = Bottom;
            Bottom = Front;
            Front = previousColor;
        }

        /// <summary>
        /// Front and Back stays the same        
        /// </summary>
        public void RotateAroundYLeftToTop()
        {
            var previousColor = Top;

            Top = Left;
            Left = Bottom;
            Bottom = Right;
            Right = previousColor;
        }

        /// <summary>
        /// Front and Back stays the same        
        /// </summary>
        public void RotateAroundYRightToTop()
        {
            var previousColor = Top;

            Top = Right;
            Right = Bottom;
            Bottom = Left;
            Left = previousColor;
        }

        /// <summary>
        /// Top and Bottom stays the same        
        /// </summary>
        public void RotateAroundZLeftToFront()
        {
            var previousColor = Front;

            Front = Left;
            Left = Back;
            Back = Right;
            Right = previousColor;
        }

        /// <summary>
        /// Top and Bottom stays the same        
        /// </summary>
        public void RotateAroundZRightToFront()
        {
            var previousColor = Front;

            Front = Right;
            Right = Back;
            Back = Left;
            Left = previousColor;
        }
    }
}