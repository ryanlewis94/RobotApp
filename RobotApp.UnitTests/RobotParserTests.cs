using RobotApp.Enums;
using RobotApp.Models;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class RobotParserTests
    {
        [Test]
        public void RobotParserSuccessfullyParsed()
        {
            string[] instructions =
            {
                "GRID 4x3",
                "",
                "OBSTACLE 1 2",
                "",
                "1 1 E",
                "RF",
                "1 0 W"
            };

            RobotParser robotParser = new(instructions);

            Assert.That(robotParser.RobotGrid.Width, Is.EqualTo(4));
            Assert.That(robotParser.RobotGrid.Height, Is.EqualTo(3));

            Assert.That(robotParser.RobotObstacles.First().X, Is.EqualTo(1));
            Assert.That(robotParser.RobotObstacles.First().Y, Is.EqualTo(2));

            Assert.That(robotParser.RobotJourneys.First().StartingPosition.X, Is.EqualTo(1));
            Assert.That(robotParser.RobotJourneys.First().StartingPosition.Y, Is.EqualTo(1));
            Assert.That(robotParser.RobotJourneys.First().StartingPosition.Direction, Is.EqualTo(DirectionEnum.E));

            Assert.That(robotParser.RobotJourneys.First().Commands.First(), Is.EqualTo(CommandEnum.R));
            Assert.That(robotParser.RobotJourneys.First().Commands.Last(), Is.EqualTo(CommandEnum.F));

            Assert.That(robotParser.RobotJourneys.First().ExpectedPosition.X, Is.EqualTo(1));
            Assert.That(robotParser.RobotJourneys.First().ExpectedPosition.Y, Is.EqualTo(0));
            Assert.That(robotParser.RobotJourneys.First().ExpectedPosition.Direction, Is.EqualTo(DirectionEnum.W));
        }

        [Test]
        public void RobotParser_FailedObstacleParse()
        {
            string[] instructions =
            {
                "GRID 4x3",
                "",
                "1 1 E",
                "RFR",
                "1 0 W",
                "",
                "1 1 E",
                "RFRF",
                "1 1 E",
                "",
                "OBSTACLE 1 2"
            };

            Exception ex = Assert.Throws<FormatException>(() => new RobotParser(instructions));
            Assert.That(ex.Message, Is.EqualTo("Please make sure all obstacles are declared before the start of any journeys"));
        }
    }
}
