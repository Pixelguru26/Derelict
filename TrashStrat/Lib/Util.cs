using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashStrat.Lib
{
	public static class Util
	{
		public static double Floor(double a, double b)
		{
			return a - (a % b);
		}
	}
}
