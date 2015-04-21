using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayData
{
	public partial class Settings : Form
	{
		internal string settings;
		private MainApp _MainApp;
		public Settings(MainApp mainApp)
		{
			InitializeComponent();
			_MainApp = mainApp;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		internal void button1_Click(object sender, EventArgs e)
		{
			double pp, pn, rp, rn;
			if (double.TryParse(PosPitchTB.Text, out pp) || PosPitchTB.Text == "")
			{
				if (double.TryParse(NegPitchTB.Text, out pn) || PosPitchTB.Text == "")
				{
					if (double.TryParse(PosRollTB.Text, out rp) || PosRollTB.Text == "")
					{
						if (double.TryParse(NegRollTB.Text, out rn) || NegRollTB.Text == "")
						{
							//MessageBox.Show(pp.ToString("F1") + ':' + pn.ToString("F1") + ':' + rp.ToString("F1") + ':' + rn.ToString("F1") + ':');

							_MainApp.Setting = true;
							settings = pp.ToString("F1") + ':' + pn.ToString("F1") + ':' + rp.ToString("F1") + ':' + rn.ToString("F1");
							_MainApp.SerialSend(settings);

							this.Close();
						}
						else
							Error();
					}
					else
						Error();
				}
				else
					Error();
			}
			else
				Error();
						
		}

		void Error()
		{
			MessageBox.Show("Please Input an double precision number for each box.");
		}
	}
}
