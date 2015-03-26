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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.serialInitBtn = new System.Windows.Forms.Button();
			this.ArduinoPort = new System.IO.Ports.SerialPort(this.components);
			this.Input = new System.Windows.Forms.TextBox();
			this.ReceiverOutput = new System.Windows.Forms.RichTextBox();
			this.TxBtn = new System.Windows.Forms.Button();
			this.SerialKillerBtn = new System.Windows.Forms.Button();
			this.clearBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.lbl_LeftRefAngle = new System.Windows.Forms.Label();
			this.lbl_RightRefAngle = new System.Windows.Forms.Label();
			this.lbl_LeftGroundAngle = new System.Windows.Forms.Label();
			this.lbl_RightGroundAngle = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// chart1
			// 
			chartArea9.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea9);
			this.chart1.Location = new System.Drawing.Point(14, 21);
			this.chart1.Name = "chart1";
			this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			this.chart1.Size = new System.Drawing.Size(464, 175);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";
			// 
			// serialInitBtn
			// 
			this.serialInitBtn.AutoSize = true;
			this.serialInitBtn.Location = new System.Drawing.Point(14, 404);
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
			this.Input.Location = new System.Drawing.Point(1094, 337);
			this.Input.Name = "Input";
			this.Input.Size = new System.Drawing.Size(268, 21);
			this.Input.TabIndex = 2;
			// 
			// ReceiverOutput
			// 
			this.ReceiverOutput.Location = new System.Drawing.Point(1040, 366);
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
			this.TxBtn.Location = new System.Drawing.Point(1370, 337);
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
			this.SerialKillerBtn.Location = new System.Drawing.Point(162, 404);
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
			this.clearBtn.Location = new System.Drawing.Point(1040, 337);
			this.clearBtn.Name = "clearBtn";
			this.clearBtn.Size = new System.Drawing.Size(48, 23);
			this.clearBtn.TabIndex = 6;
			this.clearBtn.Text = "Clear";
			this.clearBtn.UseVisualStyleBackColor = true;
			this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
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
			// chart2
			// 
			chartArea10.Name = "ChartArea1";
			this.chart2.ChartAreas.Add(chartArea10);
			this.chart2.Location = new System.Drawing.Point(17, 219);
			this.chart2.Name = "chart2";
			this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
			this.chart2.Size = new System.Drawing.Size(461, 179);
			this.chart2.TabIndex = 16;
			this.chart2.Text = "chart2";
			// 
			// lbl_LeftRefAngle
			// 
			this.lbl_LeftRefAngle.BackColor = System.Drawing.SystemColors.Control;
			this.lbl_LeftRefAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_LeftRefAngle.Location = new System.Drawing.Point(3, 0);
			this.lbl_LeftRefAngle.Name = "lbl_LeftRefAngle";
			this.lbl_LeftRefAngle.Size = new System.Drawing.Size(55, 20);
			this.lbl_LeftRefAngle.TabIndex = 17;
			this.lbl_LeftRefAngle.Text = "0";
			// 
			// lbl_RightRefAngle
			// 
			this.lbl_RightRefAngle.BackColor = System.Drawing.SystemColors.Control;
			this.lbl_RightRefAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_RightRefAngle.Location = new System.Drawing.Point(3, 0);
			this.lbl_RightRefAngle.Name = "lbl_RightRefAngle";
			this.lbl_RightRefAngle.Size = new System.Drawing.Size(55, 20);
			this.lbl_RightRefAngle.TabIndex = 18;
			this.lbl_RightRefAngle.Text = "0";
			// 
			// lbl_LeftGroundAngle
			// 
			this.lbl_LeftGroundAngle.BackColor = System.Drawing.SystemColors.Control;
			this.lbl_LeftGroundAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_LeftGroundAngle.Location = new System.Drawing.Point(3, 0);
			this.lbl_LeftGroundAngle.Name = "lbl_LeftGroundAngle";
			this.lbl_LeftGroundAngle.Size = new System.Drawing.Size(55, 20);
			this.lbl_LeftGroundAngle.TabIndex = 19;
			this.lbl_LeftGroundAngle.Text = "0";
			// 
			// lbl_RightGroundAngle
			// 
			this.lbl_RightGroundAngle.BackColor = System.Drawing.SystemColors.Control;
			this.lbl_RightGroundAngle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_RightGroundAngle.Location = new System.Drawing.Point(3, 0);
			this.lbl_RightGroundAngle.Name = "lbl_RightGroundAngle";
			this.lbl_RightGroundAngle.Size = new System.Drawing.Size(55, 20);
			this.lbl_RightGroundAngle.TabIndex = 20;
			this.lbl_RightGroundAngle.Text = "0";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(531, 394);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 245);
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.lbl_RightGroundAngle);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(193, 125);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(184, 117);
			this.panel4.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.lbl_LeftGroundAngle);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(3, 125);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(184, 117);
			this.panel3.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lbl_RightRefAngle);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(193, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(184, 116);
			this.panel2.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lbl_LeftRefAngle);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(184, 116);
			this.panel1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(531, 361);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 23;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::DisplayData.Properties.Resources.Assem2_insole1;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBox1.Location = new System.Drawing.Point(531, 21);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(790, 277);
			this.pictureBox1.TabIndex = 24;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PB1);
			// 
			// MainApp
			// 
			this.AcceptButton = this.TxBtn;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.clearBtn;
			this.ClientSize = new System.Drawing.Size(1428, 641);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.chart2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.clearBtn);
			this.Controls.Add(this.SerialKillerBtn);
			this.Controls.Add(this.TxBtn);
			this.Controls.Add(this.ReceiverOutput);
			this.Controls.Add(this.Input);
			this.Controls.Add(this.serialInitBtn);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MainApp";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
		private System.Windows.Forms.Label lbl_LeftRefAngle;
		private System.Windows.Forms.Label lbl_RightRefAngle;
		private System.Windows.Forms.Label lbl_LeftGroundAngle;
		private System.Windows.Forms.Label lbl_RightGroundAngle;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

