using RobotApp.Models;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class RobotJourneyTests
    {
        [Test]
        public void RobotJourney_Success()
        {
            string[] instructions =
            {
                "GRID 4x3",
                "",
                "OBSTACLE 1 2",
                "",
                "1 1 E",
                "RFR",
                "1 0 W"
            };

            RobotParser robotParser = new(instructions);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                foreach (RobotJourney journey in robotParser.RobotJourneys)
                {
                    journey.BeginJourney(robotParser.RobotGrid, robotParser.RobotObstacles);
                }

                Assert.That(sw.ToString(), Is.EqualTo($"SUCCESS 1 0 W{Environment.NewLine}"));
            }
        }

        [Test]
        public void RobotJourney_Failure()
        {
            string[] instructions =
            {
                "GRID 4x3",
                "",
                "1 1 E",
                "RFRF",
                "1 1 E"
            };

            RobotParser robotParser = new(instructions);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                foreach (RobotJourney journey in robotParser.RobotJourneys)
                {
                    journey.BeginJourney(robotParser.RobotGrid, robotParser.RobotObstacles);
                }

                Assert.That(sw.ToString(), Is.EqualTo($"FAILURE 0 0 W{Environment.NewLine}"));
            }
        }

        [Test]
        public void RobotJourney_OutOfBounds()
        {
            string[] instructions =
            {
                "GRID 4x3",
                "",
                "1 1 E",
                "RFF",
                "1 1 E"
            };

            RobotParser robotParser = new(instructions);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                foreach (RobotJourney journey in robotParser.RobotJourneys)
                {
                    journey.BeginJourney(robotParser.RobotGrid, robotParser.RobotObstacles);
                }

                Assert.That(sw.ToString(), Is.EqualTo($"OUT OF BOUNDS{Environment.NewLine}"));
            }
        }

        [Test]
        public void RobotJourney_Crashed()
        {
            string[] instructions =
            {
                "GRID 20x20",
                "",
                "OBSTACLE 1 2",
                "OBSTACLE 1 3",
                "OBSTACLE 2 4",
                "",
                "0 3 W",
                "LLFFFLFLFL",
                "2 4 S"
            };

            RobotParser robotParser = new(instructions);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                foreach (RobotJourney journey in robotParser.RobotJourneys)
                {
                    journey.BeginJourney(robotParser.RobotGrid, robotParser.RobotObstacles);
                }

                Assert.That(sw.ToString(), Is.EqualTo($"CRASHED 1 3{Environment.NewLine}"));
            }
        }

        [Test]
        public void RobotJourney_IncorrectJourney()
        {
            List<string> journey = new()
            {
                "1 1 E",
                "RFR",
                "1 0 W",
                "test"
            };

            Exception ex = Assert.Throws<ArgumentException>(() => new RobotJourney(journey, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("journey must include a valid starting position e.g. '1 1 E', commands e.g. 'LFRF', and expected ending position e.g. '1 1 E' (Parameter 'journey')"));
        }

        [Test]
        public void RobotJourney_NoCommands()
        {
            List<string> journey = new()
            {
                "1 1 E",
                "1 1 E",
                "1 0 W"
            };

            Exception ex = Assert.Throws<ArgumentNullException>(() => new RobotJourney(journey, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("journey must include a valid set of commands e.g. 'LFRF' (Parameter 'journey')"));
        }

        [Test]
        public void RobotJourney_StartingPositionXIncorrect()
        {
            string[] journey =
            {
                "-1 1 E",
                "RFRF",
                "1 1 E"
            };

            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new RobotJourney(journey, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("Starting position is outside the boundaries of the grid. Please make sure StartingPosition can be plotted inside a 4x3 grid (Parameter 'StartingPosition')"));
        }

        [Test]
        public void RobotJourney_StartingPositionYIncorrect()
        {
            string[] journey =
            {
                "1 4 E",
                "RFRF",
                "1 1 E"
            };

            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new RobotJourney(journey, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("Starting position is outside the boundaries of the grid. Please make sure StartingPosition can be plotted inside a 4x3 grid (Parameter 'StartingPosition')"));
        }

        [Test]
        public void RobotJourney_ExpectedPositionXIncorrect()
        {
            string[] journey =
            {
                "1 1 E",
                "RFRF",
                "6 1 E"
            };

            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new RobotJourney(journey, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("Expected position is outside the boundaries of the grid. Please make sure ExpectedPosition can be plotted inside a 4x3 grid (Parameter 'ExpectedPosition')"));
        }

        [Test]
        public void RobotJourney_ExpectedPositionYIncorrect()
        {
            string[] journey =
            {
                "1 1 E",
                "RFRF",
                "1 10 E"
            };

            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new RobotJourney(journey, new Grid("GRID 4x3")));
            Assert.That(ex.Message, Is.EqualTo("Expected position is outside the boundaries of the grid. Please make sure ExpectedPosition can be plotted inside a 4x3 grid (Parameter 'ExpectedPosition')"));
        }
    }
}
