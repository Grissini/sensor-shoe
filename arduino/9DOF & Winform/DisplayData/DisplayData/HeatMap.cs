using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayData
{
	class HeatMap
	{
		public static Color getHeatMapColor(double value)
		{
			int red, green, blue;
			const int NUM_COLORS = 4;
			int[,] color = new int[NUM_COLORS, 3] { { 0, 0, 255 }, { 0, 255, 0 }, { 255, 255, 0 }, { 255, 0, 0 } };


			int idx1, idx2;
			//double fracval = value / 32000.00;
			double fracval = value / 2480.00;

			double fractBetween = 0;


			if (fracval <= 0) { idx1 = idx2 = 0; }
			else if (fracval >= 1) { idx1 = idx2 = NUM_COLORS - 1; }
			else
			{
				fracval = fracval * (NUM_COLORS - 1);        // Will multiply value by 3.
				idx1 = (int)Math.Floor(fracval);                  // Our desired color will be after this index.
				idx2 = idx1 + 1;                        // ... and before this index (inclusive).
				fractBetween = fracval - (double)(idx1);    // Distance between the two indexes (0-1).
			}

			red = (int)Math.Floor((color[idx2, 0] - color[idx1, 0]) * fractBetween + color[idx1, 0]);
			green = (int)Math.Floor((color[idx2, 1] - color[idx1, 1]) * fractBetween + color[idx1, 1]);
			blue = (int)Math.Floor((color[idx2,2] - color[idx1,2]) * fractBetween + color[idx1,2]);
			return Color.FromArgb(red, green, blue);
		}
	}
}
