using RobotApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace RobotApp.Extensions
{
    internal static class PositionExtensions
    {
        internal static bool IsOutOfBounds(this Position position, Grid grid)
        {
            return position.X < 0 || position.Y < 0 || grid.Height - 1 < position.X || grid.Width - 1 < position.Y;
        }

        internal static string HasCrashed(this Position position, IEnumerable<Obstacle> obstacles)
        {
            var obstacle = obstacles.FirstOrDefault(o => o.X == position.X && o.Y == position.Y);
            return obstacle != null ? $"CRASHED {obstacle.X} {obstacle.Y}" : string.Empty;
        }
    }
}
