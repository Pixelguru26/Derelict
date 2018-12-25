using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace TrashStrat
{
	class Unit
	{
		public Texture2D Chibi;

		// battle only
		public int X;
		public int Y;

		public Unit(Texture2D Chibi)
		{
			this.Chibi = Chibi;

			X = 0;
			Y = 0;
		}
	}
}
