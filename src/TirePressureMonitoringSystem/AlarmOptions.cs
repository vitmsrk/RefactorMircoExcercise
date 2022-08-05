namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class AlarmOptions
    {
        public double LowPressureThreshold { get; set; }
        public double HighPressureThreshold { get; set; }

        public static AlarmOptions Default =>
            new AlarmOptions
            {
                LowPressureThreshold = 17,
                HighPressureThreshold = 21
            };
    }
}