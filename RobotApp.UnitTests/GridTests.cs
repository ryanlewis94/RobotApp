using RobotApp.Models;
using RobotApp.Services.FileService;

namespace RobotApp.UnitTests
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void Grid_SuccessfullyInitialised()
        {
            Grid grid = new("GRID 20x10");

            Assert.That(grid.Width, Is.EqualTo(20));
            Assert.That(grid.Height, Is.EqualTo(10));
        }

        [Test]
        public void Grid_NullOrEmpty()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => new Grid(string.Empty));
            Assert.That(ex.Message, Is.EqualTo("gridDefinition cannot be empty. Please provide a valid gridDefinition e.g. 'GRID 4x3' (Parameter 'gridDefinition')"));
        }

        [Test]
        public void Grid_IncorrectFormat()
        {
            Exception ex = Assert.Throws<ArgumentException>(() => new Grid("GRID 43"));
            Assert.That(ex.Message, Is.EqualTo("'GRID 43' is not a valid gridDefinition. Please provide a valid gridDefinition e.g. 'GRID 4x3' (Parameter 'gridDefinition')"));
        }

        [Test]
        public void Grid_IncorrectWidthFormat()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Grid("GRID Wx3"));
            Assert.That(ex.Message, Is.EqualTo("'W' is not a valid width. Please provide a valid width for the grid e.g. 'GRID 4x3'"));
        }

        [Test]
        public void Grid_IncorrectHeightFormat()
        {
            Exception ex = Assert.Throws<FormatException>(() => new Grid("GRID 4xH"));
            Assert.That(ex.Message, Is.EqualTo("'H' is not a valid height. Please provide a valid height for the grid e.g. 'GRID 4x3'"));
        }

        [Test]
        public void Grid_IncorrectWidth()
        {
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Grid("GRID -1x3"));
            Assert.That(ex.Message, Is.EqualTo("'-1' is not a valid width. Width must be more than or equal to 1 (Parameter 'gridDefinition')"));
        }

        [Test]
        public void Grid_IncorrectHeight()
        {
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Grid("GRID 4x0"));
            Assert.That(ex.Message, Is.EqualTo("'0' is not a valid height. Height must be more than or equal to 1 (Parameter 'gridDefinition')"));
        }
    }
}
