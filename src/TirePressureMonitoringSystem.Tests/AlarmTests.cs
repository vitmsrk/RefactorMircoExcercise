using System;
using Moq;
using NUnit.Framework;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Tests
{
    [TestFixture]
    public class AlarmTests
    {
        [Test]
        public void Constructor_WhenCalledWithNullSensor_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Alarm(null));
        }

        [Test]
        public void Check_DoesNotThrowAnyExceptions()
        {
            var alarm = new Alarm();
            Assert.DoesNotThrow(() => alarm.Check());
        }

        [TestCase(1.0, 2.0, 1.1)]
        [TestCase(1.0, 2.0, 1.0)]
        [TestCase(1.0, 2.0, 2.0)]
        public void Check_WhenPressureDoesNotExceedThresholds_AlarmIsOff(
            double lowPressureThreshold,
            double highPressureThreshold,
            double pressureValue)
        {
            // Arrange
            Mock<ISensor> sensorMock = SetupSensorMock(pressureValue);

            var alarm = new Alarm(sensorMock.Object, options =>
            {
                options.LowPressureThreshold = lowPressureThreshold;
                options.HighPressureThreshold = highPressureThreshold;
            });
            
            // Act
            alarm.Check();
            
            // Assert
            Assert.False(alarm.AlarmOn);
        }

        [TestCase(1.0, 2.0, 2.1)]
        [TestCase(1.0, 2.0, 0.9)]
        [TestCase(1.123456, 1.123457, 1.123458)]
        public void Check_WhenPressureExceedsThresholds_AlarmIsOn(
            double lowPressureThreshold,
            double highPressureThreshold,
            double pressureValue)
        {
            // Arrange
            Mock<ISensor> sensorMock = SetupSensorMock(pressureValue);

            var alarm = new Alarm(sensorMock.Object, options =>
            {
                options.LowPressureThreshold = lowPressureThreshold;
                options.HighPressureThreshold = highPressureThreshold;
            });
            
            // Act
            alarm.Check();
            
            // Assert
            Assert.IsTrue(alarm.AlarmOn);
        }
        
        [Test]
        public void Check_CallsSensorExactlyOnce()
        {
            // Arrange
            Mock<ISensor> sensorMock = SetupSensorMock(1.0);
            var alarm = new Alarm(sensorMock.Object);

            // Act
            alarm.Check();
            
            // Assert
            sensorMock.Verify(x => x.PopNextPressurePsiValue(), Times.Once);
        }

        private Mock<ISensor> SetupSensorMock(double pressureValue)
        {
            var sensorMock = new Mock<ISensor>();
            sensorMock.Setup(x => x.PopNextPressurePsiValue())
                .Returns(pressureValue)
                .Verifiable();

            return sensorMock;
        }
    }
}