using System;
using RobotApp.Enums;

namespace RobotApp.Models
{
    public class Position
    {
        public Position(string coordinates)
        {
            if (string.IsNullOrWhiteSpace(coordinates))
            {
                throw new ArgumentNullException(nameof(coordinates), $"{nameof(coordinates)} cannot be empty. Please provide a valid Position e.g. '1 2 E'");
            }

            string[] coordinatesArray = coordinates.Split();

            if (coordinatesArray.Length != 3)
            {
                throw new ArgumentException($"'{coordinates}' is not valid Position {nameof(coordinates)}. Please provide valid {nameof(coordinates)} for Position e.g. '1 2 E'", nameof(coordinates));
            }

            if (!int.TryParse(coordinatesArray[0], out var x))
            {
                throw new FormatException($"'{coordinatesArray[0]}' is not a valid x coordinate. Please provide valid x {nameof(coordinates)} for Position e.g. '1 2 E'");
            }

            if (!int.TryParse(coordinatesArray[1], out var y))
            {
                throw new FormatException($"'{coordinatesArray[1]}' is not a valid y coordinate. Please provide valid y {nameof(coordinates)} for Position e.g. '1 2 E'");
            }

            if (!Enum.TryParse(coordinatesArray[2], out DirectionEnum direction))
            {
                throw new FormatException($"'{coordinatesArray[2]}' is not a valid direction. Please provide a valid direction for Position e.g. '1 2 E'");
            }

            X = x;
            Y = y;
            Direction = direction;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public DirectionEnum Direction { get; set; }
    }
}
