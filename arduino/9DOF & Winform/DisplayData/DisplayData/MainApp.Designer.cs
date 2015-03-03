namespace DisplayData
{
	partial class MainApp
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.serialInitBtn = new System.Windows.Forms.Button();
			this.ArduinoPort = new System.IO.Ports.SerialPort(this.components);
			this.Input = new System.Windows.Forms.TextBox();
			this.ReceiverOutput = new System.Windows.Forms.RichTextBox();
			this.TxBtn = new System.Windows.Forms.Button();
			this.SerialKillerBtn = new System.Windows.Forms.Button();
			this.clearBtn = new System.Windows.Forms.Button();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.progressBar3 = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
			this.SuspendLayout();
			// 
			// chart1
			// 
			chartArea3.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea3);
			this.chart1.Location = new System.Drawing.Point(14, 21);
			this.chart1.Name = "chart1";
			this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			this.chart1.Size = new System.Drawing.Size(453, 348);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			// 
			// serialInitBtn
			// 
			this.serialInitBtn.AutoSize = true;
			this.serialInitBtn.Location = new System.Drawing.Point(12, 507);
			this.serialInitBtn.Name = "serialInitBtn";
			this.serialInitBtn.Size = new System.Drawing.Size(126, 23);
			this.serialInitBtn.TabIndex = 1;
			this.serialInitBtn.Text = "Open Serial Comm";
			this.serialInitBtn.UseVisualStyleBackColor = true;
			this.serialInitBtn.Click += new System.EventHandler(this.serialInitBtn_Click);
			// 
			// ArduinoPort
			// 
			this.ArduinoPort.BaudRate = 115200;
			this.ArduinoPort.DtrEnable = true;
			this.ArduinoPort.PortName = "COM7";
			this.ArduinoPort.RtsEnable = true;
			this.ArduinoPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ArduinoPort_DataReceived);
			// 
			// Input
			// 
			this.Input.Location = new System.Drawing.Point(1042, 12);
			this.Input.Name = "Input";
			this.Input.Size = new System.Drawing.Size(320, 21);
			this.Input.TabIndex = 2;
			// 
			// ReceiverOutput
			// 
			this.ReceiverOutput.Location = new System.Drawing.Point(1040, 41);
			this.ReceiverOutput.Name = "ReceiverOutput";
			this.ReceiverOutput.ReadOnly = true;
			this.ReceiverOutput.Size = new System.Drawing.Size(376, 263);
			this.ReceiverOutput.TabIndex = 3;
			this.ReceiverOutput.Text = "";
			// 
			// TxBtn
			// 
			this.TxBtn.AutoSize = true;
			this.TxBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TxBtn.Enabled = false;
			this.TxBtn.Location = new System.Drawing.Point(1370, 12);
			this.TxBtn.Name = "TxBtn";
			this.TxBtn.Size = new System.Drawing.Size(46, 23);
			this.TxBtn.TabIndex = 4;
			this.TxBtn.Text = "Send";
			this.TxBtn.UseVisualStyleBackColor = true;
			this.TxBtn.Click += new System.EventHandler(this.TxBtn_Click);
			// 
			// SerialKillerBtn
			// 
			this.SerialKillerBtn.AutoSize = true;
			this.SerialKillerBtn.Enabled = false;
			this.SerialKillerBtn.Location = new System.Drawing.Point(173, 507);
			this.SerialKillerBtn.Name = "SerialKillerBtn";
			this.SerialKillerBtn.Size = new System.Drawing.Size(128, 23);
			this.SerialKillerBtn.TabIndex = 5;
			this.SerialKillerBtn.Text = "Close Serial Comm";
			this.SerialKillerBtn.UseVisualStyleBackColor = true;
			this.SerialKillerBtn.Click += new System.EventHandler(this.SerialKillerBtn_Click);
			// 
			// clearBtn
			// 
			this.clearBtn.AutoSize = true;
			this.clearBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.clearBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.clearBtn.Location = new System.Drawing.Point(1368, 337);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(48, 23);
			this.clearBtn.TabIndex = 6;
			this.clearBtn.Text = "Clear";
			this.clearBtn.UseVisualStyleBackColor = true;
			this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
			// 
			// progressBar2
			// 
			this.progressBar2.Location = new System.Drawing.Point(12, 466);
			this.progressBar2.Maximum = 32767;
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(377, 23);
			this.progressBar2.Step = 1;
			this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar2.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "label1";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 427);
			this.progressBar1.Maximum = 32767;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.progressBar1.Size = new System.Drawing.Size(377, 23);
			this.progressBar1.Step = 1;
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 11;
			// 
			// trackBar1
			// 
			this.trackBar1.AutoSize = false;
			this.trackBar1.Location = new System.Drawing.Point(474, 21);
			this.trackBar1.Maximum = 180;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar1.Size = new System.Drawing.Size(29, 348);
			this.trackBar1.TabIndex = 14;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
			// 
			// chart2
			// 
			chartArea4.Name = "ChartArea1";
			this.chart2.ChartAreas.Add(chartArea4);
			this.chart2.Location = new System.Drawing.Point(509, 21);
			this.chart2.Name = "chart2";
			this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			this.chart2.Size = new System.Drawing.Size(453, 348);
			this.chart2.TabIndex = 16;
			this.chart2.Text = "chart2";
			// 
			// progressBar3
			// 
			this.progressBar3.Location = new System.Drawing.Point(12, 375);
			this.progressBar3.Maximum = 361;
			this.progressBar3.Name = "progressBar3";
			this.progressBar3.Size = new System.Drawing.Size(377, 23);
			this.progressBar3.Step = 1;
			this.progressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar3.TabIndex = 17;
			// 
			// MainApp
			// 
			this.AcceptButton = this.TxBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.clearBtn;
			this.ClientSize = new System.Drawing.Size(1428, 641);
			this.Controls.Add(this.progressBar3);
			this.Controls.Add(this.chart2);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.progressBar2);
			this.Controls.Add(this.clearBtn);
			this.Controls.Add(this.SerialKillerBtn);
			this.Controls.Add(this.TxBtn);
			this.Controls.Add(this.ReceiverOutput);
			this.Controls.Add(this.Input);
			this.Controls.Add(this.serialInitBtn);
			this.Controls.Add(this.chart1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainApp";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
		private System.Windows.Forms.Button serialInitBtn;
		private System.IO.Ports.SerialPort ArduinoPort;
		private System.Windows.Forms.TextBox Input;
		private System.Windows.Forms.RichTextBox ReceiverOutput;
		private System.Windows.Forms.Button TxBtn;
		private System.Windows.Forms.Button SerialKillerBtn;
		private System.Windows.Forms.Button clearBtn;
		private System.Windows.Forms.ProgressBar progressBar2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
		private System.Windows.Forms.ProgressBar progressBar3;
	}
}

