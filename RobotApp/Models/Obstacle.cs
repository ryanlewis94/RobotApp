using System;

namespace RobotApp.Models
{
    public class Obstacle
    {
        public Obstacle(string coordinates, Grid grid)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
            {
                throw new ArgumentNullException(nameof(coordinates), $"Obstacle {nameof(coordinates)} cannot be empty. Please provide a valid obstacle e.g. 'OBSTACLE 1 2'");
            }

            string[] coordinatesArray = coordinates.Split();
            if (coordinatesArray.Length != 3)
            {
                throw new ArgumentException($"'{coordinates}' is not a valid obstacle definition. Please provide a valid obstacle definition e.g. 'OBSTACLE 1 2'", nameof(coordinates));
            }

            if (!int.TryParse(coordinatesArray[1], out var x))
            {
                throw new FormatException($"'{coordinatesArray[1]}' is not a valid x coordinate. Please provide valid x {nameof(coordinates)} for Obstacle e.g. 'OBSTACLE 1 2'");
            }

            if (!int.TryParse(coordinatesArray[2], out var y))
            {
                throw new FormatException($"'{coordinatesArray[2]}' is not a valid y coordinate. Please provide valid y {nameof(coordinates)} for Obstacle e.g. 'OBSTACLE 1 2'");
            }

            if (x > grid.Width || x < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinates), $"'{x}' is outside the width of the grid. Please make sure {nameof(coordinates)} can be plotted inside a {grid.Width}x{grid.Height} grid");
            }

            if (y > grid.Height || y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(coordinates), $"'{y}' is outside the height of the grid. Please make sure {nameof(coordinates)} can be plotted inside a {grid.Width}x{grid.Height} grid");
            }

            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
