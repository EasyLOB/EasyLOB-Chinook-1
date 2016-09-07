using Microsoft.Practices.Unity;
using Xunit;

namespace Chinook.UnitTest.xUnit
{
    public class CoreTests : BaseTest
    {
        [Fact]
        [Trait("Category", "Core")] // ???
        public void False()
        {
            // Arrange

            // Act
            bool result = false;

            // Assert
            Assert.Equal(false, result);
        }

        [Fact]
        [Trait("Category", "Core")] // ???
        public void True()
        {
            // Arrange

            // Act
            bool result = true;

            // Assert
            Assert.Equal(true, result);
        }
    }
}