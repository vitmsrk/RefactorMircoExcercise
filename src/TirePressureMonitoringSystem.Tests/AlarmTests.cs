using NUnit.Framework;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Tests
{
    [TestFixture]
    public class AlarmTests
    {
        [Test]
        public void Check_DoesNotThrowAnyExceptions()
        {
            var alarm = new Alarm();
            Assert.DoesNotThrow(() => alarm.Check());
        }
    }
}