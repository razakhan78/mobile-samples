using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using MWC.BL;

namespace MWC.iOS.Screens.Common.Session
{
	public partial class SessionDayScheduleScreen : DialogViewController
	{
		protected SessionDetailsScreen _sessionDetailsScreen;
		protected IList<BL.Session> _sessions;
		string _dayName;
		
		/// <summary>
		/// Display sessions for the day, grouped by time slot
		/// </summary>
		public SessionDayScheduleScreen ( string dayName, int day) : base (UITableViewStyle.Grouped, null)
		{
			this._sessions = BL.Managers.SessionManager.GetSessions ( day );
			this._dayName = dayName;
			this.Title = this._dayName;

			Root = 	new RootElement (this._dayName) {
					from s in this._sessions
						group s by s.Start.Ticks into g
						orderby g.Key
						select new Section (new DateTime (g.Key).ToShortTimeString() ) {
						from hs in g
						   select (Element) new MWC.iOS.UI.CustomElements.SessionElement (hs)
			}};

		}
	}
}