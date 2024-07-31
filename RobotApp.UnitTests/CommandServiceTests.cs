using RobotApp.Enums;
using RobotApp.Models;
using RobotApp.Services.CommandService;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class CommandServiceTests
    {
        [Test]
        public void ExecuteCommand_RotateLeft()
        {
            Position position = new ("1 1 N");
            CommandEnum leftCommand = CommandEnum.L;

            CommandService commandService = new ();
            Position newPosition = commandService.ExecuteCommand(position, leftCommand);

            Assert.That(newPosition.X, Is.EqualTo(new Position("1 1 W").X));
            Assert.That(newPosition.Y, Is.EqualTo(new Position("1 1 W").Y));
            Assert.That(newPosition.Direction, Is.EqualTo(new Position("1 1 W").Direction));
        }

        [Test]
        public void ExecuteCommand_RotateRight()
        {
            Position position = new("0 0 W");
            CommandEnum rightCommand = CommandEnum.R;

            CommandService commandService = new();
            Position newPosition = commandService.ExecuteCommand(position, rightCommand);

            Assert.That(newPosition.X, Is.EqualTo(new Position("0 0 N").X));
            Assert.That(newPosition.Y, Is.EqualTo(new Position("0 0 N").Y));
            Assert.That(newPosition.Direction, Is.EqualTo(new Position("0 0 N").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveNorth()
        {
            Position position = new("0 0 N");
            CommandEnum forwardCommand = CommandEnum.F;

            CommandService commandService = new();
            Position newPosition = commandService.ExecuteCommand(position, forwardCommand);

            Assert.That(newPosition.X, Is.EqualTo(new Position("0 1 N").X));
            Assert.That(newPosition.Y, Is.EqualTo(new Position("0 1 N").Y));
            Assert.That(newPosition.Direction, Is.EqualTo(new Position("0 1 N").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveEast()
        {
            Position position = new("0 0 E");
            CommandEnum forwardCommand = CommandEnum.F;

            CommandService commandService = new();
            Position newPosition = commandService.ExecuteCommand(position, forwardCommand);

            Assert.That(newPosition.X, Is.EqualTo(new Position("1 0 E").X));
            Assert.That(newPosition.Y, Is.EqualTo(new Position("1 0 E").Y));
            Assert.That(newPosition.Direction, Is.EqualTo(new Position("1 0 E").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveSouth()
        {
            Position position = new("1 1 S");
            CommandEnum forwardCommand = CommandEnum.F;

            CommandService commandService = new();
            Position newPosition = commandService.ExecuteCommand(position, forwardCommand);

            Assert.That(newPosition.X, Is.EqualTo(new Position("1 0 S").X));
            Assert.That(newPosition.Y, Is.EqualTo(new Position("1 0 S").Y));
            Assert.That(newPosition.Direction, Is.EqualTo(new Position("1 0 S").Direction));
        }

        [Test]
        public void ExecuteCommand_MoveWest()
        {
            Position position = new("1 1 W");
            CommandEnum forwardCommand = CommandEnum.F;

            CommandService commandService = new();
            Position newPosition = commandService.ExecuteCommand(position, forwardCommand);

            Assert.That(newPosition.X, Is.EqualTo(new Position("0 1 W").X));
            Assert.That(newPosition.Y, Is.EqualTo(new Position("0 1 W").Y));
            Assert.That(newPosition.Direction, Is.EqualTo(new Position("0 1 W").Direction));
        }
    }
}
