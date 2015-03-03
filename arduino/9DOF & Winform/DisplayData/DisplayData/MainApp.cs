using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DisplayData
{
	public partial class MainApp : Form
	{
		int i = 0, i2 = 0;
		string RxString;
		double Rpoint, Ppoint, Hpoint;
		double Xpoint, Ypoint, Zpoint;
		double adc0, adc1;//, adc2, adc3;
		//double adc4, adc5, adc6, adc7;

		//int potValue;
		//bool init = false;
		//int samplerate = 0;

		//bool testcomp = false;

		System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
	550, 20, 300, 300);

		public MainApp()
		{
			InitializeComponent();
			dostuff();
		}

		void dostuff()
		{
			chart1.Series.Add("Roll");
			chart1.Series[0].ChartType = SeriesChartType.FastLine;
			chart1.Series[0].ChartArea = "ChartArea1";
			chart1.Series[0].BorderWidth = 2;
			chart1.Series.Add("Pitch");
			chart1.Series[1].ChartType = SeriesChartType.FastLine;
			chart1.Series[1].ChartArea = "ChartArea1";
			chart1.Series[1].BorderWidth = 2;
			chart1.Series.Add("Heading");
			chart1.Series[2].ChartType = SeriesChartType.FastLine;
			chart1.Series[2].ChartArea = "ChartArea1";
			chart1.Series[2].BorderWidth = 2;
			chart1.Series[2].Enabled = false;

			chart2.Series.Add("X");
			chart2.Series[0].ChartType = SeriesChartType.FastLine;
			chart2.Series[0].ChartArea = "ChartArea1";
			chart2.Series[0].BorderWidth = 2;
			chart2.Series.Add("Y");
			chart2.Series[1].ChartType = SeriesChartType.FastLine;
			chart2.Series[1].ChartArea = "ChartArea1";
			chart2.Series[1].BorderWidth = 2;
			chart2.Series.Add("Z");
			chart2.Series[2].ChartType = SeriesChartType.FastLine;
			chart2.Series[2].ChartArea = "ChartArea1";
			chart2.Series[2].BorderWidth = 2;
			//chart2.Series[2].Enabled = false;

			//not really needed but gives an initialization value for reference.

			chart1.Series[0].Points.Add(0);
			chart1.Series[1].Points.Add(0);
			chart1.Series[2].Points.Add(0);

			chart2.Series[0].Points.Add(0);
			chart2.Series[1].Points.Add(0);
			chart2.Series[2].Points.Add(0);
			//if (!init)
			//{
			//	for (int n = 0; n <= 20; n++)
			//	{
			//		chart1.Series[0].Points.Add(0);
			//		chart1.Series[1].Points.Add(0);
			//		chart1.Series[2].Points.Add(0);
			//	}
			//	init = true;
			//}


		}

		private void serialInitBtn_Click(object sender, EventArgs e)
		{
			serialINIT();
		}

		private void serialINIT()
		{
			ArduinoPort.Open();
			if (ArduinoPort.IsOpen)
			{
				TxBtn.Enabled = true;
				serialInitBtn.Enabled = false;
				SerialKillerBtn.Enabled = true;
			}
		}

		private void TxBtn_Click(object sender, EventArgs e)
		{
			if (!ArduinoPort.IsOpen) return;
			string input = Input.Text;
			input = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(input));

			ArduinoPort.Write(input);

			ArduinoPort.Write("\r\n");
			Input.Text = "";
		}

		private void ArduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			RxString = ArduinoPort.ReadLine();
			//this.Invoke(new EventHandler(OutputText));
			if (RxString[0] == '~' && RxString[RxString.Length - 2] == '~')
			{
				char[] buffering = new char[32];
				int m = 0;
				for (int l = 1; l <= (RxString.Length - 3); l++)
				{
					buffering[m] = RxString[l];
					m++;
				}
				string buffered = new string(buffering);
				string[] RPH = buffered.Split(':');
				//foreach (string axis in RPH)
				//{
					
				//}
				Rpoint = double.Parse(RPH[1].Trim());
				Ppoint = double.Parse(RPH[2].Trim());
				Hpoint = (180+double.Parse(RPH[3].Trim()));
				//if (Hpoint > 360)
				//	Hpoint = Hpoint-360;

				//Hpoint = 360 - Hpoint;
				this.Invoke(new EventHandler(RPHChartMaker));

			}
			else if (RxString[0] == '&' && RxString[RxString.Length - 2] == '&')
			{
				char[] buffering = new char[32];
				int m = 0;
				for (int l = 1; l <= (RxString.Length - 3); l++)
				{
					buffering[m] = RxString[l];
					m++;
				}
				string buffered = new string(buffering);
				string[] XYZ = buffered.Split(':');
				Xpoint = double.Parse(XYZ[1].Trim());
				Ypoint = double.Parse(XYZ[2].Trim());
				Zpoint = double.Parse(XYZ[3].Trim());
				this.Invoke(new EventHandler(XYZChartMaker));

			}
			else if (RxString[0] == '*' && RxString[RxString.Length - 2] == '*')
			{
				char[] buffering = new char[32];
				int m = 0;
				for (int l = 1; l <= (RxString.Length - 3); l++)
				{
					buffering[m] = RxString[l];
					m++;
				}
				string buffered = new string(buffering);
				string[] ADS = buffered.Split(':');
				adc0 = Math.Abs(double.Parse(ADS[1].Trim()));
				adc1 = Math.Abs(double.Parse(ADS[2].Trim()));
				//adc2 = double.Parse(XYZ[3].Trim());
				this.Invoke(new EventHandler(PressureSensors));

			}
			else
			{
				this.Invoke(new EventHandler(OutputText));
			}
		}

		private void RPHChartMaker(object sender, EventArgs e)
		{
			chart1.Series[0].Points.AddY(Rpoint);
			chart1.ChartAreas[0].AxisY.Maximum = 180;
			chart1.ChartAreas[0].AxisY.Minimum = -180;
			//			chart1.ChartAreas[0].RecalculateAxesScale();
			chart1.Series[1].Points.AddY(Ppoint); 
			chart1.Series[2].Points.AddY(Hpoint);


			//chart3.ChartAreas[0].AxisY.Maximum = 180;
			//chart3.ChartAreas[0].AxisY.Minimum = -180;
			if (chart1.Series[0].Points.Count > 40)
				chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points.Count - 40;

			chart1.Refresh();
			i++;
			
			//progressBar1.Value = (int)Rpoint;
			//trackBar1.Value = (int)Ppoint;

			//System.Drawing.Graphics graphics = this.CreateGraphics();
			//graphics.DrawArc(System.Drawing.Pens.Black, rectangle, (float)Hpoint, 20);
			//graphics.Clear(SystemColors.Control);

			progressBar3.Value = (int)Hpoint;
		}

		private void XYZChartMaker(object sender, EventArgs e)
		{
			chart2.Series[0].Points.AddY(Xpoint);
			chart2.ChartAreas[0].AxisY.Maximum = 20;
			chart2.ChartAreas[0].AxisY.Minimum = -20;
			//chart2.ChartAreas[0].RecalculateAxesScale();
			chart2.Series[1].Points.AddY(Ypoint);
			chart2.Series[2].Points.AddY(Zpoint);


			if (chart2.Series[0].Points.Count > 40)
				chart2.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points.Count - 40;

			chart2.Refresh();
			i2++;

		}

		private void PressureSensors(object sender, EventArgs e)
		{

			progressBar1.Value = (int)adc0;
			progressBar2.Value = (int)adc1;

		}

		private void OutputText(object sender, EventArgs e)
		{
			//if (!testcomp)
			//{
			//	char[] buffering = new char[32];
			//	int m = 0;
			//	for (int l = 1; l <= (RxString.Length - 2); l++)
			//	{
			//		buffering[m] = RxString[l];
			//		m++;
			//	}
			//	string buffered = new string(buffering);
			//	if (int.Parse(buffered) > 1000 && int.Parse(buffered) <= 2000)
			//	{
			//		samplerate++;
			//	}
			//	else if (int.Parse(buffered) >= 2000 && int.Parse(buffered) <= 2100)
			//	{
			//		label1.Text = samplerate.ToString();
			//		testcomp = true;
			//	}
			//}
			ReceiverOutput.AppendText(RxString);

		}

		private void SerialKillerBtn_Click(object sender, EventArgs e)
		{
			SerialKiller();
		}

		private void SerialKiller()
		{
			ArduinoPort.Close();
			if (!ArduinoPort.IsOpen)
			{
				TxBtn.Enabled = false;
				serialInitBtn.Enabled = true;
				SerialKillerBtn.Enabled = false;
			}
		}

		private void clearBtn_Click(object sender, EventArgs e)
		{
			ReceiverOutput.Text = "";
		}
	}
}
