using System;
using System.ComponentModel.DataAnnotations;

namespace RobotApp.Models
{
    public class Grid
    {
        public Grid(string gridDefinition)
        {
            if (string.IsNullOrWhiteSpace(gridDefinition))
            {
                throw new ArgumentNullException(nameof(gridDefinition), $"{nameof(gridDefinition)} cannot be empty. Please provide a valid {nameof(gridDefinition)} e.g. 'GRID 4x3'");
            }

            string[] gridDimensions = gridDefinition.Split()[1].Split("x");
            if (gridDimensions.Length != 2)
            {
                throw new ArgumentException($"'{gridDefinition}' is not a valid {nameof(gridDefinition)}. Please provide a valid {nameof(gridDefinition)} e.g. 'GRID 4x3'", nameof(gridDefinition));
            }

            if (!int.TryParse(gridDimensions[0], out var gridWidth))
            {
                throw new FormatException($"'{gridDimensions[0]}' is not a valid width. Please provide a valid width for the grid e.g. 'GRID 4x3'");
            }

            if (!int.TryParse(gridDimensions[1], out var gridHeight))
            {
                throw new FormatException($"'{gridDimensions[1]}' is not a valid height. Please provide a valid height for the grid e.g. 'GRID 4x3'");
            }

            if (gridWidth < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(gridDefinition), $"'{gridWidth}' is not a valid width. Width must be more than or equal to 1");
            }

            if (gridHeight < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(gridDefinition), $"'{gridHeight}' is not a valid height. Height must be more than or equal to 1");
            }

            Width = gridWidth;
            Height = gridHeight;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
