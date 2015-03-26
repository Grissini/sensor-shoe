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
using System.Runtime.InteropServices;

/* Notes: this build is still extremely buggy. Almost no error catching is 
 * instituted yet. If you try to run this and it throws an error just stop it
 * and start it again a couple of times. it should work. if it doesnt after a few
 * then would be the time to ask me about it.
 * */


namespace DisplayData
{
	public partial class MainApp : Form
	{
		//counter variables
		int ii = 0, ii2 = 0;

		//memory space for the recieved string
		string RxString;
		
		//Roll, pitch, heading values
		double Rpoint, Ppoint, Hpoint;
		
		//X,Y,Z accel values
		double Xpoint, Ypoint, Zpoint;

		//analog-to-digital converter values
		double adc0=0, adc1=0, adc2=0, adc3=0;
		double adc4=0, adc5=0, adc6=0, adc7=0;

	//	System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
	//550, 20, 300, 300);

		public MainApp()
		{
			InitializeComponent();
			InitializeCharts();
		}

		void InitializeCharts()
		{
			//sets up the chart for the orientation data.
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
			chart1.ChartAreas[0].AxisY.Maximum = 180;
			chart1.ChartAreas[0].AxisY.Minimum = -180;
			
			//sets up the chart for the acceleration data.
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
			chart2.ChartAreas[0].AxisY.Maximum = 20;
			chart2.ChartAreas[0].AxisY.Minimum = -20;

			
			//not really needed but gives an initialization value for reference.
			//relevant for calculations and keeps there from being a null reference exception.

			chart1.Series[0].Points.Add(0);
			chart1.Series[1].Points.Add(0);
			chart1.Series[2].Points.Add(0);

			chart2.Series[0].Points.Add(0);
			chart2.Series[1].Points.Add(0);
			chart2.Series[2].Points.Add(0);		
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
			//this.Invoke(new EventHandler(OutputText)); //for debug will display every line recieved from the arduino

			//following case structure is used to determine what data is being recieved based on wrappers coming from the arduino
			//the wrappers are as follows "~" for orientation, "&" for acceleration, "*" for adc data
			//using wrappers allows the program to decide what to do with data recieved.


			if (RxString[0] == '~' && RxString[RxString.Length - 2] == '~')
			{
				/*
				essentially buffering the data from the serial read into character array
				making it a string stripped of the wrapper, then breaking the string into its 
				constituents which are known from the arduino side and passed into variables as useable data.
				*/

				char[] buffering = new char[64];
				int m = 0;

				//strips the wrapper off.
				for (int l = 1; l <= (RxString.Length - 3); l++)
				{
					buffering[m] = RxString[l];
					m++;
				}
				//creates a string from the buffered input.
				string buffered = new string(buffering);
				//splits on : which is the divider used in the arduino code.
				string[] RPH = buffered.Split(':');
				
				//TODO make this tryparse so that its more stable.
				Rpoint = double.Parse(RPH[1].Trim());
				Ppoint = double.Parse(RPH[2].Trim());
				Hpoint = (180+double.Parse(RPH[3].Trim()));

				//since this is technically another thread. we have to invoke the methods this way instead of normal calls.
				this.Invoke(new EventHandler(RPHChartMaker));

			}
			else if (RxString[0] == '&' && RxString[RxString.Length - 2] == '&')
			{
				char[] buffering = new char[64];
				int m = 0;
				for (int l = 1; l <= (RxString.Length - 3); l++)
				{
					buffering[m] = RxString[l];
					m++;
				}
				string buffered = new string(buffering);
				string[] XYZ = buffered.Split(':');

				//TODO make this tryparse so that its more stable.
				Xpoint = double.Parse(XYZ[1].Trim());
				Ypoint = double.Parse(XYZ[2].Trim());
				Zpoint = double.Parse(XYZ[3].Trim());
				this.Invoke(new EventHandler(XYZChartMaker));

			}
			else if (RxString[0] == '*' && RxString[RxString.Length - 2] == '*')
			{
				char[] buffering = new char[128];
				int m = 0;
				for (int l = 1; l <= (RxString.Length - 3); l++)
				{
					buffering[m] = RxString[l];
					m++;
				}
				string buffered = new string(buffering);
				string[] ADS = buffered.Split(':');

				//TODO make this tryparse so that its more stable.
				adc0 = Math.Abs(double.Parse(ADS[1].Trim()));
				adc1 = Math.Abs(double.Parse(ADS[2].Trim()));
				adc2 = Math.Abs(double.Parse(ADS[3].Trim()));
				adc3 = Math.Abs(double.Parse(ADS[4].Trim()));
				adc4 = Math.Abs(double.Parse(ADS[5].Trim()));
				adc5 = Math.Abs(double.Parse(ADS[6].Trim()));
				adc6 = Math.Abs(double.Parse(ADS[7].Trim()));
				adc7 = Math.Abs(double.Parse(ADS[8].Trim()));
				this.Invoke(new EventHandler(PressureSensors));
			}
			else
			{
				this.Invoke(new EventHandler(OutputText));
			}
		}

		private void RPHChartMaker(object sender, EventArgs e)
		{
			//adds the points to the chart. and sets its 
			chart1.Series[0].Points.AddY(Rpoint);
			chart1.Series[1].Points.AddY(Ppoint); 
			chart1.Series[2].Points.AddY(Hpoint);

			//this is how the chart left shifts. after 40 points it drops the leftmost point.
			if (chart1.Series[0].Points.Count > 40)
				chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points.Count - 40;

			//this is needed to redraw the chart
			chart1.Refresh();

			//this counter is used to sync the data points to one x value.
			ii++;

			//TODO add a timestamp in here somehow for live data. and a way to read timestamp for saved data.
		}

		private void RPHCalcOutput()
		{
			//this will be where the calculations happen 
		}

		private void XYZChartMaker(object sender, EventArgs e)
		{
			chart2.Series[0].Points.AddY(Xpoint);
			chart2.Series[1].Points.AddY(Ypoint);
			chart2.Series[2].Points.AddY(Zpoint);


			if (chart2.Series[0].Points.Count > 40)
				chart2.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points.Count - 40;

			chart2.Refresh();
			ii2++;

		}

		private void XYZCalcOutput()
		{
			//this will be where the calculations happen 
		}

		private void PressureSensors(object sender, EventArgs e)
		{
			//once out of the 
			DrawHeatmap();
		}

		private void OutputText(object sender, EventArgs e)
		{
			//outputs text into the richtext box
			ReceiverOutput.AppendText(RxString);

		}

		private void SerialKillerBtn_Click(object sender, EventArgs e)
		{
			SerialKiller();
		}

		private void SerialKiller()
		{
			//closes the arduino port. (its buggy right now...)
			//TODO make it check errors better.
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

		private void DrawHeatmap()
		{
			//invalidates the picture which forces a redraw.
			pictureBox1.Invalidate();

		}

		private void PB1(object sender, PaintEventArgs e)
		{
			//uses the heatmap class to create colours for each adc value.
			Brush aa = new SolidBrush(HeatMap.getHeatMapColor(adc0));
			Brush ab = new SolidBrush(HeatMap.getHeatMapColor(adc1));
			Brush ac = new SolidBrush(HeatMap.getHeatMapColor(adc2));
			Brush ad = new SolidBrush(HeatMap.getHeatMapColor(adc4));
			Brush ae = new SolidBrush(HeatMap.getHeatMapColor(adc5));
			Brush af = new SolidBrush(HeatMap.getHeatMapColor(adc6));
			Brush ag = new SolidBrush(HeatMap.getHeatMapColor(adc7));

			//draws filled circles onto the picture box using the brush colours previously created.
			e.Graphics.FillEllipse(aa, 74, 188, 35, 35);
			e.Graphics.FillEllipse(ab, 133, 61, 35, 35);
			e.Graphics.FillEllipse(ac, 191, 206, 35, 35);

			e.Graphics.FillEllipse(ad, 239, 101, 35, 35);
			e.Graphics.FillEllipse(ae, 274, 29, 35, 35);

			e.Graphics.FillEllipse(af, 429, 70, 35, 35);
			e.Graphics.FillEllipse(ag, 670, 132, 35, 35);

			/*
			92, 206 big toe
			151, 79  pinky toe
			208, 224 big toe ball
			257, 119 middle ball
			292, 47  pinky ball
			447, 88  middle arch
			688, 150 heel

			531,21 offset

			with offset
			623, 227
			682, 100
			739, 245
			788, 140
			823, 68
			978, 109
			1219, 171
			*/
		}

	}
}
