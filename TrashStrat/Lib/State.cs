using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashStrat.Lib
{
	interface State
	{
		void Initialize();

		void LoadContent();

		void UnloadContent();

		void Update(GameTime gameTime, double deltaTime);

		void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch);
	}
}
