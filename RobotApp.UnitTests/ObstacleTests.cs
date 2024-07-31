using RobotApp.Models;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class ObstacleTests
    {
        [Test]
        public void Obstacle_SuccessfullyInitialised()
        {
            Obstacle obstacle = new("OBSTACLE 1 2", new Grid("GRID 4x3"));

            Assert.That(obstacle.X, Is.EqualTo(1));
            Assert.That(obstacle.Y, Is.EqualTo(2));
        }

        [Test]
        public void Obstacle_NullOrEmpty()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new Obstacle(string.Empty, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("Obstacle coordinates cannot be empty. Please provide a valid obstacle e.g. 'OBSTACLE 1 2' (Parameter 'coordinates')"));
        }

        [Test]
        public void Obstacle_IncorrectFormat()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => new Obstacle("OBSTACLE 12", new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("'OBSTACLE 12' is not a valid obstacle definition. Please provide a valid obstacle definition e.g. 'OBSTACLE 1 2' (Parameter 'coordinates')"));
        }

        [Test]
        public void Obstacle_IncorrectXFormat()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Obstacle("OBSTACLE R 2", new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("'R' is not a valid x coordinate. Please provide valid x coordinates for Obstacle e.g. 'OBSTACLE 1 2'"));
        }

        [Test]
        public void Obstacle_IncorrectYFormat()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Obstacle("OBSTACLE 1 L", new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("'L' is not a valid y coordinate. Please provide valid y coordinates for Obstacle e.g. 'OBSTACLE 1 2'"));
        }

        [Test]
        public void Obstacle_IncorrectX()
        {
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Obstacle("OBSTACLE 5 2", new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("'5' is outside the width of the grid. Please make sure coordinates can be plotted inside a 4x3 grid (Parameter 'coordinates')"));
        }

        [Test]
        public void Obstacle_IncorrectY()
        {
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Obstacle("OBSTACLE 1 -1", new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("'-1' is outside the height of the grid. Please make sure coordinates can be plotted inside a 4x3 grid (Parameter 'coordinates')"));
        }
    }
}
