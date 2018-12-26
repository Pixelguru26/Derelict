using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;

namespace TrashStrat.Lib
{
	class GameInstanceState : State
	{
		Texture2D SceneBG;
		int Width;
		int Height;
		Texture2D Arrow;
		Texture2D Selector;

		Unit[] playerTeam;
		Unit[] enemyTeam;

		Point MouseLast;
		Point MouseCurrent;
		Desktop host;

		public GameInstanceState(StateMachine Parent) : base(Parent) { }

		public override void Initialize()
		{

		}

		public override void LoadContent()
		{
			SceneBG = Parent.Host.Content.Load<Texture2D>("Grid");
			Arrow = Parent.Host.Content.Load<Texture2D>("Arrow");
			Selector = Parent.Host.Content.Load<Texture2D>("Selector");
			Width = 8;
			Height = 8;

			MouseLast = Point.Zero;
			MouseCurrent = Point.Zero;

			host = new Desktop();
			var TestBlock = new TextBlock
			{
				Text = "Hello hep is this thing working pls tell me I hope"
			};
			host.Widgets.Add(TestBlock);

			// TEMPORARY
			playerTeam = new Unit[1];
			enemyTeam = new Unit[0];
			playerTeam[0] = new Unit(Parent.Host.Content.Load<Texture2D>("UnitBase"));
		}

		public override void UnloadContent()
		{

		}

		public override void Update(GameTime gameTime, double deltaTime)
		{

		}

		public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			DrawScene(new Rectangle(0, 0, Width * 50, Height * 50), gameTime, graphics, spriteBatch);
			//spriteBatch.Draw(playerTeam[0].Chibi, Mouse.GetState().Position.ToVector2(), Color.White);
			spriteBatch.End();

			host.Bounds = new Rectangle(0, 0, graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);
			host.Render();
		}

		private void DrawScene(Rectangle destinationRectangle, GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
		{
			// general render-instance variables for convenience
			float spaceWidth = destinationRectangle.Width / Width;
			float spaceHeight = destinationRectangle.Height / Height;

			// renders the scene itself
			spriteBatch.Draw(SceneBG, destinationRectangle, Color.White);

			// renders every unit scaled by width from the bottom center of their tile
			Vector2 unitPos = new Vector2();
			float x, y;
			Unit v;
			//	renders all player units
			for (int i = 0; i < playerTeam.Length; i++)
			{
				v = playerTeam[i];
				x = v.X;
				y = v.Y;
				unitPos.X = x * spaceWidth + destinationRectangle.X + spaceWidth / 2;
				unitPos.Y = y * spaceHeight + destinationRectangle.Y + spaceHeight;
				Console.Out.WriteLine(unitPos);
				spriteBatch.Draw(v.Chibi, unitPos, v.Chibi.Bounds, Color.White, 0, new Vector2(v.Chibi.Width / 2, v.Chibi.Height), destinationRectangle.Width / (float)SceneBG.Width, SpriteEffects.None, y);
			}
			//	renders all enemy units
			for (int i = 0; i < enemyTeam.Length; i++)
			{
				v = enemyTeam[i];
				x = v.X;
				y = v.Y;
				unitPos.X = x * spaceWidth + destinationRectangle.X + spaceWidth / 2;
				unitPos.Y = y * spaceHeight + destinationRectangle.Y + spaceHeight;
				spriteBatch.Draw(v.Chibi, unitPos, null, Color.White, 0, new Vector2(v.Chibi.Width / 2, v.Chibi.Height), destinationRectangle.Width / (float)SceneBG.Width, SpriteEffects.None, y);
			}
			// render selection box
			if (destinationRectangle.Contains(Mouse.GetState().Position))
			{
				spriteBatch.Draw(
					Selector,
					new Rectangle(
						(int)Util.Floor(Mouse.GetState().X, spaceWidth),
						(int)Util.Floor(Mouse.GetState().Y, spaceHeight),
						(int)spaceWidth,
						(int)spaceHeight
						),
					Color.White
				);
			}
		}
	}
}
