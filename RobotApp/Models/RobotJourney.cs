using System;
using System.Collections.Generic;
using System.Linq;
using RobotApp.Enums;

namespace RobotApp.Models
{
    public class RobotJourney
    {
        public RobotJourney(IEnumerable<string> journey, Grid grid)
        {
            if (journey.Count() != 3)
            {
                throw new ArgumentException($"{nameof(journey)} must include a valid starting position e.g. '1 1 E', commands e.g. 'LFRF', and expected ending position e.g. '1 1 E'", nameof(journey));
            }

            var commands = journey.SingleOrDefault(j => !j.Contains(" "));
            if (string.IsNullOrWhiteSpace(commands))
            {
                throw new ArgumentNullException(nameof(journey), $"{nameof(journey)} must include a valid set of commands e.g. 'LFRF'");
            }

            Commands = commands.Select(c =>
            {
                if (Enum.TryParse(c.ToString(), out CommandEnum command))
                {
                    return command;
                }

                throw new FormatException($"'{c}' is not a valid command. Please include a valid set of commands e.g. 'LFRF'");
            });

            StartingPosition = new Position(journey.First());
            
            ExpectedPosition = new Position(journey.Last());

            if (grid.IsOutOfBounds(StartingPosition))
            {
                throw new ArgumentOutOfRangeException(nameof(StartingPosition), $"Starting position is outside the boundaries of the grid. Please make sure {nameof(StartingPosition)} can be plotted inside a {grid.Width}x{grid.Height} grid");
            }

            if (grid.IsOutOfBounds(ExpectedPosition))
            {
                throw new ArgumentOutOfRangeException(nameof(ExpectedPosition), $"Expected position is outside the boundaries of the grid. Please make sure {nameof(ExpectedPosition)} can be plotted inside a {grid.Width}x{grid.Height} grid");
            }
        }

        public Position StartingPosition { get; set; } 

        public IEnumerable<CommandEnum> Commands { get; set; }

        public Position ExpectedPosition { get; set; }


        public void BeginJourney(Grid grid, IEnumerable<Obstacle> obstacles)
        {
            Position currentPosition = StartingPosition;
            bool isOutOfBoundsOrCrashed = false;

            foreach (var command in Commands)
            {
                currentPosition.ExecuteCommand(command);

                if (grid.IsOutOfBounds(currentPosition))
                {
                    FinishJourney("OUT OF BOUNDS");
                    isOutOfBoundsOrCrashed = true;
                    break;
                }

                string hasCrashedMessage = currentPosition.HasCrashed(obstacles);
                if (!string.IsNullOrWhiteSpace(hasCrashedMessage))
                {
                    FinishJourney(hasCrashedMessage);
                    isOutOfBoundsOrCrashed = true;
                    break;
                }
            }

            if (!isOutOfBoundsOrCrashed)
            {
                FinishJourney($"{Outcome(currentPosition)} {currentPosition.X} {currentPosition.Y} {currentPosition.Direction}");
            }
        }

        private void FinishJourney(string finishMessage)
        {
            Console.WriteLine(finishMessage);
        }

        private string Outcome(Position position)
        {
            return position.X == ExpectedPosition.X &&
                   position.Y == ExpectedPosition.Y &&
                   position.Direction == ExpectedPosition.Direction
                   ? "SUCCESS" : "FAILURE";
        }
    }
}
