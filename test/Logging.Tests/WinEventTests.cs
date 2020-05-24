using System;
using Xunit;

namespace Logging.Tests
{
    public class WinEventTests
    {
        #region Verify if code can validate existence of Event Log
        [Fact]
        public void DoesEventSourceExist()
        {
            // Arrange
            WinEvent logging = new WinEvent("Application", LogLevel.Information);
            // Act
            // Assert
            Assert.True(logging.TestEventSource());
        }
        #endregion

        #region Verify if creation of event works and verify if it exists in Event Log
        /*[Theory]
        [InlineData("First test", "Information")]
        [InlineData("Second test","Warning")]
        [InlineData("Third test","Error")]
        [InlineData("Fourth test", "")]
        public void WritingEventToEventLog(string message, string level)
        {
            //Arrange
            string guid = Guid.NewGuid().ToString();
            message = $"{message} - {guid}";
            //Act
            EventLogging.WinEvents.LogEvent(message, level);
            //Assert
            Assert.Equal( message, EventLogging.WinEvents.SearchEvent(message) );
        }*/
        #endregion
    }
}