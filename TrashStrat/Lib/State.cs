using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashStrat.Lib
{
	abstract class State
	{
		private StateMachine parent;
		public StateMachine Parent
		{
			get => parent;
		}

		public State(StateMachine Parent)
		{
			parent = Parent;
		}

		public abstract void Initialize();

		public abstract void LoadContent();

		public abstract void UnloadContent();

		public abstract void Update(GameTime gameTime, double deltaTime);

		public abstract void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch);
	}
}
