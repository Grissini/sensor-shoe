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
		Debugging_Virtual_Shoe DVS;// = new Debugging_Virtual_Shoe();

		List<RPHData> RPHList;// = new List<RPHData>();
		List<XYZData> XYZList;// = new List<XYZData>();
		List<PressureData> PressureList;// = new List<PressureData>();

		Brush[] curbrush = new Brush[8];

		//counter variables
        int r,x,p;
		//int ii = 0, ii2 = 0;

		//memory space for the recieved string
		//string RxString;
		
		//Roll, pitch, heading values
		double Rpoint, Ppoint, Hpoint;
		
		//X,Y,Z accel values
		double Xpoint, Ypoint, Zpoint;

		//analog-to-digital converter values
		//double adc0=0, adc1=0, adc2=0, adc3=0;
		//double adc4=0, adc5=0, adc6=0, adc7=0;

		double[] adc;// = new double[8];
		//System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(
		//550, 20, 300, 300);

        bool SerialKill;// = true;

		internal bool Running, Connected, Setting;


		internal string settings;
        //byte connStatus, curState;

        private void ValueInitialization()
        {
            DVS = new Debugging_Virtual_Shoe();

            RPHList = new List<RPHData>();
            XYZList = new List<XYZData>();
            PressureList = new List<PressureData>();

            curbrush = new Brush[8];

            adc = new double[8];
            
            SerialKill = true;
			Running = false;
			Connected = false;
            Setting = false;

			settings = "";
            //connStatus = 0;
            //curState = 0;
            
            r = 0;
            x = 0;
            p = 0;
        }

		public MainApp()
		{
			InitializeComponent();
            ValueInitialization();
			InitializeCharts();
			InitializeStuff();

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

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 40;

            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 40;
		}

		private void serialInitBtn_Click(object sender, EventArgs e)
		{
            SerialKill = false;
			string portname = serialPortNames.Text;
			serialINIT(portname);
		}

		private void serialINIT(string portname = "")
		{
			if (portname != "" && portname != "VirtualCom")
			{
				ArduinoPort.PortName = portname;
                try
                {
                    ArduinoPort.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show(portname + " was unable to connect because:"+
                        Environment.NewLine + e + Environment.NewLine + "Please try again.");
                }
				if (ArduinoPort.IsOpen)
				{
					TxBtn.Enabled = true;
					serialInitBtn.Enabled = false;
					SerialKillerBtn.Enabled = true;
                    SerialKill = false;
				}
			}
			else if (portname == "VirtualCom")
			{
				virtualSerialTimer.Enabled = true;
				if (virtualSerialTimer.Enabled)
				{
					TxBtn.Enabled = true;
					serialInitBtn.Enabled = false;
					SerialKillerBtn.Enabled = true;

				}
			}
			else
			{

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

		private void virtualSerial(object sender, EventArgs e)
		{
			string RxString = DVS.virtualShoeOut();
            SerialStuff(RxString);
		}

        private void SerialStuff(string RxString)
		{
            
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
				Hpoint = (180 + double.Parse(RPH[3].Trim()));

				RPHData cur = new RPHData();
				cur.Roll = Rpoint;
				cur.Pitch = Ppoint;
				cur.Heading = Hpoint;
				RPHList.Add(cur);

				RPHChartMaker();
                r++;
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

				XYZData cur = new XYZData();
				cur.XPoint = Rpoint;
				cur.YPoint = Ppoint;
				cur.ZPoint = Hpoint;
				XYZList.Add(cur);

				XYZChartMaker();
                x++;
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
				adc[0] = Math.Abs(double.Parse(ADS[1].Trim()));
				adc[1] = Math.Abs(double.Parse(ADS[2].Trim()));
				adc[2] = Math.Abs(double.Parse(ADS[5].Trim()));
				adc[3] = Math.Abs(double.Parse(ADS[7].Trim()));
				adc[4] = Math.Abs(double.Parse(ADS[3].Trim()));
				adc[5] = Math.Abs(double.Parse(ADS[4].Trim()));
				adc[6] = Math.Abs(double.Parse(ADS[8].Trim()));
				adc[7] = Math.Abs(double.Parse(ADS[6].Trim()));


				PressureData cur = new PressureData();
				cur.pressures0 = adc[0];
				cur.pressures1 = adc[1];
				cur.pressures2 = adc[2];
				cur.pressures3 = adc[3];
				cur.pressures4 = adc[4];
				cur.pressures5 = adc[5];
				cur.pressures6 = adc[6];
				cur.pressures7 = adc[7];
				PressureList.Add(cur);

				PressureSensors(adc);
				p++;
			}
			else
			{
				OutputText(RxString);
			}
		}

		private void ArduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
            if (e.EventType != SerialData.Chars) return;
			//this.Invoke(new EventHandler(OutputText)); //for debug will display every line recieved from the arduino
            this.Invoke(new EventHandler(SerialDeBuffer));		
		}

        private void SerialDeBuffer(object sender, EventArgs e)
		{
			string RxLine = "";
			//if (!Connected && !Running && !Setting)
			//{
			//    while (ArduinoPort.BytesToRead > 0)
			    while (ArduinoPort.BytesToRead > 0)
			{
				RxLine = ArduinoPort.ReadLine();
				SerialAccept(RxLine);
			}
			//        RxLine.TrimEnd('\n');
			//        if (RxLine == "1")
			//        {
			//            byte b;
			//            if (byte.TryParse(RxLine, out b))
			//            {
			//                if (b == 1)
			//                {
			//                    ArduinoPort.WriteLine(b.ToString());
			//                    Connected = true;
			//                }
			//            }
			//        }
			//        //ReceiverOutput.AppendText(RxLine);
			//        break;
			//    }
			//}

		}

        private void SerialAccept(string s)
        {
			//if (Connected)
			//{
                if (Running && !Setting)
                {
                    SerialStuff(s);
                }
                else if (!Running && Setting)
                {
					OutputText(s);
                }
                else if (!Running && !Setting)
                {
					OutputText(s);
                }
                else
                {
                    //somethings wrong both states shouldnt be active. cancel both?
                }
			//}
        }

		internal static void SerialInvoke(object sender, EventArgs e, string s)
		{
			//SerialSend(s);
		}

        internal void SerialSend(string s = "")
        {
			string TxLine = s;
			//if (Connected)
			//{
                if (Running && !Setting)
                {
					ArduinoPort.WriteLine("r"+TxLine);
					//MessageBox.Show("r" + TxLine + Running.ToString() + Setting.ToString());
                }
                else if (!Running && Setting)
                {
					ArduinoPort.WriteLine("s" + TxLine);
					//MessageBox.Show("s" + TxLine + Setting.ToString());
					Setting = false;
					}
                else if (!Running && !Setting)
                {
					ArduinoPort.WriteLine("x"+TxLine);
					//MessageBox.Show("x" + TxLine + Running.ToString() + Setting.ToString());
                }
                else
                {
					ArduinoPort.WriteLine("x" + TxLine);
                    //somethings wrong both states shouldnt be active. cancel both?
                }
			//}
        }

		private void RPHChartMaker()
		{
			//adds the points to the chart. and sets its 
			chart1.Series[0].Points.AddY(Rpoint);
			chart1.Series[1].Points.AddY(Ppoint); 
			chart1.Series[2].Points.AddY(Hpoint);

			//this is how the chart left shifts. after 40 points it drops the leftmost point.
			if (chart1.Series[0].Points.Count > 40)
			{
				chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points.Count - 40;
				chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points.Count;
			}

			//this is needed to redraw the chart
			chart1.Refresh();

			lbl_LeftRefAngle.Text = Rpoint.ToString("F2");
			lbl_LeftGroundAngle.Text = Ppoint.ToString("F2");

			//this counter is used to sync the data points to one x value.
			//ii++;

			//TODO add a timestamp in here somehow for live data. and a way to read timestamp for saved data.
		}

		private void RPHCalcOutput()
		{
			//this will be where the calculations happen 
		}

		private void XYZChartMaker()
		{
			chart2.Series[0].Points.AddY(Xpoint);
			chart2.Series[1].Points.AddY(Ypoint);
			chart2.Series[2].Points.AddY(Zpoint);


			if (chart2.Series[0].Points.Count > 40)
			{
				chart2.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points.Count - 40;
				chart2.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points.Count;
			}

			chart2.Refresh();
			//ii2++;
			

		}

		private void XYZCalcOutput()
		{
			//this will be where the calculations happen 
		}

		private void PressureSensors(double[] adcVal, bool live = true)
		{
			if (live)
			{
				//TODO check here it keeps just adding the current one into every spot on the list.
			}
            //OutputText();
			//once out of the 
			DrawHeatmap(adcVal);
		}

		private void OutputText(string RX)
		{
			//outputs text into the richtext box
            ReceiverOutput.AppendText(RX);

		}

		private void Output(string s)
		{
			//outputs text into the richtext box
			ReceiverOutput.AppendText(s + Environment.NewLine);

		}

		private void SerialKillerBtn_Click(object sender, EventArgs e)
		{
			SerialKiller();
            SerialKill = true;
		}

		private void SerialKiller()
		{
			//closes the arduino port. (its buggy right now...)
			//TODO make it check errors better.
			if (ArduinoPort.IsOpen)
			{
				ArduinoPort.Close();
			}

			if (virtualSerialTimer.Enabled)
			{
				virtualSerialTimer.Enabled = false;
			}

			if (!ArduinoPort.IsOpen || !virtualSerialTimer.Enabled)
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

		private void DrawHeatmap(double[] adcVal)
		{
			//invalidates the picture which forces a redraw.
			
			//Output(adc0.ToString());
			//Output(adc1.ToString());
			//Output(adc2.ToString());
			//Output(adc3.ToString());
			//Output(adc4.ToString());
			//Output(adc5.ToString());
			//Output(adc6.ToString());
			//Output(adc7.ToString());
			//uses the heatmap class to create colours for each adc value.
			for (int i = 0; i < curbrush.Length; i++)
			{
				curbrush[i] = new SolidBrush(HeatMap.getHeatMapColor(adcVal[i]));
			}
			/*
			 = new SolidBrush(HeatMap.getHeatMapColor(adc[1]));
			 = new SolidBrush(HeatMap.getHeatMapColor(adc[2]));
			 = new SolidBrush(HeatMap.getHeatMapColor(adc[4]));
			 = new SolidBrush(HeatMap.getHeatMapColor(adc[5]));
			 = new SolidBrush(HeatMap.getHeatMapColor(adc[6]));
			 = new SolidBrush(HeatMap.getHeatMapColor(adc[7]));
			*/

			Graphics g = pictureBox1.CreateGraphics();
			g.FillEllipse(curbrush[0], 74, 188, 35, 35);
			g.FillEllipse(curbrush[1], 133, 61, 35, 35);
			g.FillEllipse(curbrush[2], 191, 206, 35, 35);

			g.FillEllipse(curbrush[3], 239, 101, 35, 35);
			g.FillEllipse(curbrush[4], 274, 29, 35, 35);

			g.FillEllipse(curbrush[5], 429, 70, 35, 35);
			g.FillEllipse(curbrush[6], 670, 132, 35, 35);

			pictureBox1.Refresh();
		}

		private void PB1(object sender, PaintEventArgs e)
		{

			Graphics g = e.Graphics;
			g.FillEllipse(curbrush[0], 74, 188, 35, 35);
			g.FillEllipse(curbrush[1], 133, 61, 35, 35);
			g.FillEllipse(curbrush[2], 191, 206, 35, 35);

			g.FillEllipse(curbrush[3], 239, 101, 35, 35);
			g.FillEllipse(curbrush[4], 274, 29, 35, 35);

			g.FillEllipse(curbrush[5], 429, 70, 35, 35);
			g.FillEllipse(curbrush[6], 670, 132, 35, 35);
			//draws filled circles onto the picture box using the brush colours previously created.
			/*
			92, 206 big toe
			151, 79  pinky toe
			208, 224 big toe ball
			257, 119 middle ball
			292, 47  pinky ball
			447, 88  middle arch
			688, 150 heel
			*/
		}

		private void InitializeStuff()
		{
			DrawHeatmap(adc);
			RefreshSerialNames(true);
		}

		private void refreshBtn_Click(object sender, EventArgs e)
		{
			RefreshSerialNames();
		}

		private void RefreshSerialNames(bool init = false)
		{
            serialPortNames.Items.Clear();
            serialPortNames.Items.Add("VirtualCom");
			foreach (String sp in SerialPort.GetPortNames())
			{
				serialPortNames.Items.Add(sp);
			}
			if (init)
				serialPortNames.SelectedIndex = 0;
		}

		private void XYZHistBtn_Click(object sender, EventArgs e)
		{
			foreach (XYZData xyz in XYZList)
			{
				ReceiverOutput.AppendText(xyz.XPoint.ToString() + ":" + xyz.YPoint.ToString() + ":" + xyz.ZPoint.ToString() + Environment.NewLine);
			}
		}

		private void goTo_Click(object sender, EventArgs e)
		{
			int goToLoc = 0;
			int.TryParse(goToTB.Text, out goToLoc);

			double[] adcTemp = new double[8];
			adcTemp[0] = PressureList[goToLoc].pressures0;
			adcTemp[1] = PressureList[goToLoc].pressures1;
			adcTemp[2] = PressureList[goToLoc].pressures2;
			adcTemp[3] = PressureList[goToLoc].pressures3;
			adcTemp[4] = PressureList[goToLoc].pressures4;
			adcTemp[5] = PressureList[goToLoc].pressures5;
			adcTemp[6] = PressureList[goToLoc].pressures6;
			adcTemp[7] = PressureList[goToLoc].pressures7;

			DrawHeatmap(adcTemp);

			ReceiverOutput.AppendText(XYZList[goToLoc].XPoint.ToString() + ":" + XYZList[goToLoc].YPoint.ToString() + ":" + XYZList[goToLoc].ZPoint.ToString());

			chart1.ChartAreas[0].AxisX.Minimum = goToLoc;
			chart1.ChartAreas[0].AxisX.Maximum = goToLoc + 40;

			chart2.ChartAreas[0].AxisX.Minimum = goToLoc;
			chart2.ChartAreas[0].AxisX.Maximum = goToLoc + 40;
			pictureBox1.Refresh();
		
		}

        private void GoToEnter(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return))
            {
                goTo_Click(sender, e);
            }
        }

        private void clearAll_btn_Click(object sender, EventArgs e)
        {
            ValueInitialization();
            ChartResetter();
            DrawHeatmap(adc);
        }

        private void ChartResetter()
        {

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();	

            chart1.Series[0].Points.Add(0);
            chart1.Series[1].Points.Add(0);
            chart1.Series[2].Points.Add(0);

            chart2.Series[0].Points.Add(0);
            chart2.Series[1].Points.Add(0);
            chart2.Series[2].Points.Add(0);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 40;

            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 40;
        }

        private void displayAllVals_Click(object sender, EventArgs e)
        {
            foreach (RPHData r in RPHList)
            {
                ReceiverOutput.AppendText(
                    RPHList.IndexOf(r).ToString() + ':' +
                    r.Heading.ToString() + ':' +
                    r.Pitch.ToString() + ':' +
                    r.Roll.ToString() +
                    Environment.NewLine);
            }

            foreach (XYZData x in XYZList)
            {
                ReceiverOutput.AppendText(
                    XYZList.IndexOf(x).ToString() + ':' +
                    x.XPoint.ToString() + ':' +
                    x.YPoint.ToString() + ':' +
                    x.ZPoint.ToString() +
                    Environment.NewLine);
            }

            foreach (PressureData p in PressureList)
            {
                ReceiverOutput.AppendText(
                    PressureList.IndexOf(p).ToString() + ':' +
                    p.pressures0.ToString() + ':' +
                    p.pressures1.ToString() + ':' +
                    p.pressures2.ToString() + ':' +
                    p.pressures3.ToString() + ':' +
                    p.pressures4.ToString() + ':' +
                    p.pressures5.ToString() + ':' +
                    p.pressures6.ToString() + ':' +
                    p.pressures7.ToString() +
                    Environment.NewLine);
            }


        }

        private void iterationTimer_Tick(object sender, EventArgs e)
        {
            double i=0;

            if (r > 0 && x > 0 && p > 0)
                i = (r + x + p) / 3;
            else if (r > 0 && x > 0 && p == 0)
                i = (r + x) / 2;
            else if (r > 0 && p > 0 && x == 0)
                i = (r + p) / 2;
            else if (p > 0 && x > 0 && r == 0)
                i = (p + x) / 2;
            else if (r > 0)
                i = r;
            else if (x > 0)
                i = x;
            else if (p > 0)
                p = x;
            else
                i = 0;

            refreshOP.Text = i.ToString();
            r = 0;
            x = 0;
            p = 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form DDA = new DisplayDataAbout();
            DDA.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            this.Close();
        }

		private void shoeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Settings s = new Settings(this);
			s.ShowDialog();
			if (s.DialogResult == DialogResult.Cancel)
			{
				//MessageBox.Show("You cancelled out of the settings form");
			}
			else
			{
				MessageBox.Show(settings);
			}
		}

		private void startTrackBtn_Click(object sender, EventArgs e)
		{
			Running = !Running;
			SerialSend();
		}
	}

	struct RPHData
	{
		internal double Roll;
		internal double Pitch;
		internal double Heading;
	}

	struct XYZData
	{
		internal double XPoint;
		internal double YPoint;
		internal double ZPoint;
	}

	struct PressureData
	{
		internal double pressures0;
		internal double pressures1;
		internal double pressures2;
		internal double pressures3;
		internal double pressures4;
		internal double pressures5;
		internal double pressures6;
		internal double pressures7;

		//struct Register
		//{
		//	private ushort RegisterA;

		//	internal int d
		//	{
		//		get
		//		{
		//			return RegisterA & 15;
		//		}

		//		set
		//		{

		//		}
		//	}
		//	internal int c
		//	{
		//		get
		//		{
		//			return (RegisterA >>4) & 15;
		//		}
		//		set
		//		{

		//		}
		//	}
		//	internal int b
		//	{
		//		get
		//		{
		//			return (RegisterA>>8) & 15;
		//		}
		//		set
		//		{

		//		}
		//	}
		//	internal int a
		//	{
		//		get
		//		{
		//			return (RegisterA>>12) & 15;
		//		}
		//		set
		//		{

		//		}
		//	}
		//}

		//internal PressureData(double[] press)
		//{
		//	pressures = new double[8];
		//}
	}

}

