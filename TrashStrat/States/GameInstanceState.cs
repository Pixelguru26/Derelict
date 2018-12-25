using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TrashStrat.Lib
{
	class GameInstanceState : State
	{
		Texture2D SceneBG;
		int Width;
		int Height;

		Unit[] playerTeam;
		Unit[] enemyTeam;

		public GameInstanceState(StateMachine Parent) : base(Parent) { }

		public override void Initialize()
		{

		}

		public override void LoadContent()
		{
			SceneBG = Parent.Host.Content.Load<Texture2D>("Grid");
			Width = 8;
			Height = 8;

			// TEMPORARY
			playerTeam = new Unit[0];
			enemyTeam = new Unit[0];
		}

		public override void UnloadContent()
		{

		}

		public override void Update(GameTime gameTime, double deltaTime)
		{

		}

		public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
		{
			DrawScene(new Rectangle(0, 0, Width * 50, Height * 50), gameTime, graphics, spriteBatch);
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
				unitPos.X = y + spaceWidth / 2;
				unitPos.Y = y + spaceHeight / 2;
				spriteBatch.Draw(v.Chibi, unitPos, null, Color.White, 0, new Vector2(v.Chibi.Width/2, v.Chibi.Height), destinationRectangle.Width / SceneBG.Width, SpriteEffects.None, layerDepth: y);
			}
			//	renders all enemy units
			for (int i = 0; i < enemyTeam.Length; i++)
			{
				v = enemyTeam[i];
				x = v.X;
				y = v.Y;
				unitPos.X = y + spaceWidth / 2;
				unitPos.Y = y + spaceHeight / 2;
				spriteBatch.Draw(v.Chibi, unitPos, null, Color.White, 0, new Vector2(v.Chibi.Width / 2, v.Chibi.Height), destinationRectangle.Width / SceneBG.Width, SpriteEffects.None, layerDepth: y);
			}
		}
	}
}
