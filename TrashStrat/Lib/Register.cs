using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrashStrat.Lib
{
	partial class StateMachine
	{
		public event EventHandler StateRegistration;
		public delegate void StateRegistrationEventHandler(object sender, EventArgs e);
		private void RegisterStates()
		{
			// event handling
			StateRegistration?.Invoke(this, new EventArgs());

			// main registration
			// template:
			//	Register(new State(), "state");
			Register(new MenuState(),"menu");
		}
	}
}
