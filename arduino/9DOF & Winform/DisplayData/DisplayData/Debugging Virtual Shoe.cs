using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayData
{
	internal class Debugging_Virtual_Shoe
	{
		double R = 0;
		double P = 0;
		double H = 0;

		int[] adc = new int[8];

		double Xaccel = 0, Yaccel = 0, Zaccel = 0;

		int counter = 0;


		internal string virtualShoeOut()
		{
			string TempSerialOut;
			switch (counter)
			{
				case 0:
					TempSerialOut = ADCOut();
					counter++;
					break;
				case 1:
					TempSerialOut = XYZOut();
					counter++;
					break;

				case 2:
					TempSerialOut = RPHOut();
					counter = 0;
					break;
				default:
					TempSerialOut = "";
					counter = 0;
					break;
			}

			return TempSerialOut;
		}

		private string ADCOut()
		{
			Random randomAnalog = new Random();
			int index = 0;
			foreach (int i in adc)
			{
				adc[index] = randomAnalog.Next(32000);
				index++;
			}

			string ADCstr = "*" + ":" + adc[0] + ":" + adc[1] + ":" + adc[2] + ":" + adc[3] + ":" + adc[4] + ":" + adc[5] + ":" + adc[6] + ":" + adc[7] + ":*\n";
			return ADCstr;
			//Serial.println(ADCstr);
		}

		private string XYZOut()
		{
			Random randomXYZ = new Random();
			Xaccel = randomXYZ.Next(20)-10;
			Yaccel = randomXYZ.Next(20)-10;
			Zaccel = randomXYZ.Next(20)-10;


			string XYZstr = "&" + ":" + Xaccel + ":" + Yaccel + ":" + Zaccel + ":&\n";
			return XYZstr;
			//Serial.println(XYZstr);
		}

		private string RPHOut()
		{
			Random randomRPH = new Random();
			R = randomRPH.Next(360) - 180;
			P = randomRPH.Next(180) - 90;
			H = randomRPH.Next(360) - 180;


			string RPHstr = "~" + ":" + R + ":" + P + ":" + H + ":~\n";
			return RPHstr;
			//Serial.println(RPHstr);
		}
	}
}
