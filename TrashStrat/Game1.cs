using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TrashStrat.Lib;
using Myra;
using Myra.Graphics2D.UI;
using System;

namespace TrashStrat
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		StateMachine stateMachine;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
			IsMouseVisible = true;
			stateMachine = new StateMachine(this);
			stateMachine.SetState("gameinstance");
        }
		
        protected override void Initialize()
        {
			stateMachine.Initialize();

            base.Initialize();
        }
		
        protected override void LoadContent()
		{
			Console.Out.WriteLine("Loading content...");

			// Create a new SpriteBatch which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			MyraEnvironment.Game = this;

			stateMachine.LoadContent();
        }
		
        protected override void UnloadContent()
        {
			stateMachine.UnloadContent();
        }
		
        protected override void Update(GameTime gameTime)
        {
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
			stateMachine.Update(gameTime);

            base.Update(gameTime);
        }
		
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();
			stateMachine.Draw(gameTime, graphics, spriteBatch);
			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
