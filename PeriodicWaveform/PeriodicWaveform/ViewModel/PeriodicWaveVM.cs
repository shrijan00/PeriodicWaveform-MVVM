using PeriodicWaveform.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace PeriodicWaveform.ViewModel
{
    internal class PeriodicWaveVM : ChangeNotifier
    {
        private PeriodicWave _periodicWave;

        public PeriodicWave TargetWaveform
        {
            get { return _periodicWave; }
            set { _periodicWave = value; OnPropertyChanged(nameof(TargetWaveform)); }
        }

        private string _wavename;
        int currentSecond = 0;
    
        

        public string WaveName
        {
            get { return _wavename; }
            set
            {
                _wavename = value;
                OnPropertyChanged(nameof(WaveName));
            }
        }
        PointCollection wavePointCollection = new PointCollection();
        private Queue<Point> SignalQueue { get; set; } = new Queue<Point>();

        private PointCollection _signal;
        public PointCollection SignalPoints
        {
            get { return _signal; }
            set { _signal = value; OnPropertyChanged(nameof(SignalPoints)); }
        }

        private bool canexecuteradiomethod(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void executemethod(object parameter)
        {
            WaveName = (string)parameter;

        }

        private bool canExecuteMethod(object parameter)
        {
            if (string.IsNullOrEmpty(_wavename) || _periodicWave.Amplitude == 0 || _periodicWave.Frequency == 0 || _periodicWave.SamplingFrequency == 0)
            {
                return false;
            }
            return true;
        }


        // Function with Timer
        public void TimerGenerateWave(object parameter)
        {
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
            double x = currentSecond;
            double y = 2 * Math.PI * _periodicWave.Frequency * currentSecond / _periodicWave.SamplingFrequency;


            // Check Radio button
            if (WaveName == "Sine")
            {
                SignalQueue.Enqueue(new Point(x, _periodicWave.Amplitude * Math.Sin(y)));
            }
            else
            {
                SignalQueue.Enqueue(new Point(x, _periodicWave.Amplitude * Math.Cos(y)));
            }
            currentSecond++;
            if (SignalQueue.Count > 20)
            {
                SignalPoints = new PointCollection();
                while (SignalQueue.Count > 0)
                {
                    wavePointCollection.Add(SignalQueue.Dequeue());
                    SignalPoints = wavePointCollection;
                }
                //wavePointCollection.Clear();
            }
            





        }

        // Function without Timer
        public void GenerateWave(object parameter)
        {


            if (WaveName == "Sine")
            {
                for (int i = 0; i < 500; i++)
                {
                    wavePointCollection.Add(new Point(i, _periodicWave.Amplitude * Math.Sin(2 * Math.PI * _periodicWave.Frequency * i / _periodicWave.SamplingFrequency)));
                }
            }
            else
            {
                for (int i = 0; i < 500; i++)
                {
                    wavePointCollection.Add(new Point(i, _periodicWave.Amplitude * Math.Cos(2 * Math.PI * _periodicWave.Frequency * i / _periodicWave.SamplingFrequency)));
                }
            }
            SignalPoints = wavePointCollection;

        }

        // Define the button Commands
        public ICommand radio => new RelayCommand(executemethod, canexecuteradiomethod);
        public ICommand start => new RelayCommand(TimerGenerateWave, canExecuteMethod);


        public PeriodicWaveVM()
        {

            TargetWaveform = new PeriodicWave();
            //Signal = new PointCollection();
            //Signal = wavePointCollection;
        }
    }
}