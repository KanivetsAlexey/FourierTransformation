using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WindowsFormsApplication1
{
    class Calc
    {
        double ret = 0;
        double Amplitude = Math.Pow(33.51015608785815,1/0.98);
        double[] x = new double[1000];
        StreamReader sr;

        public void Read(string path)
        {
            sr = new StreamReader(path);
            
            //TODO: end reading from file
            //for(int i =0;i<sr.)
        }

        public double Compare()
        {
            //TODO: end calculations
            double t = 0.005;
            //double Xcos = 
            //double Xsin =

            return ret;
        }
    }
}
