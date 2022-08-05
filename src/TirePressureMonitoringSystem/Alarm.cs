using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        private readonly ISensor _sensor;
        private readonly AlarmOptions _options;

        private bool _alarmOn = false;

        public Alarm() : this(new Sensor())
        {
        }

        public Alarm(ISensor sensor, Action<AlarmOptions> configureOptions = null)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));

            _options = AlarmOptions.Default;
            configureOptions?.Invoke(_options);
        }

        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();

            if (psiPressureValue < _options.LowPressureThreshold
                || _options.HighPressureThreshold < psiPressureValue)
            {
                _alarmOn = true;
            }
        }

        public bool AlarmOn
        {
            get { return _alarmOn; }
        }
    }
}
