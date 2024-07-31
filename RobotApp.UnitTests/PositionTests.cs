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
    }
}
