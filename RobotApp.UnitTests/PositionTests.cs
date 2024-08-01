using RobotApp.Enums;
using RobotApp.Models;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Position_SuccessfullyInitialised()
        {
            Position position = new("3 2 E");

            Assert.That(position.X, Is.EqualTo(3));
            Assert.That(position.Y, Is.EqualTo(2));
            Assert.That(position.Direction, Is.EqualTo(DirectionEnum.E));
        }

        [Test]
        public void Position_NullOrEmpty()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new Position(string.Empty));
            Assert.That(ex.Message, Is.EqualTo("coordinates cannot be empty. Please provide a valid Position e.g. '1 2 E' (Parameter 'coordinates')"));
        }

        [Test]
        public void Position_IncorrectFormat()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => new Position("1 1E"));
            Assert.That(ex.Message, Is.EqualTo("'1 1E' is not valid Position coordinates. Please provide valid coordinates for Position e.g. '1 2 E' (Parameter 'coordinates')"));
        }

        [Test]
        public void Position_IncorrectX()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Position("Q 1 E"));
            Assert.That(ex.Message, Is.EqualTo("'Q' is not a valid x coordinate. Please provide valid x coordinates for Position e.g. '1 2 E'"));
        }

        [Test]
        public void Position_IncorrectY()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Position("1 T W"));
            Assert.That(ex.Message, Is.EqualTo("'T' is not a valid y coordinate. Please provide valid y coordinates for Position e.g. '1 2 E'"));
        }

        [Test]
        public void Position_IncorrectDirection()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Position("1 1 B"));
            Assert.That(ex.Message, Is.EqualTo("'B' is not a valid direction. Please provide a valid direction for Position e.g. '1 2 E'"));
        }

        [Test]
        public void ExecuteCommand_RotateLeft()
        {
            Position position = new("1 1 N");
            CommandEnum leftCommand = CommandEnum.L;

            position.ExecuteCommand(leftCommand);

            Assert.That(position.X, Is.EqualTo(new Position("1 1 W").X));
            Assert.That(position.Y, Is.EqualTo(new Position("1 1 W").Y));
            Assert.That(position.Direction, Is.EqualTo(new Position("1 1 W").Direction));
        }

        [Test]
        public void ExecuteCommand_RotateRight()
        {
            Position position = new("0 0 W");
            CommandEnum rightCommand = CommandEnum.R;

            position.ExecuteCommand(rightCommand);

            Assert.That(position.X, Is.EqualTo(new Position("0 0 N").X));
            Assert.That(position.Y, Is.EqualTo(new Position("0 0 N").Y));
            Assert.That(position.Direction, Is.EqualTo(new Position("0 0 N").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveNorth()
        {
            Position position = new("0 0 N");
            CommandEnum forwardCommand = CommandEnum.F;

            position.ExecuteCommand(forwardCommand);

            Assert.That(position.X, Is.EqualTo(new Position("0 1 N").X));
            Assert.That(position.Y, Is.EqualTo(new Position("0 1 N").Y));
            Assert.That(position.Direction, Is.EqualTo(new Position("0 1 N").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveEast()
        {
            Position position = new("0 0 E");
            CommandEnum forwardCommand = CommandEnum.F;

            position.ExecuteCommand(forwardCommand);

            Assert.That(position.X, Is.EqualTo(new Position("1 0 E").X));
            Assert.That(position.Y, Is.EqualTo(new Position("1 0 E").Y));
            Assert.That(position.Direction, Is.EqualTo(new Position("1 0 E").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveSouth()
        {
            Position position = new("1 1 S");
            CommandEnum forwardCommand = CommandEnum.F;

            position.ExecuteCommand(forwardCommand);

            Assert.That(position.X, Is.EqualTo(new Position("1 0 S").X));
            Assert.That(position.Y, Is.EqualTo(new Position("1 0 S").Y));
            Assert.That(position.Direction, Is.EqualTo(new Position("1 0 S").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveWest()
        {
            Position position = new("1 1 W");
            CommandEnum forwardCommand = CommandEnum.F;

            position.ExecuteCommand(forwardCommand);

            Assert.That(position.X, Is.EqualTo(new Position("0 1 W").X));
            Assert.That(position.Y, Is.EqualTo(new Position("0 1 W").Y));
            Assert.That(position.Direction, Is.EqualTo(new Position("0 1 W").Direction));
        }

        [Test]
        public void Position_HasNotCrashed()
        {
            Position position = new("3 2 E");
            IEnumerable<Obstacle> obstacles = new List<Obstacle>
            {
                new Obstacle("OBSTACLE 1 2", new Grid("GRID 4x3")),
                new Obstacle("OBSTACLE 1 3", new Grid("GRID 4x3"))
            };

            string hasCrashedMessage = position.HasCrashed(obstacles);

            Assert.That(hasCrashedMessage, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Position_HasCrashed()
        {
            Position position = new("3 2 E");
            IEnumerable<Obstacle> obstacles = new List<Obstacle>
            {
                new ("OBSTACLE 1 2", new Grid("GRID 4x3")),
                new ("OBSTACLE 3 2", new Grid("GRID 4x3"))
            };

            string hasCrashedMessage = position.HasCrashed(obstacles);

            Assert.That(hasCrashedMessage, Is.EqualTo("CRASHED 3 2"));
        }
    }
}
