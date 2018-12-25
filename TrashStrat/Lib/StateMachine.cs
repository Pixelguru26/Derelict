using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashStrat.Lib
{
	partial class StateMachine
	{
		private Game1 host;
		public Game1 Host
		{
			get => host;
		}

		// storage system for states
		Dictionary<string, int> StatesDict;
		List<State> StatesList;
		public State CurrentState;

		// constructor
		public StateMachine(Game1 Host)
		{
			host = Host;
			StatesDict = new Dictionary<string, int>();
			StatesList = new List<State>();
			RegisterStates();
		}
		
		// storage system manipulators
		//	gets current state
		public State GetState()
		{
			return CurrentState;
		}
		//	"safely" returns state by string name
		public State GetState(string Key)
		{
			int index;
			StatesDict.TryGetValue(Key, out index);
			return GetState(index);
		}
		//	"safely" returns state by int key
		public State GetState(int Key)
		{
			if (Key >= 0 && Key < StatesList.Count)
			{
				return StatesList[Key];
			} else
			{
				return null;
			}
		}
		//	"safely" sets the current state by string name
		public State SetState(string Key)
		{
			State res = GetState(Key);
			if (res != null)
			{
				CurrentState = res;
			}
			return res;
		}
		//	registers a state to the storage system
		public State Register(State state, string name)
		{
			StatesDict.Add(name, StatesList.Count);
			StatesList.Add(state);
			return state;
		}
		public State Register(Type type, string name = null)
		{
			name = name == null ? type.Name : name;
			State state = (State)Activator.CreateInstance(type);
			Register(state, name);
			return state;
		}
		//	removes a state from the storage system
		public void Remove(string name)
		{
			int index;
			bool found = StatesDict.TryGetValue(name, out index);
			if (found)
			{
				StatesDict.Remove(name);
				StatesList.RemoveAt(index);
			}
		}

		// callbacks
		public event EventHandler<InitializationEventArgs> StateInitialization;
		public delegate void StateInitializationEventHandler(object sender, InitializationEventArgs e);
		public class InitializationEventArgs : EventArgs
		{

		}
		public void Initialize()
		{
			StateInitialization?.Invoke(this, new InitializationEventArgs());
			//CurrentState?.Initialize();
			for (int i = 0; i < StatesList.Count; i++)
			{
				StatesList[i].Initialize();
			}
		}

		public event EventHandler<LoadEventArgs> StateLoad;
		public delegate void StateLoadEventHandler(object sender, LoadEventArgs e);
		public class LoadEventArgs : EventArgs
		{

		}
		public void LoadContent()
		{
			StateLoad?.Invoke(this, new LoadEventArgs());
			CurrentState?.LoadContent();
		}

		public event EventHandler<UnloadEventArgs> StateUnload;
		public delegate void StateUnloadEventHandler(object sender, UnloadEventArgs e);
		public class UnloadEventArgs : EventArgs
		{

		}
		public void UnloadContent()
		{
			StateUnload?.Invoke(this, new UnloadEventArgs());
			CurrentState?.UnloadContent();
		}

		public event EventHandler<UpdateEventArgs> StateUpdate;
		public delegate void StateUpdateEventHandler(object sender, UpdateEventArgs e);
		public class UpdateEventArgs : EventArgs
		{
			public GameTime GameTime;
			public double DeltaTime
			{
				get
				{
					return GameTime.ElapsedGameTime.TotalSeconds;
				}
			}
			public UpdateEventArgs(GameTime gameTime)
			{
				GameTime = gameTime;
			}
		}
		public void Update(GameTime gameTime)
		{
			StateUpdate?.Invoke(this, new UpdateEventArgs(gameTime));
			CurrentState?.Update(gameTime,gameTime.ElapsedGameTime.TotalSeconds);
		}

		public event EventHandler<DrawEventArgs> StateDraw;
		public delegate void StateDrawEventHandler(object sender, DrawEventArgs e);
		public class DrawEventArgs : EventArgs
		{
			public GameTime GameTime;
			public GraphicsDeviceManager Graphics;
			public SpriteBatch SpriteBatch;
			public DrawEventArgs(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
			{
				GameTime = gameTime;
				Graphics = graphics;
				SpriteBatch = spriteBatch;
			}
		}
		public void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
		{
			StateDraw?.Invoke(this, new DrawEventArgs(gameTime, graphics, spriteBatch));
			CurrentState?.Draw(gameTime, graphics, spriteBatch);
		}
	}
}
