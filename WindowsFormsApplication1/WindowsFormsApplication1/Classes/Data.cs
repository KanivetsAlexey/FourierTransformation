using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    
    class Data
    {
        public const double K = 0.98;
        public const int N = 1000;
        public const int T = 1;
        public List<double> Time { get; set; }
        public List<double> Signal { get; set; }
        public int Frequency { get; set; }      
        public int Amplitude { get; set; }
        public double CalcAmplitude { get; set; }
        public List<double> FiltredSignal { get; set; }
        public double Alpha { get; set; }

        public List<double> ListCalcAmplitude { get; set; }

        public Data()
        {
            Time = new List<double>();
            Signal = new List<double>();
            FiltredSignal = new List<double>();
            ListCalcAmplitude = new List<double>();
        }

        public Data(int freq, int amplitude, double alpha):this()
        {
            Frequency = freq;
            Amplitude = amplitude;
            Alpha = alpha;
            GenerateSignal();
        }

        private void GenerateSignal() 
        {
            double y_i = 0;
            double t = (double)T / N;
            for(double i = 0; i < 1; i += t/10)
            {
                Time.Add(i);
                y_i = Math.Pow(Amplitude, K) * Math.Sin(2 * Math.PI * Frequency * i); //+ Math.Pow(2 * Amplitude, K) * Math.Sin(2 * Math.PI * 2 * Frequency * i);
                Signal.Add(y_i);
            }

            //CalcSum();
            Filter(); 
            CalculateAmplitude();
        }

        public void Filter()
        {
            double yi = 0;
            FiltredSignal.Clear();
            FiltredSignal.Add(0);

            for (int i = 1; i < N; i++)
            {
                yi = (1 - Alpha) * Signal[i] - Alpha * FiltredSignal[i - 1];
                FiltredSignal.Add(yi);
            }
            
        }

        private void CalculateAmplitude()
        {          
            double[] ReX = new double[N / 2 + 1];
            double[] ImX = new double[N / 2 + 1];
            for(int j = 0; j < N / 2; j++)
            {
                ReX[j] = 0;
                ImX[j] = 0;
                for (int i = 0; i < N; i++)
                {
                    double tmp = Math.PI * 2 * Time[i] * j / N;
                    ReX[j] += Signal[i] * Math.Cos(tmp);
                    ImX[j] -= Signal[i] * Math.Sin(tmp);
                }
                ListCalcAmplitude.Add(Math.Sqrt(Math.Pow(ReX[j], 2) + Math.Pow(ImX[j], 2)));
            }
            CalcAmplitude = ListCalcAmplitude[Frequency];           
        }
    }
}
