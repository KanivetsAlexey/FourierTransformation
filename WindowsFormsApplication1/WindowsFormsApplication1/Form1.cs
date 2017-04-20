using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.Classes;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Bitmap btmp;
        private Bitmap btmp2;
        Graphics graph;
        Graphics graph2;
        Pen blackPen = new Pen(Color.Black, 1);
        Pen greenPen = new Pen(Color.Green, 1);
        Pen redPen = new Pen(Color.Red, 1);
        Pen bluePen = new Pen(Color.Blue, 1);
        Pen greyPen = new Pen(Color.LightGray, 1);
        //float width;
        //float height;
        Data data1, data2, data3;
        float scalex = 0.5F;
        float scaley = 0.5f;
        float scale = 0.5f;


        public Form1()
        {
            InitializeComponent();

            data1 = new Data(100,72,0.85);
            data2 = new Data(60, 220, 0.85);
            data3 = new Data(400, 380, 0.85);
        }        

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Init()
        {
            float width = pictureBox1.Width;
            float height = pictureBox1.Height;

            btmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Refresh();
            graph = Graphics.FromImage(btmp);
            graph.TranslateTransform(10, height / 2);
            graph.ScaleTransform(1, -1);
            pictureBox1.BackgroundImage = btmp;
            scalex = trackBar1.Value / 100f;
            scaley = trackBar2.Value / 100f;
            scale = scaley;


            graph.ScaleTransform(scalex, scaley);
            DrawCoords(graph, width, height);
            DrawAxis(graph, width, height);

            if (radioButton1.Checked)
            {
                DrawSins(data1.Signal, redPen);               
                label5.Text = data1.Amplitude.ToString();
                label6.Text = data1.Frequency.ToString();
                label9.Text = Math.Round(data1.CalcAmplitude, 3).ToString();

                if (checkBox1.Checked)
                    DrawSins(data1.FiltredSignal, new Pen(Color.LawnGreen, 1));
            }

            if (radioButton2.Checked)
            {
                DrawSins(data2.Signal, greenPen);
                label5.Text = data2.Amplitude.ToString();
                label6.Text = data2.Frequency.ToString();
                label9.Text = Math.Round(data2.CalcAmplitude, 3).ToString();

                if (checkBox1.Checked)
                    DrawSins(data2.FiltredSignal, new Pen(Color.LawnGreen, 1));
            }
            if (radioButton3.Checked)
            {
                DrawSins(data3.Signal, bluePen);
                label5.Text = data3.Amplitude.ToString();
                label6.Text = data3.Frequency.ToString();
                label9.Text = Math.Round(data3.CalcAmplitude, 3).ToString();
                
                if (checkBox1.Checked)
                    DrawSins(data3.FiltredSignal, new Pen(Color.LawnGreen, 1));
                
            }
        }

        private void Init2()
        {
            float width = pictureBox2.Width;
            float height = pictureBox1.Height;

            btmp2 = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Refresh();
            graph2 = Graphics.FromImage(btmp2);
            graph2.TranslateTransform(10, height / 2);
            graph2.ScaleTransform(1, -1);
            pictureBox2.BackgroundImage = btmp2;
            scalex = trackBar1.Value / 100f;
            scaley = trackBar2.Value / 100f;
            scale = scaley;


            graph2.ScaleTransform(scalex, scaley);
            DrawCoords(graph2, width, height);
            DrawAxis(graph2, width, height);

            if (radioButton1.Checked)
            {
                DrawLineSignal(data1, bluePen);              
            }

            if (radioButton2.Checked)
            {
                DrawLineSignal(data2, bluePen);
            }
            if (radioButton3.Checked)
            {
                DrawLineSignal(data2, bluePen);

            }
        }

        private void Visualize_button_Click(object sender, EventArgs e)
        {
            Init();
            Init2();
        }

        private void DrawAxis(Graphics graph, float width, float height)
        {
            graph.DrawLine(blackPen,0,0,width/scale,0);
            graph.DrawLine(blackPen,0,-height/(2* scale),0,height/ (2 * scale));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {           
            Init();
            //Init2();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Init();
            //Init2();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            data1.Alpha = (double)(numericUpDown1.Value) / 100;
            data2.Alpha = (double)(numericUpDown1.Value) / 100;
            data3.Alpha = (double)(numericUpDown1.Value) / 100;           
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            Init();
            if (radioButton1.Checked)
            {
                 DrawSins(data1.FiltredSignal, new Pen(Color.LawnGreen, 1));
            }

            if (radioButton2.Checked)
            {
                DrawSins(data2.FiltredSignal, new Pen(Color.LawnGreen, 1));
            }
            if (radioButton3.Checked)
            {
                DrawSins(data3.FiltredSignal, new Pen(Color.LawnGreen, 1));
            }
        }

        private void DrawCoords(Graphics graph, float width, float height)
        {
            for (float i = 0; i < width/ scale; i += 10)
                graph.DrawLine(greyPen, i, -height/ scale, i, height/ scale);

            for (float i = -height/ (2 * scale); i <= height/ (2 * scale); i += 10)
                graph.DrawLine(greyPen, 0, i, width/ scale , i);
        }

        private void DrawSins(List<double> list, Pen pen)
        {
            for (int i = 1; i < list.Count - 1; i++)
                graph.DrawLine(pen, (i - 1), (float)list[i - 1], i, (float)list[i] );
        }

        private void DrawLineSignal(Data data, Pen pen)
        {
            List<double> list = data.ListCalcAmplitude;         
            int x = data.Frequency;
            for (int i = 1; i < x; i++)
                graph2.DrawLine(pen, x, (float)list[i - 1], x, (float)list[i]);
        }
    }
}
