using PeriodicWaveform.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PeriodicWaveform.Model
{
    internal class PeriodicWave : ChangeNotifier
    {
        private int _amplitude;
        private int _frequency;
        private int _sampling;

        
        public int Amplitude
        {
            get { return _amplitude; }
            set { _amplitude = value; OnPropertyChanged(nameof(Amplitude)); }
        }
        public int Frequency
        {
            get { return _frequency; }
            set { _frequency = value; OnPropertyChanged(nameof(Frequency)); }
        }
        public int SamplingFrequency
        {
            get { return _sampling; }
            set { _sampling = value; OnPropertyChanged(nameof(_sampling)); }
        }
        
    }
}
